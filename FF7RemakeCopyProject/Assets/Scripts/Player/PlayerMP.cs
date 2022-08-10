using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
   
    public int maxMP = 5000;
    int mp;
    public int MP
    {
        get {return mp;}
        set
        {
           mp = value;
        }
    }
    // Start is alled before the first frame update
    void Start()
    {
        MP = maxMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
