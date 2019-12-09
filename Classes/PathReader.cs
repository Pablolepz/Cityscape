using System.IO;
using System.Collections.Generic;

public static class PathReader
{
    private static DirectoryInfo directoryInfo;
    public static void SetPath(string path){
        string fullPath= Path.GetFullPath(path);
        directoryInfo = new DirectoryInfo(path);
    }
    public static List<PathItem> GetFiles()
    {
        FileInfo[] files = directoryInfo.GetFiles();
        return GetList(files);
    }
    public static List<PathItem> GetDirectories()
    {
        DirectoryInfo[] directories = GetDirectoryStrings();
        return GetList(directories);
    }
    public static List<PathItem> GetVolumes()
    {
        DriveInfo[] volumes = DriveInfo.GetDrives();
        return GetList(volumes);
    }
    private static List<PathItem> GetList(FileInfo[] list)
    {
        List<PathItem> pathItems = new List<PathItem>();
        foreach (FileInfo item in list)
        {
            pathItems.Add(new PathItem(item));
        }
        return pathItems;
    }
    private static List<PathItem> GetList(DirectoryInfo[] list)
    {
        List<PathItem> pathItems = new List<PathItem>();
        foreach (DirectoryInfo item in list)
        {
            pathItems.Add(new PathItem(item));
        }
        return pathItems;
    }
    private static List<PathItem> GetList(DriveInfo[] list)
    {
        List<PathItem> pathItems = new List<PathItem>();
        foreach (DriveInfo item in list)
        {
            pathItems.Add(new PathItem(item));
        }
        return pathItems;
    }
    private static FileInfo[] GetFileStrings()
    {
        return directoryInfo.GetFiles();
    }
    private static DirectoryInfo[] GetDirectoryStrings()
    {
        return directoryInfo.GetDirectories();
    }
    private static string[] GetVolumeStrings()
    {
        DriveInfo[] driveInfo = GetVolumeInfo();
        return GetDriveStrings(driveInfo);
    }
    private static DriveInfo[] GetVolumeInfo() =>
        DriveInfo.GetDrives();
    private static string[] GetDriveStrings(DriveInfo[] driveInfo)
    {
        string[] volumes = new string[driveInfo.Length];
        for (int i = 0; i < driveInfo.Length; i++)
        {
            volumes[i] = driveInfo[i].ToString();
        }
        return volumes;
    }
}