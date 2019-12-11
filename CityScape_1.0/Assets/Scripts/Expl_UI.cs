using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expl_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txtCurrentDirectory;
    // void Start()
    // {
    // }
    public void updateUI(string curr_path)
    {
      txtCurrentDirectory.text = curr_path;
    }
    // Update is called once per frame
    // void Update()
    // {
    //
    // }
}
