#define TESTING
using System;
using System.Collections;
using System.Linq;
using System.IO;

namespace DirectoryReader{
    class DirectoryReader{
        enum Type {FILE, FOLDER};
        static void Main(){
            String path = @"C:\";
            Console.WriteLine("Listing: " + path);
            
            String[] dirs = GetDirectories(path);
            String[] files = GetFiles(path);
            Console.WriteLine("-----------DIRECTORIES----------");
            WriteList(dirs, Type.FOLDER);
            Console.WriteLine("-----------FILES----------------");
            WriteList(files, Type.FILE);
        }
        static String[] GetDirectories(String path){
            return Enumerable.ToArray(
                Directory.EnumerateDirectories(path)
            );
        }
        static String[] GetFiles(String path){
            return Enumerable.ToArray(
                Directory.EnumerateFiles(path)
            );
        }
        static void WriteList(String[] list, Type type){
            char c = (
                type == Type.FOLDER ? '\\' : '\0'
            );
            foreach(String s in list){
                int trimPoint = s.LastIndexOf(@"\")+1;
                Console.WriteLine("    " + s.Substring(trimPoint) + c);
            }
        }
    }
}