using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
//D:\Documents\College

public class main : MonoBehaviour
{
  public Text txtCurrentDirectory;
  // Mouse Variables ===================
  float TimeT = 0;
  // float maxTimeBetweenClicks = 0.5f; // half a second
//     // ========================


  private static FileSystemReader file_rtvr = new FileSystemReader();
  private static string pathForCity;
  private static city_class curr_city = new city_class();
  public static float GlobalUnit = 1f;
  public static float GlobalNormalizer_orig = 6f;
  public static float GlobalNormalizer = 6f;
  public static bool focused = false;
  // camera rotation variables ==========================
  // public static GameObject cameraOrbit;
  public static float defViewAng = 45.0f;
  public float rotateSpeed = 8f;
  public Color tempColor;
  public static GameObject mainPlatform;
  public GameObject camRotPoint;
  // Mouse Variables ===================
  private float lastTimeClicked;

  public static float GlobalSize(float input)
  {
    return input * GlobalUnit;
  }
  public static float Norm(float input){
    return input * GlobalNormalizer;
  }
  public void updateUI(string curr_path)
  {
    Debug.Log("OLD Current path: " + curr_path);

    int slashA = 0;
    int slashB = 0;

    //if in windows api
    curr_path = curr_path.Replace("\\\\", "\\");
    // curr_path = "D:\\1234567890\\1234567890\\1234567890\\1234567890\\test";
    int k = 0;
    Debug.Log("path length = " + curr_path.Length);
    while(k < curr_path.Length)
    {
      // Debug.Log("Current char: " + curr_path[k] + "Current k: " + k);
      if (curr_path[k] == '\\')
      {
        if (slashA == 0)
        {
          slashA = k;
        }
        else
        {
          if (slashB == 0)
          {
            slashB = k;
          }
          else
          {
            slashA = slashB;
            slashB = k;
          }
        }
      }
      k++;
    }
    if (curr_path.Length > 40)
    {
      curr_path = curr_path.Substring(0, 3) + "..." + curr_path.Substring(slashA);
    }
    txtCurrentDirectory.text = "Current path: " + Path.GetFullPath(curr_path);
    Debug.Log("Current path: " + curr_path);
  }

  public static city_class initCity(string input, bool starter = false)
  {
    //to find parent and make path conform to windows path api (only accepts one slash at the moment)
    Debug.Log("GEEZ RICK: " + input);
    int par_path_index = 0;
    int slashA = 0;
    int slashB = 0;
    int slashCount = 0;
    int k = 0;
    bool winSytax = false;
    string par_path = "_";
    if (starter)
    {
      input = input.Replace("\\", "\\\\");
    }
    if (input[input.Length - 1] == ':')
    {
      Debug.Log(input);
      Debug.Log("this is a Disk!");
      input = input + @"\";
      Debug.Log(input);
      par_path = input;
    }
    else
    {
      slashA = input.LastIndexOf(@"\") - 1;
      Debug.Log(slashA);
      par_path = input.Substring(0,slashA);
    }
    Debug.Log("parent_path = " + par_path + " slashCount = " + slashCount);
    Debug.Log("new input = " + input);
    city_class new_city = new city_class();
    file_rtvr.FetchAll(input);

    //prepare platform/host building
    new_city.par_bldng = new building_class();
    new_city.par_bldng.obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
    // new_city.par_bldng.obj.tag = "par_bld";
    Debug.Log("ADDING PATH: " + par_path +  "ADDING INPUT: " + input);
    if (par_path != "_")
    {
      new_city.par_bldng.obj.name = input;
      new_city.par_bldng.path = par_path;
    }
    else
    {
      Debug.Log("AYO LOOK HERE: " + input + "parentpath is: " + par_path);
      // input = input + "\\\\"; //TODO when going backwards, the drive is saved as D: and not D:\\ therefore, when the retriever searches for "D:\X", it probably wont find X.
      new_city.par_bldng.obj.name = input;
      new_city.par_bldng.path = input;
    }
    new_city.par_bldng.obj.AddComponent<build_prop>().parent_city = new_city;
    new_city.par_bldng.city_of = par_path;

    //finding city size =================================================
    int city_list_size = file_rtvr.FilesAndDirectories.Count;
    float x = 1f;
    while(x*x < city_list_size)
    {
      x = x + 1f;
    }
    new_city.par_bldng.obj.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.29f), GlobalSize(x));
    new_city.par_bldng.obj.transform.position = new Vector3(GlobalSize(x / 2), -GlobalSize(0.1f), GlobalSize(x / 2));
    new_city.par_bldng.obj.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    // new_city.par_bldng.obj.AddComponent<build_prop>().child_city = new_city;
    new_city.par_bldng.obj.tag = "Base";
    new_city.par_bldng.obj.AddComponent<build_prop>();
    new_city.par_bldng.obj.GetComponent<build_prop>().fileName = input;
    new_city.par_bldng.obj.GetComponent<build_prop>().parent_class = new_city.par_bldng;


    //================City Creation======================================
    new_city.building_list = new List<building_class>();
    //finding max size
    float max = 0.0f;
    for(int i = 0; i < file_rtvr.FilesAndDirectories.Count; i++)
    {
      if ((float)file_rtvr.FilesAndDirectories[i].ByteSize > max)
      {
        max = (float)file_rtvr.FilesAndDirectories[i].ByteSize;
      }
      // Debug.Log(file_rtvr.FilesAndDirectories[i].ToString());
    }

    GlobalNormalizer = GlobalNormalizer_orig/max;
    // Debug.Log("GlobalNormalizer: " + GlobalNormalizer);
    int a = 0;
    int b = 0;
    for (int i = 0; i < file_rtvr.FilesAndDirectories.Count; i++)
    {
      new_city.building_list.Add(new building_class());
      new_city.building_list[i].path = input + "\\\\" + file_rtvr.FilesAndDirectories[i].Name;
      new_city.building_list[i].item = file_rtvr.FilesAndDirectories[i];

      //different initializations depending on file type
      if (file_rtvr.FilesAndDirectories[i].IsType == PathItem.Type.File)
      {
        new_city.building_list[i].obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        new_city.building_list[i].obj.tag="File";
        new_city.building_list[i].obj.name = file_rtvr.FilesAndDirectories[i].Name;
        new_city.building_list[i].obj.GetComponent<Renderer>().material.color = Color.grey;
        new_city.building_list[i].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, Norm(GlobalSize((float)new_city.building_list[i].item.ByteSize))/2, GlobalUnit * 0.7f);
        new_city.building_list[i].obj.transform.position = new Vector3(GlobalSize(a + 0.5f),(((new_city.building_list[i].obj.transform.localScale.y)) + ((GlobalSize(0.1f))/2)),GlobalSize(b + 0.5f));
      }
      else if (file_rtvr.FilesAndDirectories[i].IsType == PathItem.Type.Directory)
      {
        new_city.building_list[i].obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_city.building_list[i].obj.tag="Directory";
        new_city.building_list[i].obj.name = file_rtvr.FilesAndDirectories[i].Name;
        // new_city.building_list[i].obj.GetComponent<Renderer>().material.color = Color.white;
        if (Norm(GlobalSize((float)new_city.building_list[i].item.ByteSize))/2 < 0.1)
        {
            new_city.building_list[i].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, 0.1f, GlobalUnit * 0.7f);
        }
        else
        {
            new_city.building_list[i].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, Norm(GlobalSize((float)new_city.building_list[i].item.ByteSize))/2, GlobalUnit * 0.7f);
        }
        // new_city.building_list[i].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, Norm(GlobalSize((float)new_city.building_list[i].item.ByteSize))/2, GlobalUnit * 0.7f);
        // Debug.Log(new_city.building_list[i].obj.transform.localScale.y);
        new_city.building_list[i].obj.transform.position = new Vector3(GlobalSize(a + 0.5f),(((new_city.building_list[i].obj.transform.localScale.y)/2) + ((GlobalSize(0.1f))/2)),GlobalSize(b + 0.5f));
      }
      new_city.building_list[i].obj.AddComponent<build_prop>();
      new_city.building_list[i].obj.GetComponent<build_prop>().fileName = file_rtvr.FilesAndDirectories[i].Name;
      new_city.building_list[i].obj.GetComponent<build_prop>().parent_class = new_city.building_list[i];
      new_city.building_list[i].obj.GetComponent<build_prop>().size = (float)file_rtvr.FilesAndDirectories[i].ByteSize;

      //city iterator
      b++;
      if (b >= x)
      {
        a++;
        b = 0;
      }
    }
    Debug.Log("New city made");
    return new_city;
  }

  public static void clearCity(GameObject clickedBuilding)
  {
    foreach (building_class i in curr_city.building_list)
    {
      // Debug.Log(i.obj);
      if (i.obj.tag != "foc_bldng")
      {
        {
          i.obj.GetComponent<MeshRenderer>().enabled = false;
          i.obj.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
      }
    }
    return;
  }
  public static void zoom_to_building(GameObject new_plat)
  {
    // runZoomAnimation();
    GameObject newPlatform = new_plat;
    // clearCity(new_plat);

    // Debug.Log("Going to "+ newPlatform.GetComponent<build_prop>().parent_class.path + "=============================");

    city_class tmpCity = initCity(newPlatform.GetComponent<build_prop>().parent_class.path);
    //Destroy previous city objects n stuff
    foreach (building_class i in curr_city.building_list)
    {
      if (i.obj.tag != "Base")
      {
        // print("Destroying " + i.obj.tag);
        Destroy(i.obj);
      }
      else
      {
        i.obj.GetComponent<MeshRenderer>().enabled = true;
        i.obj.layer = LayerMask.NameToLayer("Default");
      }
    }
    Destroy(curr_city.par_bldng.obj);
    // Destroy(newPlatform.GetComponent<build_prop>().parent_class.city_of.par_bldng);
    curr_city = tmpCity;

    // returnCity(newPlatform);
    return;
  }
  public static void zoom_out_building(string input)
  {
    string par_city_path = input;
    // city_class tmpCity = initCity(par_city_path);
    foreach (building_class i in curr_city.building_list)
    {
      if (i.obj.tag != "Base")
      {
        // print("Destroying " + i.obj.tag);
        Destroy(i.obj);
      }
      else
      {
        i.obj.GetComponent<MeshRenderer>().enabled = true;
        i.obj.layer = LayerMask.NameToLayer("Default");
      }
    }
    Destroy(curr_city.par_bldng.obj);
    // Destroy(newPlatform.GetComponent<build_prop>().parent_class.city_of.par_bldng);
    // curr_city = tmpCity;
    curr_city = initCity(par_city_path);

    // returnCity(newPlatform);
    return;
  }
  public static void returnCity(GameObject clickedBase)
  {
    foreach (building_class i in curr_city.building_list)
    {
  //   // curr_city = clickedBase.GetComponent<build_prop>().obj.parent_city;
  //   for (int a = 0; a < curr_city.building_list.GetLength(0); a++){
  //     for(int b = 0; b < curr_city.building_list.GetLength(1); b++){
      if (i.obj.tag == "foc_bldng")
      {
        if (i.obj.GetComponent<build_prop>().child_city == null)
        {
          i.obj.tag = "File";
        }
        else
        {
          i.obj.tag = "Directory";
        }
      }
      i.obj.GetComponent<MeshRenderer>().enabled = true;
      i.obj.layer = LayerMask.NameToLayer("Default");
  //
    }
  }
  public void sortCityBasedOnName(){
    for (int i = curr_city.building_list.Count-1; i >=0; i--)
    {
      //Debug.Log("For i = " + i);
      //Debug.Log(curr_city.building_list[i].obj.GetComponent<build_prop>().fileName);
      for (int j = 0; j < i; j++)
      {
          if(1 == string.Compare( curr_city.building_list[j].obj.GetComponent<build_prop>().fileName,curr_city.building_list[j+1].obj.GetComponent<build_prop>().fileName)){
            //Debug.Log("max found : "+ min.obj.GetComponent<build_prop>().fileName+" < "+ curr_city.building_list[j].obj.GetComponent<build_prop>().fileName);
            building_class temp = new building_class();
            temp = curr_city.building_list[j+1];
            curr_city.building_list[j+1] =curr_city.building_list[j];
            curr_city.building_list[j] = temp;
          }
      }
    }

    Debug.Log("=========================================================================");
    Debug.Log(curr_city.building_list.Count);
    for (int i = 0; i < curr_city.building_list.Count; i++)
    {
      Debug.Log(curr_city.building_list[i].obj.GetComponent<build_prop>().fileName);
    }

  }

  public void sortCityBasedOnSize(){
    for (int i = curr_city.building_list.Count-1; i >=0; i--)
    {
      //Debug.Log("For i = " + i);
      //Debug.Log(curr_city.building_list[i].obj.GetComponent<build_prop>().fileName);
      for (int j = 0; j < i; j++)
      {
          if(curr_city.building_list[j].obj.GetComponent<build_prop>().size > curr_city.building_list[j+1].obj.GetComponent<build_prop>().size){
            //Debug.Log("max found : "+ min.obj.GetComponent<build_prop>().fileName+" < "+ curr_city.building_list[j].obj.GetComponent<build_prop>().fileName);
            building_class temp = new building_class();
            temp = curr_city.building_list[j+1];
            curr_city.building_list[j+1] =curr_city.building_list[j];
            curr_city.building_list[j] = temp;
          }
      }
    }

    Debug.Log("=========================================================================");
    Debug.Log(curr_city.building_list.Count);
    for (int i = 0; i < curr_city.building_list.Count; i++)
    {
      Debug.Log(curr_city.building_list[i].obj.GetComponent<build_prop>().fileName);
    }

  }






  void Start()
  {
    pathForCity = Intro.userDirInput;
    Debug.Log("We're in! " + pathForCity);
    updateUI(pathForCity);
    curr_city = initCity(pathForCity, true);
    //set up camera rotate point
    camRotPoint = new GameObject();
    camRotPoint.transform.position = new Vector3(curr_city.par_bldng.obj.transform.position.x,curr_city.par_bldng.obj.transform.position.y,curr_city.par_bldng.obj.transform.position.z);
    camRotPoint.transform.Rotate(0,135,0);
    GameObject.Find("Main Camera").transform.parent = camRotPoint.transform;
  }
  void Update()
  {

    TimeT = TimeT + Time.deltaTime;

    //left click
    if (Input.GetMouseButton(0) && TimeT > 0.5)
    {
      RaycastHit hitInfo = new RaycastHit();
      bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
      if (hit)
      {
          // Debug.Log("Hit " + hitInfo.transform.gameObject.name + ",");// + hitInfo.transform.gameObject.tag + "," + focused);
          // Debug.Log("Hit Tag " + hitInfo.transform.gameObject.tag);
          if ((hitInfo.transform.gameObject.tag == "File") || (hitInfo.transform.gameObject.tag == "Directory"))
          {
              hitInfo.transform.gameObject.tag="foc_bldng";
//               // setCameraToObj(
//               // hitInfo.transform.gameObject);
              // Debug.Log ("DIRECTORY FOCUSED");
              focused = true;
              clearCity(hitInfo.transform.gameObject);
          } else {
              // Debug.Log ("nopz");
          }
          if ((hitInfo.transform.gameObject.tag == "Base") && (focused == true))
          {
              returnCity(hitInfo.transform.gameObject);
              focused = false;
              // Debug.Log ("CITY_RETURNED");
          } else {
              // Debug.Log ("nopz");
          }
      } else {
          // Debug.Log("No hit");
      }
//         Debug.Log("Mouse is down");
      TimeT = 0;
    }


    if (Input.GetMouseButton(1) && TimeT > 0.5)
    {
      float deltaTime = Time.time - lastTimeClicked;
      float h = rotateSpeed * Input.GetAxis("Mouse X");
      float v = rotateSpeed * Input.GetAxis("Mouse Y");
      float maxTilt = 90 - defViewAng;
      if (camRotPoint.transform.eulerAngles.z + v <= 0.1f || camRotPoint.transform.eulerAngles.z + v >= maxTilt)
          v = 0;

      camRotPoint.transform.eulerAngles = new Vector3(camRotPoint.transform.eulerAngles.x, camRotPoint.transform.eulerAngles.y + h, camRotPoint.transform.eulerAngles.z + v);
     // }
     lastTimeClicked = Time.time;

   }












    //
    if (Input.GetMouseButton(2) && TimeT > 0.5)
    {
      RaycastHit hitInfo = new RaycastHit();
      bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
      if (hit)
      {
          // Debug.Log("Hit " + hitInfo.transform.gameObject.name);
          // Debug.Log("Hit Tag " + hitInfo.transform.gameObject.tag);
          //
          // //if hitting a building that was focused.
          if (hitInfo.transform.gameObject.tag == "foc_bldng")
          {
            //file was focused, so the tag needs to be replaced
            if (hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.item.IsType == PathItem.Type.File)
            {
              // hitInfo.transform.gameObject.tag = "File";
              // Debug.Log ("Not a directory.");
            }
            else if (hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.item.IsType == PathItem.Type.Directory)
            {
              hitInfo.transform.gameObject.tag = "Directory";
            }
          }

          // // otherwise if not focused
          if (hitInfo.transform.gameObject.tag == "Directory")
          {
              Debug.Log("curr_city.par_bldng.path"+ curr_city.par_bldng.path + "Going to: " + hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.path);
              updateUI(hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.path);
              zoom_to_building(hitInfo.transform.gameObject);
              // clearCity(hitInfo.transform.gameObject);
              // hitInfo.transform.gameObject.tag = "new_base";
              // Debug.Log ("It's working!");
          }
          if (hitInfo.transform.gameObject.tag == "Base")
          {
            hitInfo.transform.gameObject.tag = "Directory";
            if (hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.path != "_")
            {
              Debug.Log("Going to BASE! @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ ");
              Debug.Log("Going to: " + hitInfo.transform.gameObject.GetComponent<build_prop>().parent_city.par_bldng.path);
              updateUI(hitInfo.transform.gameObject.GetComponent<build_prop>().parent_city.par_bldng.path);
              zoom_out_building(hitInfo.transform.gameObject.GetComponent<build_prop>().parent_city.par_bldng.path);
            }
          }
      } else {
          // Debug.Log("No hit or hit was a file");
      }
      // Debug.Log("Mouse is down");
      TimeT = 0;
    }
    float scrollFactor = Input.GetAxis("Mouse ScrollWheel");

    if (scrollFactor != 0)
    {
        camRotPoint.transform.localScale = camRotPoint.transform.localScale * (1f - scrollFactor);
    }
  }
}
