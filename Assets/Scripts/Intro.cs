using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // FileSystemReader fsReader = new FileSystemReader();
    public void startApplication()
    {
        Debug.Log("start button clicked");
        // goes to main scene
        SceneManager.LoadScene("Directories");

        // fsReader.FetchFromPath("/Users/andrew/Documents/GitHub/Cityscape");
        // List<PathItem> filesAndFolderNames = fsReader.FilesAndDirectories;
        // foreach(PathItem item in filesAndFolderNames) Debug.Log(item);
        // DriveInfo[] allDrives = DriveInfo.GetDrives();
        // Debug.Log("root dir: " + allDrives[1]); 
        // foreach(var d in allDrives) Debug.Log(d.Name);

    }

    public void quitApplication()
    {
        Debug.Log("quit button clicked");
        Application.Quit();
    }


}
