using Newtonsoft.Json;
using System;
using System.IO;
using TurboMapReader;

namespace Rogue
{
    internal class MapReader
    {
        public void ReadMapFromFileTest(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                Console.WriteLine("File contents:");
                Console.WriteLine();

                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break; // Reached end of file
                    }
                    Console.WriteLine(line);
                }
            }
        }

        public Map LoadMapFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} not found");
                return null;
            }

            string fileContents;
            using (StreamReader reader = File.OpenText(fileName))
            {
                fileContents = reader.ReadToEnd(); // Read entire file
            }

            TurboMapReader.TiledMap mapMadeInTiled = TurboMapReader.MapReader.LoadMapFromFile(fileName);
            Map loadedMap = ConvertTiledMapToMap(mapMadeInTiled); // Convert external map to game map
            return loadedMap;
        }

        public Map ConvertTiledMapToMap(TiledMap turboMap)
        {
            Map rogueMap = new Map();

            TurboMapReader.MapLayer groundLayer = turboMap.GetLayerByName("ground");

            int groundWidth = groundLayer.width;
            rogueMap.mapWidth = groundWidth;

            int howManyTiles = groundLayer.data.Length;
            int[] groundTiles = groundLayer.data;

            MapLayer myGroundLayer = new MapLayer(howManyTiles);
            myGroundLayer.name = "ground";
            myGroundLayer.mapTiles = groundTiles;

            rogueMap.layers[0] = myGroundLayer; // Assign ground layer to index 0

            TurboMapReader.MapLayer enemyLayer = turboMap.GetLayerByName("enemies");
            howManyTiles = enemyLayer.data.Length;
            int[] enemyTiles = enemyLayer.data;
            MapLayer myEnemyLayer = new MapLayer(howManyTiles);
            myEnemyLayer.name = "enemies";
            myEnemyLayer.mapTiles = enemyTiles;

            rogueMap.layers[1] = myEnemyLayer; // Assign enemy layer to index 1

            TurboMapReader.MapLayer itemLayer = turboMap.GetLayerByName("items");
            howManyTiles = itemLayer.data.Length;
            int[] itemTiles = itemLayer.data;
            MapLayer myItemLayer = new MapLayer(howManyTiles);
            myItemLayer.name = "items";
            myItemLayer.mapTiles = itemTiles;

            rogueMap.layers[2] = myItemLayer; // Assign item layer to index 2

            return rogueMap; // Return the fully built map
        }
    }
}
