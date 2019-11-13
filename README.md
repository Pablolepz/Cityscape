# Cityscape Documentation
File Explorer

A 3D file-system explorer.

**FileSystemReader**
contains classes to represent objects in the file system.
  + The file needs to interface with a sorting class that can be called from a FileSystemReader object.
  + It may be useful to write an Interface to be implemented by a sorting class.
  + The sorting class needs to have a SortingRules enum providing enums for sorting types: Alphanumeric, Size, Type, and anything else that can be useful.
  

**GUI** 
TODO
make it prettier
+ needs a list of all folders/files there are in the current directory
    + find out how to retrieve names of said folders/files