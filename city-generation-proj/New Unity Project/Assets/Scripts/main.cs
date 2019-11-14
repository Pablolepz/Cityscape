using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class main : MonoBehaviour
{
    public static float GlobalUnit = 0.5f;
    public static float platformUnit = 0.5f;
    public static float defViewAng = 45.0f;
    //This array is sudo for the array of objects given
    public static GameObject[,] city = new GameObject[10,10];
    // camera rotation variables ==========================
    // public static GameObject cameraOrbit;
    public float rotateSpeed = 8f;
    public Color tempColor;
    public GameObject platform;
    public GameObject camRotPoint;
    // Mouse Variables ===================
    private float lastTimeClicked;
    float maxTimeBetweenClicks = 0.5f; // half a second
    // ========================
    public static float GlobalSize(float input)
    {
      return input * GlobalUnit;
    }
    public static (float, float, float) calcCamPlat(float inputAng, GameObject plat) {
      float platThicc = (float) Math.Sqrt((Math.Pow(plat.transform.localScale.x,2)) + (Math.Pow(plat.transform.localScale.x,2)));
      float screenThicc = ((float) Math.Cos(inputAng)) * ((float)Math.Tan(inputAng) * (platThicc + (platThicc/2)));
      float y = (float) Math.Cos(inputAng) * screenThicc *-1;
      return (-plat.transform.localScale.x/6,y,-plat.transform.localScale.x/6);
    }
    // Start is called before the first frame update
    void Start()
    {
        // mouse setup ========================================
        // environment setup ==================================
        int inputArrSize = city.GetLength(0) * city.GetLength(1);
        int x = 1;
        while(x*x < inputArrSize)
        {
          x++;
        }
        platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        platform.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.2f), GlobalSize(x));
        platform.transform.position = new Vector3(GlobalSize(x / 2 + 0.5f), GlobalSize(0.1f), GlobalSize(x / 2 + 0.5f));
        platform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        for (int a = 0; a < city.GetLength(0); a++){
          for(int b = 0; b < city.GetLength(1); b++){
            city[a,b] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            city[a,b].tag="Cube";
            city[a,b].transform.localScale = new Vector3(GlobalUnit * 0.7f,GlobalSize(UnityEngine.Random.Range(1,5)),GlobalUnit * 0.7f);
            city[a,b].transform.position = new Vector3(GlobalSize(a + 0.5f),(((city[a,b].transform.localScale.y)/2) + ((platform.transform.localScale.y)/2)),GlobalSize(b + 0.5f));
                city[a, b].GetComponent<Renderer>().material.SetColor("_Color", new Color(
                  UnityEngine.Random.Range(0f, 1f),
                  UnityEngine.Random.Range(0f, 1f),
                  UnityEngine.Random.Range(0f, 1f))
                );
          }
        }
        // Set Camera Angle
        var newCameraXYZ = calcCamPlat(defViewAng,platform);
        if (defViewAng >= 45f) {
          GameObject.Find("Main Camera").transform.position = new Vector3(newCameraXYZ.Item1,-newCameraXYZ.Item2,newCameraXYZ.Item3);
        }
        else
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(newCameraXYZ.Item1,newCameraXYZ.Item2,newCameraXYZ.Item3);
        }
        GameObject.Find("Main Camera").transform.Rotate(defViewAng,45,0);

        //set up camera rotate point
        camRotPoint = new GameObject();
        camRotPoint.transform.position = new Vector3(platform.transform.position.x,platform.transform.position.y,platform.transform.position.z);
        camRotPoint.transform.Rotate(0,135,0);
        GameObject.Find("Main Camera").transform.parent = camRotPoint.transform;
        // cameraOrbit = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tempColor = new Color(
          200f,
          0f,
          100f
        );
        // tempColor.a = 0.5f;
        // cameraOrbit.GetComponent<Renderer>().material.SetColor("_Color", tempColor);
        // cameraOrbit.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        // cameraOrbit.transform.position = new Vector3(platform.transform.localScale.x/2,platform.transform.localScale.y/2,platform.transform.localScale.x/2);
        // var dist = Vector3.Distance(GameObject.Find("Main Camera").transform.position, cameraOrbit.transform.position) * 2;
        // cameraOrbit.transform.localScale = new Vector3(dist,dist, dist);
    }

    // Update is called once per frame
    void Update()
    {
      //on click
      if (Input.GetMouseButton(0))
      {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit)
        {
            Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            if (hitInfo.transform.gameObject.tag == "Cube")
            {
                hitInfo.transform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(
                  UnityEngine.Random.Range(0f, 1f),
                  UnityEngine.Random.Range(0f, 1f),
                  UnityEngine.Random.Range(0f, 1f))
                );
                Debug.Log ("It's working!");
            } else {
                Debug.Log ("nopz");
            }
        } else {
            Debug.Log("No hit");
        }
        Debug.Log("Mouse is down");
      }
      if (Input.GetMouseButton(1))
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

       float scrollFactor = Input.GetAxis("Mouse ScrollWheel");

       if (scrollFactor != 0)
       {
           camRotPoint.transform.localScale = camRotPoint.transform.localScale * (1f - scrollFactor);
       }
       // GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, 0);
       //
       // GameObject.Find("Main Camera").transform.LookAt(cameraOrbit.transform.position);


    }
}
