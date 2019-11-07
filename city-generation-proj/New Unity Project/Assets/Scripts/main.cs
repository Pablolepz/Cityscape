using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public static float GlobalUnit = 0.5f;
    public static float platformUnit = 0.5f;
    //This array is sudo for the array of objects given
    public static GameObject[,] city = new GameObject[1000,1000];
    public static float GlobalSize(float input)
    {
      return input * GlobalUnit;
    }
    // Start is called before the first frame update
    void Start()
    {
        int inputArrSize = city.GetLength(0) * city.GetLength(1);
        int x = 1;
        while(x*x < inputArrSize)
        {
          x++;
        }
        var platform = Object.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        platform.transform.localScale = new Vector3(GlobalSize(x), GlobalSize(0.2f), GlobalSize(x));
        platform.transform.position = new Vector3(GlobalSize(x / 2 + 0.5f), GlobalSize(0.1f), GlobalSize(x / 2 + 0.5f));
        platform.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        for (int a = 0; a < city.GetLength(0); a++){
          for(int b = 0; b < city.GetLength(1); b++){
            city[a,b] = Object.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));;
            city[a,b].transform.localScale = new Vector3(GlobalUnit * 0.7f,GlobalSize(Random.Range(1,5)),GlobalUnit * 0.7f);
            city[a,b].transform.position = new Vector3(GlobalSize(a + 0.5f),(((city[a,b].transform.localScale.y)/2) + ((platform.transform.localScale.y)/2)),GlobalSize(b + 0.5f));
          }
        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }
}
