using System;
using System.IO;
using System.Collections.Generic;

namespace FileSystemReader
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemReader fsReader = new FileSystemReader();
            fsReader.FetchAll(Directory.GetCurrentDirectory());
            Console.WriteLine(fsReader);

            List<PathItem> items = fsReader.Directories;
            foreach(PathItem item in items) Console.WriteLine(item);
        }
    }
}
