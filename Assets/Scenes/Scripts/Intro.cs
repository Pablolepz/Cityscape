using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public void startApplication()
    {
        Debug.Log("start button clicked");
        SceneManager.LoadScene("Directories");
    }

    public void quitApplication()
    {
        Debug.Log("quit button clicked");
        Application.Quit();
    }


}
