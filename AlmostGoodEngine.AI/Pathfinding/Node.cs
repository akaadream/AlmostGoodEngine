using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.AI.Pathfinding
{
    public class Node
    {
        /// <summary>
        /// The position of the node
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The cost of the node
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// The heuristic of the node
        /// </summary>
        public float Heuristic { get; set; }

        /// <summary>
        /// Cost + Heuristic
        /// </summary>
        public float TotalCost { get; set; }

        /// <summary>
        /// The parent node of this node
        /// </summary>
        public Node Parent { get; set; }

        /// <summary>
        /// If the node is representing an obstacle
        /// </summary>
        public bool IsObstacle { get; set; }

        public Node(Vector2 position, float heuristic, bool isObstacle)
        {
            Position = position;
            Heuristic = heuristic;
            IsObstacle = isObstacle;

            Cost = float.MaxValue;
        }
    }
}
