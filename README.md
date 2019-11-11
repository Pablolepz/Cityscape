# Cityscape
File Explorer

A 3D file-system explorer.


**GUI** 



**DirectoryReader**

*DirectoryReader* is in charge of reading the contents in a specified path.
The path may be provided as a String.

+ FileInfo, DirectoryInfo, and DriveInfo support various operations that will become useful down the line (e.g. Length, Name, Extension, etc.)
+ FileInfo and DirectoryInfo make use of FileAttributes class to indicate whether a file is Hidden, Indexed, or a System file. 

Comments:
+ Main() should be removed eventually to make this a standalone class.
+ .NET documentation said Strings containing paths should be well-formed, else they will throw exceptions.
+ Need to add exception handling.

