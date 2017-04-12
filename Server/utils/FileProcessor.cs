// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;
using System.IO;

public class FileProcessor
{
    // Process all files in the directory passed in, recurse on any directories 
    // that are found, and process the files they contain.
    public static String[] GetFileNamesDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        return Directory.GetFiles(targetDirectory);
    }
}