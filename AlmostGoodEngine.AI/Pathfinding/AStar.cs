using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AlmostGoodEngine.AI.Pathfinding
{
    public class AStar
    {
        /// <summary>
        /// List of remained nodes to check
        /// </summary>
        private List<Node> _openList;

        /// <summary>
        /// List of already checked nodes
        /// </summary>
        private List<Node> _closeList;

        /// <summary>
        /// Array of nodes
        /// </summary>
        private Node[,] _nodes;

        /// <summary>
        /// The start position of the path
        /// </summary>
        private Vector2 _startPosition;

        /// <summary>
        /// The end position of the path
        /// </summary>
        private Vector2 _endPosition;

        /// <summary>
        /// If the path is using a tilemap
        /// </summary>
        private int _tileSize;

        /// <summary>
        /// If the algorithm can take diagonals as shortcuts
        /// </summary>
        private bool _canUseDiagonals;

        public AStar(
            int width, 
            int height, 
            int tileSize, 
            bool canUseDiagonals, 
            Vector2 startPosition,
            Vector2 endPosition,
            bool[,] obstacles)
        {
            _startPosition = startPosition;
            _endPosition = endPosition;

            _openList = new List<Node>();
            _closeList = new List<Node>();

            _tileSize = tileSize;
            _canUseDiagonals = canUseDiagonals;

            int nodeWidth = width / _tileSize;
            int nodeHeight = height / _tileSize;

            _nodes = new Node[nodeWidth, nodeHeight];

            // All the possible nodes
            for (int y = 0; y < nodeHeight; y++)
            {
                for (int x = 0; x < nodeWidth; x++)
                {
                    Vector2 position = new(
                        x * _tileSize + _tileSize / 2,
                        y * _tileSize + _tileSize / 2
                        );

                    bool isObstacle = obstacles[x, y];

                    // Create the node
                    Node node = new(position, Vector2.Distance(position, endPosition), isObstacle);
                    _nodes[x, y] = node;
                }
            }
        }

        /// <summary>
        /// Algorithm to find the most optimized path
        /// </summary>
        /// <returns></returns>
        public List<Vector2> FindPath()
        {
            _openList.Add(_nodes[(int)_startPosition.X / _tileSize, (int)_startPosition.Y / _tileSize]);

            while (_openList.Count > 0)
            {
                Node currentNode = _openList[0];
                for (int i = 1; i < _openList.Count; i++)
                {
                    if (_openList[i].TotalCost < currentNode.TotalCost)
                    {
                        currentNode = _openList[i];
                    }
                }

                currentNode.Cost = 0;
                if (currentNode.Parent != null)
                {
                    currentNode.Cost = currentNode.Parent.Cost + Vector2.Distance(currentNode.Parent.Position, currentNode.Position);
                }

                _openList.Remove(currentNode);
                _closeList.Add(currentNode);

                if (currentNode.Position == _endPosition)
                {
                    return ReconstructPath(currentNode);
                }

                List<Node> neighbors = GetNeighbors(currentNode);
                foreach (Node node in neighbors)
                {
                    if (_closeList.Contains(node) || node.IsObstacle)
                    {
                        continue;
                    }

                    float cost = currentNode.Cost + Vector2.Distance(currentNode.Position, node.Position);
                    if (cost < node.Cost)
                    {
                        node.Cost = cost;
                        node.Parent = currentNode;
                        if (!_openList.Contains(node))
                        {
                            _openList.Add(node);
                        }
                    }
                }
            }

            return new List<Vector2>();
        }

        /// <summary>
        /// Get neighbors nodes of the given node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    if (!_canUseDiagonals)
                    {
                        if (x == -1 && y == -1)
                        {
                            continue;
                        }

                        if (x == 1 && y == -1)
                        {
                            continue;
                        }

                        if (x == 1 && y == 1)
                        {
                            continue;
                        }

                        if (x == -1 && y == 1)
                        {
                            continue;
                        }
                    }

                    int neighborX = ((int)node.Position.X - _tileSize / 2) / _tileSize + x;
                    int neighborY = ((int)node.Position.Y - _tileSize / 2) / _tileSize + y;

                    if (neighborX >= 0 && neighborX < _nodes.GetLength(0) && neighborY >= 0 && neighborY < _nodes.GetLength(1))
                    {
                        neighbors.Add(_nodes[neighborX, neighborY]);
                    }
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Reconstruct the optimized path from a node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<Vector2> ReconstructPath(Node node)
        {
            List<Vector2> path = new();
            while (node.Parent != null)
            {
                path.Add(node.Position);
                node = node.Parent;
            }

            path.Reverse();
            return path;
        }
    }
}
