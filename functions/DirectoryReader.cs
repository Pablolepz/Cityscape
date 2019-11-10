#define TESTING
using System;
using System.Collections;
using System.Linq;
using System.IO;

namespace DirectoryReader{
    class DirectoryReader{
        static void Main(){
            Console.WriteLine("----------DRIVES----------");
            DriveInfo[] drives = GetVolumes();
            WriteList(drives);
            String path = drives[0].RootDirectory.ToString();
            
            Console.WriteLine("----------(C:)------------");
            Console.WriteLine("Listing: " + path);
            DirectoryInfo[] dirs = GetDirectories(path);
            FileInfo[] files = GetFiles(path);
            Console.WriteLine("----------DIRECTORIES-----");
            WriteList(dirs);
            Console.WriteLine("----------FILES-----------");
            WriteList(files);

            
        }
        static DriveInfo[] GetVolumes(){
            DriveInfo[] drives = DriveInfo.GetDrives();
            return drives;
        }
        static DirectoryInfo[] GetDirectories(String path){
            var list = Directory.EnumerateDirectories(path);
            IEnumerator e = list.GetEnumerator();
            DirectoryInfo[] directories = new DirectoryInfo[list.Count()];
            for(int i = 0; e.MoveNext(); i++){
                directories[i] = new DirectoryInfo(e.Current.ToString());
            }
            return directories;
        }
        static FileInfo[] GetFiles(String path){
            var list = Directory.EnumerateFiles(path);
            IEnumerator e = list.GetEnumerator();
            FileInfo[] files = new FileInfo[list.Count()];
            for(int i = 0; e.MoveNext(); i++){
                files[i] = new FileInfo(e.Current.ToString());
            }
            return files;
        }
        static void WriteList(DirectoryInfo[] list){
            foreach(DirectoryInfo dir in list){
                if(!(IsSystem(dir.Attributes) || IsHidden(dir.Attributes)))
                    Console.WriteLine("    " + dir.Name + @"\");
            }
        }
        static void WriteList(FileInfo[] list){
            foreach(FileInfo file in list){
                if(!IsSystem(file.Attributes))
                    Console.WriteLine("    " + file.Name);
            }
        }
        static void WriteList(DriveInfo[] list){
            foreach(DriveInfo d in list){
                Console.WriteLine(
                    d.ToString() + "    ("
                        + PercentageUsed(d).ToString("###.##")
                        + "% used)    "
                        + d.DriveType
                    );
            }
        }
        static bool IsHidden(FileAttributes attributes){
            return attributes.HasFlag(FileAttributes.Hidden);
        }
        static bool IsSystem(FileAttributes attributes){
            return attributes.HasFlag(FileAttributes.System);
        }
        static double PercentageUsed(DriveInfo drive){
            return (((double)drive.AvailableFreeSpace/drive.TotalSize)*100);
        }
    }
}