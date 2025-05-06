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
                        break; // End of file
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
                fileContents = reader.ReadToEnd();
            }

            TurboMapReader.TiledMap mapMadeInTiled = TurboMapReader.MapReader.LoadMapFromFile(fileName);
            Map loadedMap = ConvertTiledMapToMap(mapMadeInTiled);
            return loadedMap;
        }
        public Map ConvertTiledMapToMap(TiledMap turboMap)
        {
            // Luo tyhjä kenttä
            Map rogueMap = new Map();

            // Muunna tason "ground" tiedot
            TurboMapReader.MapLayer groundLayer = turboMap.GetLayerByName("ground");

            // TODO: Lue kentän leveys. Kaikilla TurboMapReader.MapLayer olioilla on sama leveys
            int groundWidth = groundLayer.width;
            rogueMap.mapWidth = groundWidth;

            // Kuinka monta kenttäpalaa tässä tasossa on?
            int howManyTiles = groundLayer.data.Length;
            // Taulukko jossa palat ovat
            int[] groundTiles = groundLayer.data;

            // Luo uusi taso tietojen perusteella
            MapLayer myGroundLayer = new MapLayer(howManyTiles);
            myGroundLayer.name = "ground";
            myGroundLayer.mapTiles = groundTiles;




            // TODO: lue tason palat



            // Tallenna taso kenttään
            rogueMap.layers[0] = myGroundLayer;

            TurboMapReader.MapLayer enemyLayer = turboMap.GetLayerByName("enemies");
            howManyTiles = enemyLayer.data.Length;
            int[] enemyTiles = enemyLayer.data;
            MapLayer myEnemyLayer = new MapLayer(howManyTiles);
            myEnemyLayer.name = "enemies";
            myEnemyLayer.mapTiles = enemyTiles;
            rogueMap.layers[1] = myEnemyLayer;

            TurboMapReader.MapLayer itemLayer = turboMap.GetLayerByName("items");
            howManyTiles = itemLayer.data.Length;
            int[] itemTiles = itemLayer.data;
            MapLayer myItemLayer = new MapLayer(howManyTiles);
            myItemLayer.name = "items";
            myItemLayer.mapTiles = itemTiles;
            rogueMap.layers[2] = myItemLayer;


            // Lopulta palauta kenttä
            return rogueMap;
        }
    }
}
