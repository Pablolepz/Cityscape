using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
  public static float GlobalUnit = 0.5f;
  public static float platformUnit = 0.5f;  
  //FileSystemReader fsReader = new FileSystemReader();
  //This array is sudo for the array of objects given
  // public static GameObject[,] city = new GameObject[5, 5];
  
  //testestest
  FileSystemReader fsr = new FileSystemReader();

  // public Text txtSelectedDirectory;

  //testestest
  public static float GlobalSize(float input)
  {
    return input * GlobalUnit;
  }
  // Start is called before the first frame update
  void Start()
  {
    //testsetest

    // txtSelectedDirectory.text = "";
    // int a = 0;
    // int b = 0;

    // start at this directory
    fsr.FetchAll("/Users/andrew/Documents/GitHub/Cityscape");
    // retrieve current directory 
    List<PathItem> fileAndFolderNames = fsr.FilesAndDirectories;
    int citySize = Mathf.CeilToInt(Mathf.Sqrt(fileAndFolderNames.Count));
    GameObject[,] city = new GameObject[citySize, citySize];
    // foreach(PathItem item in fileAndFolderNames){
    //   Debug.Log(item);
    // }
    // Debug.Log("# of files and folders: " + fileAndFolderNames.Count);
    
    //testestetst

    int inputArrSize = city.GetLength(0) * city.GetLength(1);
    int x = 1;
    while(x*x < inputArrSize)
    {
      x++;
    }
    // make platform for city
    var platform = Object.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
    platform.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.2f), GlobalSize(x));
    platform.transform.position = new Vector3(GlobalSize(x / 2 + 0.5f), GlobalSize(0.1f), GlobalSize(x / 2 + 0.5f));
    platform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

    // create a building for each for each drive

    //test

    // foreach(var drive in DriveInfo.GetDrives()){
    //   // display in console
    //   Debug.Log("drive name: " + drive.Name + "root directory: " + drive.RootDirectory);

    //   // create cube game object
    //   var citygObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //   // position these game objects
    //   citygObject.transform.localScale = new Vector3(GlobalUnit * 0.7f,GlobalSize(Random.Range(1,5)),GlobalUnit * 0.7f);;
    //   citygObject.transform.position = new Vector3(GlobalSize(a + 0.5f), (((citygObject.transform.localScale.y) / 2) + ((platform.transform.localScale.y) / 2)),GlobalSize(b + 0.5f));

    //   citygObject.name = drive.Name;
    //   citygObject.AddComponent<PathItem>();
    //   PathItem pitem = citygObject.GetComponent<PathItem>();
    //   pitem.Name = drive.Name;
    //   pitem.FullPath = drive.RootDirectory.FullName;


    //   a++;
    //   b++;
    // }// end foreach drive

    //test

    for (int a = 0; a < city.GetLength(0); a++){
      for(int b = 0; b < city.GetLength(1); b++){
        city[a,b] = Object.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));;
        city[a,b].transform.localScale = new Vector3(GlobalUnit * 0.7f,GlobalSize(Random.Range(1,5)),GlobalUnit * 0.7f);
        city[a,b].transform.position = new Vector3(GlobalSize(a + 0.5f),(((city[a,b].transform.localScale.y)/2) + ((platform.transform.localScale.y)/2)),GlobalSize(b + 0.5f));
      }
    }
  }



  RaycastHit hitInfo = new RaycastHit();
  void Update()
  {
    // bool HoverHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
    // if(HoverHit){
    //   if(hitInfo.transform.GetComponent<PathItem>() != null){
    //     PathItem pitem = hitInfo.transform.GetComponent<PathItem>();
    //     txtSelectedDirectory.text = $"{pitem.FullPath}";
    //   }
    //   else{
    //     txtSelectedDirectory.text = $"";
      // }
    // }
  }
}