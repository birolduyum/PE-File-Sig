// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

namespace IsExe
{
    class Program
    {
        public static bool IsExe(string path)
        {
            if (File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        var bytes = br.ReadBytes(2);
                        if (bytes[0] == 'M' && bytes[1] == 'Z')
                        {
                            fs.Seek(60, SeekOrigin.Begin);
                            var e_lfanew = br.ReadInt32();
                            fs.Seek(e_lfanew, SeekOrigin.Begin);
                            var bytes2 = br.ReadBytes(4);
                            if (bytes2[0] == 'P' && bytes2[1] == 'E' && bytes2[2] == 0 && bytes2[3] == 0)
                            {
                                return true;
                            }
                        }  
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            string fileName = @"C:\Windows\System32\cmd.exe"; //exe file  
            
            if (IsExe(fileName))
            {
                Console.WriteLine("PE file");
            }
            else
            {
                Console.WriteLine("Not PE file");
            }
        }
    }
}
