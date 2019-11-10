# Documentation

*FileSystemReader* offers an easy way to interface with the underlying **PathReader** and **PathReader** classes.

## ToDo

+ The program throws an *UnauthorizedAccessException* when trying to build a PathItem for a protected folder.
+ The program throws a *DirectoryNotFoundException* when trying to fetch contents in a nonexistent folder.
+ The program should have a way to normalize path strings.
+ The program needs to be tested in OS's other than Windows.

## PathItem

Provides a unified abstration for files, folders, and volumes to make accessing their various properties easier. A PathItem object is of type File, Directory, or Volume. Employs the .NET DriveInfo, DirectoryInfo, and FileInfo classes to retrieve properties of the referenced file system object.

### Constructors

The constructor takes a *string* parameter indicating which item in the file system this PathItem should represent; and a *Type* parameter to initialize the fields with the appropriate information.

### Delegates

The *Initializer()* delegate is set by *GetInitializer()* and is intended to call the initializer that is appropriate to the PathItem's type.

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

Provides a set of methods to retrieve volumes and contents in given paths. Employs the .NET Directory class for retrieving Directory contents.

### Methods

+ Provides internal methods to retrieve a List of PathItems representing Files or Directories contained in the specified path.
+ Provides an internal method to retrieve a List of Volumes attached to the file system.

## FileSystemReader

Provides an easy-to-use wrapper for using the PathReader and PathItem classes.

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
  + Initializes internal List containing Volumes.
  + Initializes internal List containing Files and Directories in the specified path.

+ *FetchFromPath(string path*)
  + Repopulates internal Lists with Files and Directories in the specified path.

+ *ToString()* A string containing this FileSystemReader's File, Directory, and Volume counts.
