using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sort : MonoBehaviour
{
    
    List<string> differentSorts = new List<string>(){"please choose", "SORT",/* "size (small to large)", "size (large to small)"*/};

    public Dropdown sortDropdown;

    FileSystemReader fsr = new FileSystemReader();
    // PathItem pitem = new PathItem();
    // [SerializeField] private List<ListData> listDatas = fsr.FetchFromPath("/Users/andrew/Documents");


    public void Start()
    {
        populateList();

        // get initial files of directory
        // foreach(ListData item in listDatas)
        // {
            // listDatas.Add(new ListData(fsr.Files, fsr.Volumes, pitem.ByteSize));
        // }
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
            Debug.Log("SORT");
            // alphabet sort
            // SortTheList();
        }
        // else if(index == 2)
        // {
        //     Debug.Log("size small to large");
        //     // size sort small to large
        // }
        // else if(index == 3)
        // {
        //     Debug.Log("size large to small");
        //     // size sort large to small
        // }
    }

    private void SortTheList()
    {
        // listDatas.Sort(SortFunc);
    }
    
    // private int SortFunc(ListData a, ListData b)
    // {
    //     if(a.strength < b.strength)
    //     {
    //         return -1;
    //     }
    //     else if(a.strength > b.strength)
    //     {
    //         return 1;
    //     }
    //     if(a.height < b.height)
    //     {
    //         return -1;
    //     }
    //     else if(a.height > b.height)
    //     {
    //         return 1;
    //     }
    // }

    //Structs
    public struct SortHeight
    {
        public int height;
        public SortHeight(int height)
        {
            this.height = height;
        }
    }

    public struct ListData
    {
        public string FolderName;
        public int strength;
        public int height;

        public ListData(string FolderName, int strength, int height)
        {
            this.FolderName = FolderName;
            this.strength = strength;
            this.height = height;
        }
    }
}
