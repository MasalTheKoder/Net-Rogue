using System.Numerics;

namespace Rogue
{
    /// <summary>
    /// Represents an item in the game world.
    /// </summary>
    internal class Item
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        public string name;

        /// <summary>
        /// The position of the item in the game.
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// The index of the sprite used to visually represent the item.
        /// </summary>
        public int spriteIndex;

        /// <summary>
        /// Constructs a new item with the specified name, position, and sprite index.
        /// </summary>
        public Item(string name, Vector2 position, int spriteIndex)
        {
            this.name = name;
            this.position = position;
            this.spriteIndex = spriteIndex;
        }
    }
}
