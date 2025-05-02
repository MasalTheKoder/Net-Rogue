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
        public Texture playerImage;

        public PlayerCharacter(string name, Race rotu, Class luokka)
        {
            this.name = name;
            this.rotu = rotu;
            this.luokka = luokka;
            this.paikka = new Vector2(1, 1); 
        }

        public void LoadTexture()
        {
            playerImage = Raylib.LoadTexture(@"Images\Player.png");

            if (playerImage.id == 0)
            {
                Console.WriteLine("Failed to load texture!");
            }
            else
            {
                Console.WriteLine("Texture loaded successfully!");
            }
        }

        public void Draw()
        {
            Raylib.DrawTextureV(playerImage, new Vector2(paikka.X * Game.tileSize, paikka.Y * Game.tileSize), Raylib.WHITE);
        }
    }
}