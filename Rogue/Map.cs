using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public enum MapTile : int
    {
        Floor = 48,
        Wall = 40
    }

    internal class Map
    {
        public int mapWidth;
        public int[] mapTiles;
        public Texture spriteAtlas;
        const int imagesPerRow = 12;

        public int Width => mapWidth;
        public int Height => mapTiles.Length / mapWidth;

        public int getTile(int x, int y)
        {
            int index = x + y * mapWidth;
            int tileId = mapTiles[index];
            return tileId;
        }

        public MapTile GetTileAt(int x, int y)
        {
            int indexInMap = x + y * mapWidth;
            int mapTileAtIndex = mapTiles[indexInMap];
            return (MapTile)mapTileAtIndex;
        }

        public void Draw()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = x + y * mapWidth;
                    int tileId = mapTiles[index];

                    int imageX = tileId % imagesPerRow;
                    int imageY = (int)(tileId / imagesPerRow);

                    int drawPixelX = x * Game.tileSize;
                    int drawPixelY = y * Game.tileSize;

                    switch ((MapTile)tileId)
                    {
                        case MapTile.Floor:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.DARKGRAY);
                            break;
                        case MapTile.Wall:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.DARKGREEN);
                            break;
                        default:
                            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.BLACK);
                            break;
                    }
                }
            }
        }
        public void LoadTexture()
        {
            spriteAtlas = Raylib.LoadTexture(@"Images\Map.png");
        }

        public void ClearTile(int x, int y)
        {
            int drawPixelX = x * Game.tileSize;
            int drawPixelY = y * Game.tileSize;
            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.BLACK);
        }


    }
}