using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 5000;
    int hp;
    public int HP
    {
        get {return hp;}
        set
        {
            hp = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
