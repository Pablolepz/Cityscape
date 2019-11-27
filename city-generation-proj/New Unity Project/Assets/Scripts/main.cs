using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class main : MonoBehaviour
{
    private static string pathForCity = "D:\\Documents\\College";
    private static city_class curr_city = new city_class();
    public static float GlobalUnit = 1f;
    public static float GlobalNormalizer = 6f;
    public static float platformUnit = 0.5f;
    public static float defViewAng = 45.0f;
    public static bool focused = false;
    private static FileSystemReader file_rtvr;
    //This array is sudo for the array of objects given
    // somearray
    // camera rotation variables ==========================
    // public static GameObject cameraOrbit;
    public float rotateSpeed = 8f;
    public Color tempColor;
    public static GameObject mainPlatform;
    public GameObject camRotPoint;
    // Mouse Variables ===================
    private float lastTimeClicked;
    private bool debug = false;
    float maxTimeBetweenClicks = 0.5f; // half a second
//     // ========================
    public static float GlobalSize(float input)
    {
      return input * GlobalUnit;
    }
    public static float Norm(float input){
      return input * GlobalNormalizer;
    }




    // public static GameObject[,] RandCity(int levels, GameObject par_bldng = null)
    // {
//       //for each level create a city for each building.
//       for (int x = 0; x < levels; x++)
//       {
//
//       }+9
//       // ===================================================
//       GameObject platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
//       //returns GameObject array of random buildings
      // int newX = UnityEngine.Random.Range(5, 50);
      // Debug.Log(newX);
      // GameObject[,] newCity = new GameObject[newX,newX];
//       for (int a = 0; a < newCity.GetLength(0); a++){
//         for(int b = 0; b < newCity.GetLength(1); b++){
//           newCity[a,b] = GameObject.CreatePrimitive(PrimitiveType.Cube);
//           newCity[a,b].tag="Cube";
//           newCity[a,b].transform.localScale = new Vector3(GlobalUnit * 0.7f,GlobalSize(UnityEngine.Random.Range(1,5)),GlobalUnit * 0.7f);
//           newCity[a,b].transform.position = new Vector3(GlobalSize(a + 0.5f),(((newCity[a,b].transform.localScale.y)/2) + ((GlobalSize(0.1f))/2)),GlobalSize(b + 0.5f));
//           // newCity[a,b].GetComponent<Renderer>().material.SetColor("_Color", new Color(
//           //       UnityEngine.Random.Range(0f, 1f),
//           //       UnityEngine.Random.Range(0f, 1f),
//           //       UnityEngine.Random.Range(0f, 1f))
//           //     );
//           newCity[a,b].GetComponent<Renderer>().material.color = Color.grey;
//           newCity[a,b].AddComponent<building_class>();
//           var bldClass = newCity[a,b].GetComponent<building_class>();
//           if (par_bldng != null)
//           {
//             //this means it is *not* the first city
//             bldClass.par_bldng = par_bldng;
//           }
//           else
//           {
//             int inputArrSize = newCity.GetLength(0) * newCity.GetLength(1);
//             int x = 1;
//             while(x*x < inputArrSize)
//             {
//               x++;
//             }
//             platform.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.2f), GlobalSize(x));
//             platform.transform.position = new Vector3(GlobalSize(x / 2 + 0.5f), GlobalSize(0.1f), GlobalSize(x / 2 + 0.5f));
//             platform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
//             platform.tag="Base";
//             //set the current platform made to be the mainPlatform
//             mainPlatform = platform;
//             //have the parent building be the platform created
//             bldClass.par_bldng = platform;
//           }
//           //this building is a child of the city
//           bldClass.city_of = newCity;
//         }
//       }
    //   return newCity;
    // }
//
//
    // public static (float, float, float) calcCamPlat(float inputAng, GameObject plat) {
    //   float platThicc = (float) Math.Sqrt((Math.Pow(plat.transform.localScale.x,2)) + (Math.Pow(plat.transform.localScale.x,2)));
    //   float screenThicc = ((float) Math.Cos(inputAng)) * ((float)Math.Tan(inputAng) * (platThicc + (platThicc/2)));
    //   float y = (float) Math.Cos(inputAng) * screenThicc *-1;
    //   return (-plat.transform.localScale.x/6,-y,-plat.transform.localScale.x/6);
    // }
    // public static void setCameraToObj(GameObject platToFocus)
    // {
//       var newCameraXYZ = calcCamPlat(defViewAng,platToFocus);
//       if (defViewAng >= 45f) {
//         GameObject.Find("Main Camera").transform.position = new Vector3(newCameraXYZ.Item1,newCameraXYZ.Item2,newCameraXYZ.Item3);
      // }
//       else
//       {
//           GameObject.Find("Main Camera").transform.position = new Vector3(newCameraXYZ.Item1,-newCameraXYZ.Item2,newCameraXYZ.Item3);
//       }
//       GameObject.Find("Main Camera").transform.Rotate(defViewAng,45,0);
    // }






    public static void initFirstCity()
    {
      curr_city = initCity();
    }


    public static city_class initCity(string input = null, building_class parent = null)
    {
      file_rtvr = new FileSystemReader();
      city_class new_city = new city_class();
      if (input == null)
      {
        //initial city for application
        file_rtvr.FetchAll(pathForCity);
        new_city.is_main_city = true;
        new_city.par_bldng = new building_class();
        input = pathForCity;
        new_city.par_bldng.obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        new_city.par_bldng.obj.AddComponent<build_prop>().parent_city = null;

      }
      else
      {
        file_rtvr.FetchAll(input);
        new_city.par_bldng = parent;
        new_city.par_bldng.obj.AddComponent<build_prop>().parent_city = new_city;
      }
      //finding city size
      int city_list_size = file_rtvr.FilesAndDirectories.Count;
      int x = 1;
      while(x*x < city_list_size)
      {
        x++;
      }
      new_city.par_bldng.obj.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.29f), GlobalSize(x));
      new_city.par_bldng.obj.transform.position = new Vector3(GlobalSize(x / 2), -GlobalSize(0.1f), GlobalSize(x / 2));
      new_city.par_bldng.obj.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
      new_city.par_bldng.obj.AddComponent<build_prop>().child_city = new_city;
      new_city.par_bldng.obj.tag = "Base";

      new_city.building_list = new building_class[x,x];
      //finding max size
      float max = 0.0f;
      for(int i = 0; i < file_rtvr.FilesAndDirectories.Count; i++)
      {
        if ((float)file_rtvr.FilesAndDirectories[i].ByteSize > max)
        {
          max = (float)file_rtvr.FilesAndDirectories[i].ByteSize;
        }
        // Debug.Log(file_rtvr.FilesAndDirectories[i].ToString());
        // Debug.Log(max);
      }
      GlobalNormalizer = GlobalNormalizer/max;

      int a = 0;
      int b = 0;
      for (int i = 0; i < file_rtvr.FilesAndDirectories.Count; i++)
      {
        //initialize files
        new_city.building_list[a,b] = new building_class();
        new_city.building_list[a,b].path = pathForCity + "\\" + file_rtvr.FilesAndDirectories[i].Name;
        new_city.building_list[a,b].item = file_rtvr.FilesAndDirectories[i];

        if (file_rtvr.FilesAndDirectories[i].IsType == PathItem.Type.File)
        {
            // Debug.Log(file_rtvr.FilesAndDirectories[i]);

            new_city.building_list[a,b].obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            new_city.building_list[a,b].obj.tag="File";
            new_city.building_list[a,b].obj.GetComponent<Renderer>().material.color = Color.grey;
            new_city.building_list[a,b].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, Norm(GlobalSize((float)new_city.building_list[a,b].item.ByteSize))/2, GlobalUnit * 0.7f);
            new_city.building_list[a,b].obj.transform.position = new Vector3(GlobalSize(a + 0.5f),(((new_city.building_list[a,b].obj.transform.localScale.y)) + ((GlobalSize(0.1f))/2)),GlobalSize(b + 0.5f));
        }
        else if (file_rtvr.FilesAndDirectories[i].IsType == PathItem.Type.Directory)
        {
          new_city.building_list[a,b].obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
          new_city.building_list[a,b].obj.tag="Directory";
          // new_city.building_list[a,b].obj.GetComponent<Renderer>().material.color = Color.white;
          // Debug.Log(file_rtvr.FilesAndDirectories[i]);
          new_city.building_list[a,b].obj.transform.localScale = new Vector3(GlobalUnit * 0.7f, Norm(GlobalSize(max)), GlobalUnit * 0.7f);
          new_city.building_list[a,b].obj.transform.position = new Vector3(GlobalSize(a + 0.5f),(((new_city.building_list[a,b].obj.transform.localScale.y)/2) + ((GlobalSize(0.1f))/2)),GlobalSize(b + 0.5f));
        }
        new_city.building_list[a,b].obj.AddComponent<build_prop>();
        new_city.building_list[a,b].obj.GetComponent<build_prop>().fileName = file_rtvr.FilesAndDirectories[i].Name;
        new_city.building_list[a,b].obj.GetComponent<build_prop>().parent_city = new_city;
        new_city.building_list[a,b].obj.GetComponent<build_prop>().parent_class = new_city.building_list[a,b];

        //city iterator
        b++;
        if (b >= new_city.building_list.GetLength(1))
        {
          a++;
          b = 0;
        }
      }
      Debug.Log("New city made");
      return new_city;
    }



    // Start is called before the first frame update =========================================================
    void Start()
    {
      initFirstCity();

//         //to be replaced by initCity(); where initCity will grab the array of files and create buildings from the files.
//         if (debug == true)
//         {
//           // creates a random city with x levels
//           GameObject[,] city = RandCity(2);
//         }
//         else
//         {
//           // retrieves files
//           // GameObject[,] city = city_init(inputArr);
//         }
//         // Set Camera Angle
//         setCameraToObj(mainPlatform); //platform set in the random city function
//         //set up camera rotate point
//         camRotPoint = new GameObject();
//         camRotPoint.transform.position = new Vector3(mainPlatform.transform.position.x,mainPlatform.transform.position.y,mainPlatform.transform.position.z);
//         camRotPoint.transform.Rotate(0,135,0);
//         GameObject.Find("Main Camera").transform.parent = camRotPoint.transform;
//         // cameraOrbit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         tempColor = new Color(
//           200f,
//           0f,
//           100f
//         );
    }
    public static void clearCity(GameObject clickedBuilding)
    {
      // curr_city = clickedBuilding.GetComponent<build_prop>().parent_city;
      for (int a = 0; a < curr_city.building_list.GetLength(0); a++){
        for(int b = 0; b < curr_city.building_list.GetLength(1); b++){
          Debug.Log ("a,b|" + a+","+b);
          Debug.Log ("curr_city.building_list[a,b] is " + curr_city.building_list[a,b]);
          Debug.Log ("curr_city.building_list[a,b].obj is " + curr_city.building_list[a,b].obj);
          if(curr_city.building_list[a,b].obj != null)
          {
            if (curr_city.building_list[a,b].obj.tag != "foc_bldng")
            {
              curr_city.building_list[a,b].obj.GetComponent<MeshRenderer>().enabled = false;
              curr_city.building_list[a,b].obj.layer = LayerMask.NameToLayer("Ignore Raycast");
            }
          }
          else
          {
            Debug.Log ("NULLITY!");
          }
        }
      }
      return;
    }
    public static void returnCity(GameObject clickedBase)
    {
      // curr_city = clickedBase.GetComponent<build_prop>().obj.parent_city;
      for (int a = 0; a < curr_city.building_list.GetLength(0); a++){
        for(int b = 0; b < curr_city.building_list.GetLength(1); b++){
          if (curr_city.building_list[a,b].obj.tag == "foc_bldng")
          {
            if (curr_city.building_list[a,b].obj.GetComponent<build_prop>().child_city == null)
            {
              curr_city.building_list[a,b].obj.tag = "File";
            }
            else
            {
              curr_city.building_list[a,b].obj.tag = "Directory";
            }
          }
          curr_city.building_list[a,b].obj.GetComponent<MeshRenderer>().enabled = true;
          curr_city.building_list[a,b].obj.layer = LayerMask.NameToLayer("Default");

        }
      }
      return;
    }
    public static void zoom_to_building(GameObject new_plat)
    {
      // runZoomAnimation();
      Debug.Log("Going to "+ new_plat.GetComponent<build_prop>().parent_class.path + "=============================");
      Destroy(new_plat.GetComponent<build_prop>().parent_class.city_of.par_bldng);
      city_class tmpCity = initCity(new_plat.GetComponent<build_prop>().parent_class.path, new_plat.GetComponent<build_prop>().parent_class);

      for (int a = 0; a < curr_city.building_list.GetLength(0); a++){
        for(int b = 0; b < curr_city.building_list.GetLength(1); b++){
          Debug.Log("testinggggggggggggg, " + a + ", " + b);
          if(curr_city.building_list[a,b] != null)
          {
            Destroy(curr_city.building_list[a,b].obj);
          }
          else
          {
            Debug.Log("Nulled");
          }
        }
      }
      // Destroy(curr_city.par_bldng.obj);
      curr_city = null;
      curr_city = tmpCity;
      returnCity(new_plat);
      return;
    }

    // Update is called once per frame
    float TimeT = 0;

    void Update()
    {
      //on click
      TimeT = TimeT + Time.deltaTime;
      if (Input.GetMouseButton(0) && TimeT > 1)
      {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            Debug.Log("Hit " + hitInfo.transform.gameObject.name + "," + hitInfo.transform.gameObject.tag + "," + focused);
            if ((hitInfo.transform.gameObject.tag == "File") || (hitInfo.transform.gameObject.tag == "Directory"))
            {
                hitInfo.transform.gameObject.tag="foc_bldng";
                // setCameraToObj(
                // hitInfo.transform.gameObject);
                Debug.Log ("DIRECTORY FOCUSED");
                focused = true;
                clearCity(hitInfo.transform.gameObject);
            } else {
                Debug.Log ("nopz");
            }
            if ((hitInfo.transform.gameObject.tag == "Base") && (focused == true))
            {
                returnCity(hitInfo.transform.gameObject);
                focused = false;
                Debug.Log ("CITY_RETURNED");
            } else {
                Debug.Log ("nopz");
            }
        } else {
            Debug.Log("No hit");
        }
//         Debug.Log("Mouse is down");
        TimeT = 0;
      }

//======================Camera Functions right click===================
//       if (Input.GetMouseButton(1))
//        {
//           float deltaTime = Time.time - lastTimeClicked;
//           float h = rotateSpeed * Input.GetAxis("Mouse X");
//           float v = rotateSpeed * Input.GetAxis("Mouse Y");
//           float maxTilt = 90 - defViewAng;
//
//           if (camRotPoint.transform.eulerAngles.z + v <= 0.1f || camRotPoint.transform.eulerAngles.z + v >= maxTilt)
//               v = 0;
//
//           camRotPoint.transform.eulerAngles = new Vector3(camRotPoint.transform.eulerAngles.x, camRotPoint.transform.eulerAngles.y + h, camRotPoint.transform.eulerAngles.z + v);
//          // }
//          lastTimeClicked = Time.time;
//
//        }
//======================Camera Functions right click end===============

       if (Input.GetMouseButton(2))
       {
         RaycastHit hitInfo = new RaycastHit();
         bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
         if (hit)
         {
             Debug.Log("Hit " + hitInfo.transform.gameObject.name);
             if (hitInfo.transform.gameObject.tag == "foc_bldng")
             {
               if (hitInfo.transform.gameObject.GetComponent<build_prop>().parent_class.item.IsType == PathItem.Type.File)
               {
                 hitInfo.transform.gameObject.tag = "File";
               }
               else
               {
                 hitInfo.transform.gameObject.tag = "Directory";
               }
             }
             if (hitInfo.transform.gameObject.tag == "Directory")
             {
                 // clearCity(hitInfo.transform.gameObject);
                 zoom_to_building(hitInfo.transform.gameObject);
                 Debug.Log ("It's working!");
             } else {
                 Debug.Log ("Not a directory.");
             }
         } else {
             // Debug.Log("No hit");
         }
         // Debug.Log("Mouse is down");
       }
//
//        float scrollFactor = Input.GetAxis("Mouse ScrollWheel");
//
//        if (scrollFactor != 0)
//        {
//            camRotPoint.transform.localScale = camRotPoint.transform.localScale * (1f - scrollFactor);
//        }
//        // GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, 0);
//        //
//        // GameObject.Find("Main Camera").transform.LookAt(cameraOrbit.transform.position);
//
//
    }
}
