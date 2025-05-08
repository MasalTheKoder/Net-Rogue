using System.Numerics;

namespace Rogue
{
    /// <summary>
    /// Represents an enemy in the game world.
    /// </summary>
    internal class Enemy
    {
        /// <summary>
        /// The name of the enemy
        /// </summary>
        public string name;

        /// <summary>
        /// The enemy's position on the map grid.
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// The index of the sprite used to visually represent the enemy.
        /// </summary>
        public int spriteIndex;

        /// <summary>
        /// Constructs a new enemy with the specified name, position, and sprite index.
        /// </summary>
        public Enemy(string name, Vector2 position, int spriteIndex)
        {
            this.name = name;
            this.position = position;
            this.spriteIndex = spriteIndex;
        }
    }
}
