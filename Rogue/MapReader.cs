using Newtonsoft.Json;
using System;
using System.IO;

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

            Map loadedMap = JsonConvert.DeserializeObject<Map>(fileContents);
            return loadedMap;
        }
    }
}
