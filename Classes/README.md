# Documentation

*FileSystemReader* offers an easy way to interface with the underlying **PathReader** and **PathReader** classes.

## Changelog 12/8/19
+ Added exception handling for directories. When an exception is thrown, the FileSystemReader object clears the `_directories` and `_files` lists to be cleared. This might be an issue when rendering the associated objects.
+ Handled exceptions include
  + UnauthorizedAccessException
  + DirectoryNotFoundException
+ Removed Type-dependent PathItem constructors. Specialized constructors may now be used:
  + `public PathItem(FileInfo file);` for files.
  + `public PathItem(DirectoryInfo directory);` for directories.
  + `public PathItem(DriveInfo volume);` for volumes.
+ Specialized constructors made the use of delegates unnecessary.
+ The size of a Type.Volume item is now the difference between its total size and total free space. This should make rendering be based on actual content size.
+ The size of a Type.Directory item is still the nonrecursive sum of its contents if the folder is accessble. Otherwise, the sum of the directory is 0.
  + An alternative is `Ulong.MaxValue`. But this would be problematic since it places the size in millions of Terabytes.
  + This dummy value may be changed using the `DEF_SIZE` value in the PathItem class.
+ In order to improve performance, volumes are now retrieved only if:
  + The _volumes list is empty.
  + The `RefreshVolumes();` method is called.

## ToDo

+ The program throws an *UnauthorizedAccessException* when trying to build a PathItem for a protected folder.
+ The program throws a *DirectoryNotFoundException* when trying to fetch contents in a nonexistent folder.
+ The program should have a way to normalize path strings.
+ The program needs to be tested in OS's other than Windows.

## PathItem

Provides a unified abstration for files, folders, and volumes to make accessing their various properties easier. A PathItem object is of type File, Directory, or Volume. Employs the .NET DriveInfo, DirectoryInfo, and FileInfo classes to retrieve properties of the referenced file system object.

### Constructors

The constructor takes a FileInfo, DirectoryInfo, or DriveInfo item and generated the corresponding PathItem object.

### Enums

+ Type - The file system object this PathItem represents.
+ Magnitude - The order of magnitude of the PathItem's size.

### Properties

+ *FullPath* The full path of the file system object the PathItem refers to.
+ *Name* Depending on the Type of the PathItem:
  + Filename with extension.
  + Directory.
  + Volume Label.
+ *Extension* The extension of a PathItem if it refers to a File. Otherwise:
  + ':' if PathItem refers to a Volume.
  + Environment's directory separator char if PathItem refers to a Directory.
+ *ByteSize* The byte size of the PathItem as a *ulong*.
  + For Volumes, this property returns the available free space.
+ *OrderOfMagnitude* A string of the PathItem's ByteSize expressed at some order of magnitude.
+ *IsType* The Type of the PathItem as defined in **Enums**.

### Methods

+ *ToString()* Provides a string containing the PathItem's properties.

## PathReader

Provides a set of methods to retrieve volumes and contents in given paths.

### Methods

+ Provides internal methods to retrieve a List of PathItems representing Files or Directories contained in the specified path.
+ Provides an internal method to retrieve a List of Volumes attached to the file system.

## FileSystemReader

Provides an easy-to-use wrapper for using the PathReader and PathItem classes. The class makes use of a static directoryInfo item to interact with the FileSystem.
After construction of the FileSystemReader object, the `FetchAll();` method must be called. This method sets the directoryInfo path and populates the PathItem lists.

### Properties

+ *FileCount* A count of fetched files.
+ *DirectoryCount* A count of fetched directories.
+ *VolumeCount* A count of attached volumes.
+ *Files* A List of PathItems representing Files.
+ *Directories* A List of PathItems representing Directories.
+ *Volumes* A List of PathItems representing Volumes.
+ *FilesAndDirectories* A List produced by joining *Files* and *Directories*.

### Constructors

The constructor takes no parameter as it simply initializes the object's members.

### Methods

+ *FetchAll(string path)*
  + Sets directoryInfo to the specified path.
  + Populates internal List containing Volumes.
  + Populates internal List containing Files and Directories in the specified path.

+ *RefreshVolumes()*
  + Repopulates the _volumes.
+ *FetchFromPath(string path)*
  + Repopulates internal Lists with Files and Directories in the specified path.
+ *ToString()* A string containing this FileSystemReader's File, Directory, and Volume counts.
