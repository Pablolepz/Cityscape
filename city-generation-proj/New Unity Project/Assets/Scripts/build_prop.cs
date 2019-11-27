using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_prop : MonoBehaviour
{
    public string fileName = "";
    private Color orig_color;
    private bool bld_info;
    private Vector2 MousePos;
    private Rect objRect;
    private GUIStyle hoverStyle = new GUIStyle();
    private GUIContent guiCont = new GUIContent();
    public building_class parent_class = null;
    public city_class parent_city = null;
    public city_class child_city = null;
    public Texture label_bkgd;
    // Start is called before the first frame update
    void Start()
    {
      orig_color = gameObject.GetComponent<Renderer>().material.color;
      bld_info = false;
      MousePos = new Vector2(0, 0);
      objRect = new Rect(0, 0, 1, 35);

      hoverStyle.fontSize = 36;
      guiCont.image = label_bkgd;
      guiCont.text = "    " + fileName;
    }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //
    // }
    void OnMouseOver()
    {
      gameObject.GetComponent<Renderer>().material.color = Color.yellow;
      bld_info = true;
    }
    void OnMouseExit()
    {
      gameObject.GetComponent<Renderer>().material.SetColor("_Color",orig_color);
      bld_info = false;
    }
    void OnGUI()
    {
      if (bld_info == true)
      {
        MousePos = Input.mousePosition;
        objRect.x = MousePos.x;
        objRect.y = Mathf.Abs(MousePos.y - Camera.main.pixelHeight);
        GUI.Label(objRect,guiCont, hoverStyle);
      }
    }
}
