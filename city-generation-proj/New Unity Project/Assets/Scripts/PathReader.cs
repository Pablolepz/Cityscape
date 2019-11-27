using System;
using System.IO;
using System.Collections.Generic;
// using UnityEngine;

public static class PathReader
{
    public static List<PathItem> GetFiles(string path){
        string[] files = GetFileStrings(path);
        return GetPathItemList(files, PathItem.Type.File);
    }
    public static List<PathItem> GetDirectories(string path){
        // try{
          string[] directories = GetDirectoryStrings(path);
          return GetPathItemList(
              directories, PathItem.Type.Directory
          );
        // }
        // catch (UnauthorizedAccessException)
        // {
        //   Debug.Log("File was unauthorized");
        //   List<PathItem> tmp = new List<PathItem>();
        //   return tmp;
        // }
    }
    public static List<PathItem> GetVolumes(string path){
        string[] volumes = GetVolumeStrings();
        return GetPathItemList(volumes, PathItem.Type.Volume);
    }
    private static List<PathItem> GetPathItemList(
        string[] paths, PathItem.Type type
    ){
        List<PathItem> pathItems = new List<PathItem>();
        foreach(string path in paths){
            pathItems.Add(new PathItem(path, type));
        }
        return pathItems;
    }
    private static string[] GetFileStrings(string path) =>
        Directory.GetFiles(path);
    private static string[] GetDirectoryStrings(string path) =>
        Directory.GetDirectories(path);
    private static string[] GetVolumeStrings(){
        DriveInfo[] driveInfo = GetVolumeInfo();
        return GetDriveStrings(driveInfo);
    }
    private static DriveInfo[] GetVolumeInfo() =>
        DriveInfo.GetDrives();
    private static string[] GetDriveStrings(DriveInfo[] driveInfo){
        string[] volumes = new string[driveInfo.Length];
        for(int i = 0; i < driveInfo.Length; i++){
            volumes[i] = driveInfo[i].ToString();
        }
        return volumes;
    }
}
