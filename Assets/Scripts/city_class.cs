using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class city_class : MonoBehaviour
{
    public bool is_main_city;
    public building_class par_bldng;
    public building_class[,] building_list;
    // Start is called before the first frame update
    void Start()
    {
      is_main_city = false;
    }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //
    // }
}
