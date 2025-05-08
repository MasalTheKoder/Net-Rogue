using System;
using System.Collections.Generic;
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
        public int mapHeight;
        public MapLayer[] layers;
        public Texture spriteAtlas;
        const int imagesPerRow = 12;

        public List<Enemy> enemies;
        public List<Item> items;

        public int Width => mapWidth;
        public int Height => layers[0].mapTiles.Length / mapWidth;

        public Map()
        {
            mapWidth = 1;
            mapHeight = 1;
            layers = new MapLayer[3];
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new MapLayer(mapWidth * mapHeight);
            }
            enemies = new List<Enemy>() { };
            items = new List<Item>() { };
        }

        public void LoadTexture()
        {
            spriteAtlas = Raylib.LoadTexture(@"Images\Map.png");
        }

        public MapTile GetTileAt(int x, int y)
        {
            MapLayer groundLayer = GetLayer("ground");
            int indexInMap = x + y * mapWidth;
            int mapTileAtIndex = groundLayer.mapTiles[indexInMap];
            return (MapTile)mapTileAtIndex;
        }

        public Vector2 GetSpritePosition(int spriteIndex, int spritesPerRow)
        {
            float spritePixelX = (spriteIndex % spritesPerRow) * Game.tileSize;
            float spritePixelY = (spriteIndex / spritesPerRow) * Game.tileSize;
            return new Vector2(spritePixelX, spritePixelY);
        }

        /// <summary>
        /// Draws the map, its items and enemies
        /// </summary>
        public void Draw()
        {
            MapLayer groundLayer = GetLayer("ground");
            int[] mapTiles = groundLayer.mapTiles;
            int height = mapTiles.Length / mapWidth;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int tileId = mapTiles[x + y * mapWidth];
                    int spriteId = tileId -1;

                    Vector2 spritePosition = GetSpritePosition(spriteId, imagesPerRow);
                    Rectangle sourceRect = new Rectangle(spritePosition.X, spritePosition.Y, Game.tileSize, Game.tileSize);
                    Vector2 screenPosition = new Vector2(x * Game.tileSize, y * Game.tileSize);

                    Raylib.DrawTextureRec(spriteAtlas, sourceRect, screenPosition, Raylib.WHITE);
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Item currentItem = items[i];
                Vector2 itemPosition = currentItem.position;
                int itemSpriteIndex = currentItem.spriteIndex;

                Vector2 spritePosition = GetSpritePosition(itemSpriteIndex, imagesPerRow);
                Rectangle sourceRect = new Rectangle(spritePosition.X, spritePosition.Y, Game.tileSize, Game.tileSize);
                Vector2 screenPosition = new Vector2(itemPosition.X * Game.tileSize, itemPosition.Y * Game.tileSize);

                Raylib.DrawTextureRec(spriteAtlas, sourceRect, screenPosition, Raylib.WHITE);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                Vector2 enemyPosition = currentEnemy.position;
                int enemySpriteIndex = currentEnemy.spriteIndex;

                Vector2 spritePosition = GetSpritePosition(enemySpriteIndex, imagesPerRow);
                Rectangle sourceRect = new Rectangle(spritePosition.X, spritePosition.Y, Game.tileSize, Game.tileSize);
                Vector2 screenPosition = new Vector2(enemyPosition.X * Game.tileSize, enemyPosition.Y * Game.tileSize);

                Raylib.DrawTextureRec(spriteAtlas, sourceRect, screenPosition, Raylib.WHITE);
            }
        }


        /// <summary>
        /// Clears the tiles and draws them black
        /// </summary>
        public void ClearTile(int x, int y)
        {
            int drawPixelX = x * Game.tileSize;
            int drawPixelY = y * Game.tileSize;
            Raylib.DrawRectangle(drawPixelX, drawPixelY, Game.tileSize, Game.tileSize, Raylib.BLACK);
        }

        public int getTile(int x, int y)
        {
            MapLayer groundLayer = GetLayer("ground");
            int index = x + y * mapWidth;
            return groundLayer.mapTiles[index];
        }

        public MapLayer GetLayer(string layerName)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].name == layerName)
                {
                    return layers[i];
                }
            }
            Console.WriteLine($"Error: No layer with name: {layerName}");
            return null;
        }

        public string GetItemName(int spriteIndex)
        {
            switch (spriteIndex)
            {
                case 126: return "Bottle"; break;
                case 107: return "Miekka"; break;
                default: return "Unknown"; break;
            }
        }
        public string GetEnemyName(int spriteIndex)
        {
            switch (spriteIndex)
            {
                case 108: return "Ghost"; break;
                case 109: return "Cyclops"; break;
                default: return "Unknown"; break;
            }
        }

        /// <summary>
        /// Loads the items on the map
        /// </summary>
        public void LoadItems()
        {
            MapLayer itemLayer = GetLayer("items");
            int[] itemTiles = itemLayer.mapTiles;
            int layerHeight = itemTiles.Length / mapWidth;

            for (int y = 0; y < layerHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2(x, y);

                    int index = x + y * mapWidth;

                    int tileId = itemTiles[index];

                    if (tileId == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int spriteId = tileId - 1;

                        string name = GetItemName(spriteId);

                        items.Add(new Item(name, position, spriteId));
                    }
                }
            }
        }
        /// <summary>
        /// Loads the enemy 
        /// </summary>
        public void LoadEnemies()
        {
            MapLayer enemyLayer = GetLayer("enemies");
            int[] enemyTiles = enemyLayer.mapTiles;
            int layerHeight = enemyTiles.Length / mapWidth;

            for (int y = 0; y < layerHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2(x, y);

                    int index = x + y * mapWidth;

                    int tileId = enemyTiles[index];

                    if (tileId == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int spriteId = tileId - 1;

                        string name = GetEnemyName(spriteId);

                        enemies.Add(new Enemy(name, position, spriteId));
                    }
                }
            }
        }
        public Enemy GetEnemyAt(int x, int y)
        {
            foreach (Enemy enemy in enemies)
            {
                if ((int)enemy.position.X == x && (int)enemy.position.Y == y)
                {
                    return enemy;
                }
            }
            return null;
        }

        public Item GetItemAt(int x, int y)
        {
            foreach (Item item in items)
            {
                if ((int)item.position.X == x && (int)item.position.Y == y)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
