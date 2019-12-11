using System;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;

public class FileSystemReader
{
    private List<PathItem> _files = new List<PathItem>();
    private List<PathItem> _directories = new List<PathItem>();
    private List<PathItem> _volumes = new List<PathItem>();

    public int FileCount { get => _files.Count; }
    public int DirectoryCount { get => _directories.Count; }
    public int VolumeCount { get => _volumes.Count; }
    public List<PathItem> Files { get => _files; }
    public List<PathItem> Directories { get => _directories; }
    public List<PathItem> Volumes { get => _volumes; }
    public List<PathItem> FilesAndDirectories
    {
        get => JoinFilesDirectories();
    }

    public FileSystemReader() { }
    public void RefreshVolumes()
    {
        FetchVolumes();
    }
    private void FetchFiles()
    {
        _files = PathReader.GetFiles();
    }
    private void FetchDirectories()
    {
        _directories = PathReader.GetDirectories();
    }
    private void FetchVolumes()
    {
        try{
            _volumes = PathReader.GetVolumes();
        }catch(IOException){
            _volumes.Clear();
        }
    }
    private List<PathItem> JoinFilesDirectories()
    {
        List<PathItem> allItems = new List<PathItem>();
        allItems.AddRange(_files);
        allItems.AddRange(_directories);
        return allItems;
    }
    public void FetchAll(string path)
    {
        PathReader.SetPath(path);
        FetchFromPath();
        if (_volumes.Count == 0) FetchVolumes();
    }
    public void FetchFromPath()
    {
        try
        {
            FetchFiles();
            FetchDirectories();
        }catch (UnauthorizedAccessException)
        {
            _files.Clear();
            _directories.Clear();
        }catch (ArgumentException)
        {
            _files.Clear();
            _directories.Clear();
        }
    }
    public override string ToString()
    {
        return (
                "[" + _files.Count + "] files, [" +
                _directories.Count + "] directories, [" +
                _volumes.Count + "] volumes."
            );
    }
}
