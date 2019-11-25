using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    FileSystemReader fsReader = new FileSystemReader();
    public void startApplication()
    {
        Debug.Log("start button clicked");
        // goes to main scene
        SceneManager.LoadScene("Directories");

        // test for andrew's system.

        // get items from directory
        // fsReader.FetchAll("/Users/andrew/Documents/GitHub/Cityscape");
        // List<PathItem> filesAndFolderNames = fsReader.FilesAndDirectories;
        // foreach(PathItem item in filesAndFolderNames) Debug.Log(item);
        // Debug.Log("count of files and folders "+filesAndFolderNames.Count);

    }

    public void quitApplication()
    {
        Debug.Log("quit button clicked");
        Application.Quit();
    }


}
