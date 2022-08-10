using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATB : MonoBehaviour
{
   
    public float maxATB = 100;
    float aTB;
    public float ATB
    {
        get {return aTB;}
        set
        {
            aTB = value;
        }
    }
    void Start()
    {
        
    }

    float currentTime;
    float timeTakesATBMax = 1; //1 이면 100초 기다리면 max됨, 증가 비율
    void Update()
    {
        //시간에 따라 자동 채워짐
        ATB += currentTime * timeTakesATBMax; 

        if (ATB >= maxATB)
        {
            ATB = maxATB;
        }
    }
}
