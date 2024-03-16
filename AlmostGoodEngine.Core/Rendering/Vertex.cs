using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AlmostGoodEngine.Core.Rendering
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Vertex(Vector3 position, Vector2 textureCoordinate, Color color) : IVertexType
	{
		public Vector3 Position = position;
		public Vector2 TextureCoordinate = textureCoordinate;
		public Color Color = color;
		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

		public override readonly int GetHashCode()
		{
			return System.HashCode.Combine(Position, TextureCoordinate, Color);
		}

		public override readonly string ToString()
		{
			return
				"{{Position: " + Position +
				" TextureCooardinate: " + TextureCoordinate +
				" Color: " + Color +
				"}}";
		}

		public static bool operator ==(Vertex left, Vertex right)
		{
			return left.Position == right.Position &&
				left.TextureCoordinate == right.TextureCoordinate &&
				left.Color == right.Color;
		}

		public static bool operator !=(Vertex left, Vertex right)
		{
			return !(left == right);
		}

		public override readonly bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj.GetType() != base.GetType())
			{
				return false;
			}

			return this == (Vertex) obj;
		}

		static Vertex()
		{
			int offset = 0;
			var elements = new VertexElement[]
			{
				GetVertexElement(ref offset, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				GetVertexElement(ref offset, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
				GetVertexElement(ref offset, VertexElementFormat.Color, VertexElementUsage.Color, 0),
			};
			VertexDeclaration = new(elements);
		}

		private static VertexElement GetVertexElement(ref int offset, VertexElementFormat format, VertexElementUsage usage, int usageIndex)
		{
			return new(OffsetInline(ref offset, Offsets[format]), format, usage, usageIndex);
		}

		private static int OffsetInline(ref int value, int offset)
		{
			int old = value;
			value += offset;
			return old;
		}

		private static readonly Dictionary<VertexElementFormat, int> Offsets = new()
		{
			[VertexElementFormat.Single] = 4,
			[VertexElementFormat.Vector2] = 8,
			[VertexElementFormat.Vector3] = 12,
			[VertexElementFormat.Vector4] = 16,
			[VertexElementFormat.Color] = 4,
			[VertexElementFormat.Byte4] = 4,
			[VertexElementFormat.Short2] = 4,
			[VertexElementFormat.Short4] = 8,
			[VertexElementFormat.NormalizedShort2] = 4,
			[VertexElementFormat.NormalizedShort4] = 8,
			[VertexElementFormat.HalfVector2] = 4,
			[VertexElementFormat.HalfVector4] = 8,
		};
	}
}
