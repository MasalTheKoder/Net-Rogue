using System.Numerics;

namespace Rogue
{
    internal class Item
    {
        public string name;
        public Vector2 position;
        public int spriteIndex;

        public Item(string name, Vector2 position, int spriteIndex)
        {
            this.name = name;
            this.position = position;
            this.spriteIndex = spriteIndex;
        }
    }
}
