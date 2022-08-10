using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //적 HP관리, 히트모드, 버스트모드 관리
    #region HP Management
    public int maxHP = 1000;
    int hp;
    public int HP
    {
        get {return hp;}
        set
        {
            hp = value;

        }
    }
    #endregion
    
    #region Heat Mode Management
    //heatmode 면 피격 데미지 증가
    public bool isHeatMode = false;
    public int heatModeDamageIncreasingRate = 10;
    

    public int maxHeat = 100;
    int heat;
    public int Heat
    {
        get {return heat;}
        set
        {
            heat = value;
        }
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        Heat = 0;
    }

    float currentTime;
    public float heatModeHoldTime = 10f;
    void Update()
    {
        //Heat Mode 면 히트모드 유지시간 동안 대기, 넘으면 false로
        if (isHeatMode)
        {
            currentTime += Time.deltaTime;
            if (currentTime > heatModeHoldTime)
            {
                currentTime = 0;
                isHeatMode = false;
            }
        }
    }
}
