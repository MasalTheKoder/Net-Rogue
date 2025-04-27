using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;

        public int Width
        {
            get { return mapWidth; }
        }

        public int Height
        {
            get { return mapTiles.Length / mapWidth; }
        }

        public int getTile(int x, int y)
        {
            int index = x + y * mapWidth;
            int tileId = mapTiles[index];
            return tileId;
        }

        public void Draw()
        {
            int mapHeight = Height; 
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = mapTiles[index];

                    int drawPixelX = x * Game.tileSize;
                    int drawPixelY = y * Game.tileSize;

                    switch (tileId)
                    {
                        case 1:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.DARKGRAY);
                            break;
                        case 2:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.DARKGREEN);
                            break;
                        case 3:
                            Raylib.DrawText(">", drawPixelX + 4, drawPixelY + 4, Game.tileSize, Raylib.RED);
                            break;
                        default:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.BLACK);
                            break;
                    }
                }
            }
        }

        public void ClearTile(int x, int y)
        {
            int drawPixelX = x * Game.tileSize;
            int drawPixelY = y * Game.tileSize;
            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.BLACK);
        }
    }
}
