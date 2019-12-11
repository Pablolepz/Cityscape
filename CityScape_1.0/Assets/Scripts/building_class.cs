using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building_class
{
  public string path;
  public PathItem item;
  public GameObject obj;
  public string city_of;
  // public string child_path;

  // public GameObject[,] city_of;
  // private Color orig_color;
  // private bool bld_info;
  // private Vector2 MousePos;
  // private Rect objRect;

  public void setParent(string input)
  {
    city_of = input;
  }
    // Start is called before the first frame update
  // void Start()
  // {
  // }

//
//     // Update is called once per frame
//     void Update()
//     {
//
//     }
  // void OnMouseOver()
  // {
  //   gameObject.GetComponent<Renderer>().material.color = Color.yellow;
  //   bld_info = true;
  // }
  // void OnMouseExit()
  // {
  //   gameObject.GetComponent<Renderer>().material.SetColor("_Color",orig_color);
  //   bld_info = false;
  // }
  // void OnGUI()
  // {
  //   if (bld_info == true)
  //   {
  //     MousePos = Input.mousePosition;
  //     objRect.x = MousePos.x;
  //     objRect.y = Mathf.Abs(MousePos.y - Camera.main.pixelHeight);
  //     GUI.Label(objRect, gameObject.tag);
  //   }
  // }
}
