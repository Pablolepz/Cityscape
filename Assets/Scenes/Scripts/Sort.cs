using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sort : MonoBehaviour
{
    
    List<string> differentSorts = new List<string>(){"please choose", "alphabetically", "size (small to large)", "size (large to small)"};

    public Dropdown sortDropdown;

    public void Start()
    {
        populateList();
    }

    void populateList()
    {
        sortDropdown.AddOptions(differentSorts);
    }

    // TODO: put sorting algorithms here
    public void dropdownIndex(int index)
    {
        if(index == 1)
        {
            Debug.Log("alpha sort");
            // alphabet sort
        }
        else if(index == 2)
        {
            Debug.Log("size small to large");
            // size sort small to large
        }
        else if(index == 3)
        {
            Debug.Log("size large to small");
            // size sort large to small
        }
    }
    
}
