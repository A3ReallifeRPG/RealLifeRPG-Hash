using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealLifeRPG_Hash
{
    class HashObject
    {
        public int Id;
        private String FullPath;
        public String RelativPath;
        public String Hash;
        public String FileName;
        public long Size;

        public HashObject(String Path)
        {
            this.FullPath = Path;
        }

        public void Run(String TargetFolder, int Id)
        {
            FileInfo Info = new System.IO.FileInfo(this.FullPath);

            this.Id = Id;
            this.FileName = Info.Name;
            this.Size = Info.Length;
            this.Hash = CalculateMD5(this.FullPath);
            this.RelativPath = this.FullPath.Remove(0, FullPath.IndexOf(TargetFolder));
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }
    }
}
