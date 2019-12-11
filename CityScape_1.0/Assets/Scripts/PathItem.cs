using System;
using System.IO;

public class PathItem
{
    private string _path;
    private string _name;
    private string _extension;
    private ulong _size;
    private const int BASE_UNIT = 10;
    private const int DEF_SIZE = 0;

    private Type _type;
    private Magnitude _magnitude;

    public enum Type
    {
        File,
        Directory,
        Volume
    };
    private enum Magnitude
    {
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
    public string ShortSize { get => GetSizeString(); }
    public string OrderOfMagnitude
    {
        get => _magnitude.ToString();
    }
    public Type IsType { get => _type; }

    public PathItem(FileInfo file)
    {
        _name = file.Name;
        _extension = file.Extension;
        _size = GetFileSize(file);
        _magnitude = GetMagnitude(_size);
        _type = Type.File;
        _path = file.FullName;
    }
    public PathItem(DirectoryInfo directory)
    {
        _name = directory.Name;
        _size = FileSizeSum(directory);
        _magnitude = GetMagnitude(_size);
        _extension = Path.DirectorySeparatorChar.ToString();
        _type = Type.Directory;
        _path = directory.FullName;
    }
    public PathItem(DriveInfo volume)
    {
        _name = volume.Name;
        _size = (
            (ulong)volume.TotalSize - (ulong)volume.TotalFreeSpace
        );
        _magnitude = GetMagnitude(_size);
        _extension = ":";
        _type = Type.Volume;
        _path = volume.VolumeLabel;
    }
    private static ulong GetFileSize(FileInfo fileInfo) =>
        (ulong)fileInfo.Length;
    private static ulong FileSizeSum(DirectoryInfo directory)
    {
        ulong sum = 0;
        try
        {
            foreach (FileInfo file in directory.GetFiles())
                sum += GetFileSize(file);
        }
        catch (UnauthorizedAccessException)
        {
            return DEF_SIZE;
        }
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
            _size / Math.Pow(10, (int)_magnitude)
        );
    private string GetSizeString() =>
        ReducedSize().ToString("n2") + " " + _magnitude;
    public override string ToString()
    {
        return _type + ": '" +
                _name + "' [" + GetSizeString() + "]\n    (" +
                _path + ")";
    }
}
