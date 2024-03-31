using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AlmostGoodEngine.Core.Rendering
{
	public class Batcher
	{
		public GraphicsDevice GraphicsDevice { get; set; }

		Effect _shader = null!;

		const int _initialSprites = 2048;
		const int _initialVertices = _initialSprites * 4;
		const int _initialIndices = _initialSprites * 6;

		Texture2D _texture = null!;
		SamplerState _samplerState = null!;
		Matrix _view;
		Matrix _projection;

		Vertex[] _vertices = null!;
		uint[] _indices = null!;
		DynamicVertexBuffer _vertexBuffer = null!;
		IndexBuffer _indexBuffer = null!;

		int _triangleCount = 0;
		int _vertexCount = 0;
		int _indexCount = 0;

		bool _indicesChanged = false;
		uint _fromIndex = 0;
		uint _fromVertex = 0;

		public Batcher(GraphicsDevice graphicsDevice)
		{
			GraphicsDevice = graphicsDevice;

			_shader = GameManager.Engine.Content.Load<Effect>("shaders/first-shader");

			_vertices = new Vertex[_initialVertices];
			_indices = new uint[_initialIndices];

			GenerateIndexArray();

			_vertexBuffer = new(GraphicsDevice, typeof(Vertex), _vertices.Length, BufferUsage.WriteOnly);

			_indexBuffer = new(GraphicsDevice, typeof(uint), _indices.Length, BufferUsage.WriteOnly);
			_indexBuffer.SetData(_indices);
		}

		public void Begin(Texture2D? texture = null, Matrix? view = null, Matrix? projection = null, SamplerState? sampler = null)
		{
			Viewport viewport = GraphicsDevice.Viewport;

			_texture = texture;
			_view = view ?? Matrix.Identity;
			_projection = projection ?? Matrix.CreateOrthographicOffCenter(viewport.X, viewport.Width, viewport.Height, viewport.Y, 0, 1);
			_samplerState = sampler ?? SamplerState.LinearClamp;
		}

		public void Draw(Vector2 xy, Color? color = null)
		{
			EnsureSizeOrDouble(ref _vertices, _vertexCount + 4);
			_indicesChanged = EnsureSizeOrDouble(ref _indices, _indexCount + 6) || _indicesChanged;

			Vector2 topLeft = xy + new Vector2(0f, 0f);
			Vector2 topRight = xy + new Vector2(_texture.Width, 0f);
			Vector2 bottomRight = xy + new Vector2(_texture.Width, _texture.Height);
			Vector2 bottomLeft = xy + new Vector2(0f, _texture.Height);

			color ??= Color.White;

			_vertices[_vertexCount + 0] = new(
				new Vector3(topLeft, 0),
				new Vector2(0f, 0f),
				color.Value
			);
			_vertices[_vertexCount + 1] = new(
				new Vector3(topRight, 0f),
				new Vector2(1f, 0f),
				color.Value
			);
			_vertices[_vertexCount + 2] = new(
				new Vector3(bottomRight, 0f),
				new Vector2(1f, 1f),
				color.Value
			);
			_vertices[_vertexCount + 3] = new(
				new Vector3(bottomLeft, 0f),
				new Vector2(0f, 1f),
				color.Value
			);

			_triangleCount += 2;
			_vertexCount += 4;
			_indexCount += 6;
		}

		public void End()
		{
			if (_triangleCount == 0)
			{
				return;
			}

			if (_indicesChanged)
			{
				_vertexBuffer.Dispose();
				_indexBuffer.Dispose();

				_vertexBuffer = new(GraphicsDevice, typeof(Vertex), _vertices.Length, BufferUsage.WriteOnly);

				GenerateIndexArray();

				_indexBuffer = new(GraphicsDevice, typeof(uint), _indices.Length, BufferUsage.WriteOnly);
				_indexBuffer.SetData(_indices);

				_indicesChanged = false;
			}

			_vertexBuffer.SetData(_vertices);
			GraphicsDevice.SetVertexBuffer(_vertexBuffer);

			GraphicsDevice.Indices = _indexBuffer;

			_shader.Parameters["view_projection"].SetValue(_view * _projection);
			GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			GraphicsDevice.DepthStencilState = DepthStencilState.None;
			GraphicsDevice.BlendState = BlendState.AlphaBlend;
			GraphicsDevice.SamplerStates[0] = _samplerState;
			GraphicsDevice.Textures[0] = _texture;

			_shader.CurrentTechnique.Passes[0].Apply();
			GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _triangleCount);

			_triangleCount = 0;
			_vertexCount = 0;
			_indexCount = 0;
		}

		private static bool EnsureSizeOrDouble<T>(ref T[] array, int neededCapacity)
		{
			if (array.Length < neededCapacity)
			{
				Array.Resize(ref array, array.Length * 2);
				return true;
			}

			return false;
		}

		private void GenerateIndexArray()
		{
			for (uint i = _fromIndex, j = _fromVertex; i < _indices.Length; i += 6, j += 4)
			{
				_indices[i + 0] = j + 0;
				_indices[i + 1] = j + 1;
				_indices[i + 2] = j + 3;
				_indices[i + 3] = j + 1;
				_indices[i + 4] = j + 2;
				_indices[i + 5] = j + 3;
			}

			_fromIndex = (uint)_indices.Length;
			_fromVertex = (uint)_vertices.Length;
		}
	}
}
