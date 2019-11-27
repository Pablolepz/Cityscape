using System.IO;
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
    public List<PathItem> FilesAndDirectories {
        get => JoinFilesDirectories();
    }

    public FileSystemReader() { }

    private List<PathItem> FetchFiles(string path)
    {
        _files = PathReader.GetFiles(path);
        return _files;
    }
    private List<PathItem> FetchDirectories(string path)
    {
        _directories = PathReader.GetDirectories(path);
        return _directories;
    }
    private List<PathItem> FetchVolumes(string path)
    {
        _volumes = PathReader.GetVolumes(path);
        return _volumes;
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
        FetchFiles(path);
        FetchDirectories(path);
        FetchVolumes(path);
    }
    public void FetchFromPath(string path)
    {
        FetchFiles(path);
        FetchDirectories(path);
    }
    public override string ToString()
    {
        return (
                "[" + _files.Count +"] files, [" +
                _directories.Count + "] directories, [" +
                _volumes.Count + "] volumes."
            );
    }
    // public List<PathItem> Sort(SortRule rule){
    //     return null;
    // }
}