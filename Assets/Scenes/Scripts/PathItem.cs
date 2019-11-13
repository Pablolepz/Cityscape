using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

namespace FileSystemReader
{
    class PathItem : MonoBehaviour
    {
        private string _path;
        private string _name;
        private string _extension;
        private ulong _size;
        private const int BASE_UNIT = 10;

        private Type _type;
        private Magnitude _magnitude;

        public PathItem(string path, Type type){
            _path = Path.GetFullPath(path);
            _type = type;
            Initializer handler = GetInitializer();
            handler();
        }

        private delegate void Initializer();

        public enum Type{
            File,
            Directory,
            Volume
        };
        private enum Magnitude{
            Bytes = 0,
            Kilobytes = 3,
            Megabytes = 6,
            Gigabytes = 9,
            Terabytes = 12
        }

        public string FullPath { get => _path; }
        public string Name { get => _name; }
        public string Extension { get => _extension; }
        public ulong ByteSize { get => _size; }
        public string OrderOfMagnitude {
            get => _magnitude.ToString();
        }
        public Type IsType { get => _type; }
        
        private Initializer GetInitializer(){
            switch(_type){
                case Type.Volume: return InitVolume; 
                case Type.Directory: return InitDirectory;
                case Type.File: return InitFile;
                default:
                    throw new ArgumentException(
                        "Invalid Type argument."
                    );
            }
        }
        private void InitVolume(){
            DriveInfo volume = new DriveInfo(_path);
            _name = volume.VolumeLabel;            
            _size = (ulong)volume.AvailableFreeSpace;
            _magnitude = GetMagnitude(_size);
            _extension = ":";
        }
        private void InitDirectory(){
            DirectoryInfo directory = new DirectoryInfo(_path);
            _name = directory.Name;
            FileInfo[] files = directory.GetFiles();
            _size = FileSizeSum(files);
            _magnitude = GetMagnitude(_size);
            // _extension = new string(
            //     Path.DirectorySeparatorChar.ToString()
            // );
        }
        private void InitFile(){
            FileInfo file = new FileInfo(_path);
            _name = file.Name;
            _extension = file.Extension;
            _size = GetFileSize(file);
            _magnitude = GetMagnitude(_size);
        }
        private static ulong GetFileSize(FileInfo fileInfo) => 
            (ulong)fileInfo.Length;
        private static ulong FileSizeSum(FileInfo[] files){
            ulong sum = 0;
            foreach(FileInfo file in files)
                sum += GetFileSize(file);
            return sum;
        }
        private static Magnitude GetMagnitude(ulong n) =>
            n > Math.Pow(BASE_UNIT, 12) ? Magnitude.Terabytes :
            n > Math.Pow(BASE_UNIT, 9) ? Magnitude.Gigabytes :
            n > Math.Pow(BASE_UNIT, 6) ? Magnitude.Megabytes :
            n > Math.Pow(BASE_UNIT, 3) ? Magnitude.Kilobytes :
                Magnitude.Bytes;
        private float ReducedSize() => 
            (float)(
                _size/Math.Pow(10, (int)_magnitude)
            );
        private string GetSizeString() =>
            ReducedSize().ToString("n2") + " " + _magnitude;
        public override string ToString(){
            return _type + ": '" +
                   _name + "' [" + GetSizeString() + "]\n    (" +
                   _path + ")";
        }

        // structs
    }
}