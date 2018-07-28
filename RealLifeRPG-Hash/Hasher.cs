using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealLifeRPG_Hash
{
    class Hasher
    {
        String TargetPath;
        String TargetFolder;
        String OutPath;
        HashObject[] TargetArray;

        static void Main(string[] args)
        {
            Hasher Instance = new Hasher();
            
            if (args.Length > 0)
            {
                Instance.TargetPath = args[0];
            }
            else
            {
                Console.WriteLine("Arg 1 has to be Input Path");
                Console.ReadLine();
                System.Environment.Exit(1);
            }

            Console.WriteLine("Target: " + Instance.TargetPath);
            
            if (Directory.Exists(Instance.TargetPath))
            {
                Instance.TargetFolder = new DirectoryInfo(Instance.TargetPath).Name;

                if (args.Length > 1)
                {
                    Instance.OutPath = args[1];
                }
                else
                {
                    Instance.OutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + Instance.TargetFolder + " - " + DateTime.Now.ToString().Replace(':', '-') + ".json";
                }

                Console.WriteLine("Output: " + Instance.TargetPath);

                String json = Instance.HashFolder();

                File.WriteAllText(Instance.OutPath, json);
            }
            else
            {
                Console.WriteLine(Instance.TargetPath + " has to be a directory");
                Console.ReadLine();
                System.Environment.Exit(1);
            }
        }

        public String HashFolder()
        {
            String[] fileEntries = Directory.GetFiles(this.TargetPath, "*.*", SearchOption.AllDirectories);
            this.TargetArray = new HashObject[fileEntries.Length];
            int num = 0;
            foreach (string fileName in fileEntries)
            {
                Console.WriteLine(num + "/" + (fileEntries.Length - 1) + " - " + fileName);
                HashObject Object = new HashObject(fileName);
                Object.Run(this.TargetFolder, num);
                TargetArray[num] = Object;
                num++;
            }

            return JsonConvert.SerializeObject(TargetArray);
        }
    }
}
