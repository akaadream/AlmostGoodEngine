namespace AlmostGoodEngine.Physics
{
	public class VirtualMap<T>
	{
		private T[,] _map { get; set; }

		public VirtualMap(int width, int height)
		{
			_map = new T[width, height];
		}

		public bool InTile(int cellX, int cellY)
		{
			return _map[cellX, cellY] != null;
		}

		public T this[int cellX, int cellY]
		{
			get
			{
				if (cellX < 0 || cellY < 0 || cellX >= _map.GetLength(0) || cellY >= _map.GetLength(1))
				{
					return default;
				}

				return _map[cellX, cellY];
			}

			set
			{
				if (cellX < 0 || cellY < 0 || cellX >= _map.GetLength(0) || cellY >= _map.GetLength(1))
				{
					return;
				}

				_map[cellX, cellY] = value;
			}
		}

		public T[,] ToArray()
		{
			return _map;
		}
	}
}
