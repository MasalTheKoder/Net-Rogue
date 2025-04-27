using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum Race
    {
        Human,
        Elf,
        Orc
    }

    public enum Class
    {
        Rogue,
        Warrior,
        Magician
    }

    internal class PlayerCharacter
    {
        public string name;
        public Race rotu;
        public Class luokka;
        public Vector2 paikka;
        public Color playerColor;

        public PlayerCharacter(string name, Race rotu, Class luokka, Color color)
        {
            this.name = name;
            this.rotu = rotu;
            this.luokka = luokka;
            this.playerColor = color;
        }

        public void Draw()
        {
            Console.SetCursorPosition((int)paikka.X, (int)paikka.Y);
            Console.Write("@");
        }
    }
}
