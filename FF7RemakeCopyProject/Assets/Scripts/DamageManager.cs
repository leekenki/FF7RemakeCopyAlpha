using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public void CalculateDamage(GameObject player, int originalDamage, EnemyHealth enemyHealth)
    {
        GameObject enemy = enemyHealth.gameObject;
        int finalDamage = originalDamage;
        if (enemyHealth.isHeatMode)
        {
            finalDamage *= enemyHealth.heatModeDamageIncreasingRate; //n배
        }
        //element 에 따른 데미지 증가

        //최종 데미지가 적 hp넘으면 DIE 상태로
        if (finalDamage >= enemyHealth.HP)
        {            
            // 죽었을 때, 이 객체(enemy)를 담고 있는 모든 리스트를 찾아서 remove한다
            GameObject[] tempArr = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gO in tempArr)
            {
                if (gO.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Active)
                {
                    gO.GetComponent<PlayerBase>().activeEnemyList.Remove(enemy);
                }
                else if (gO.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Inactive)
                {
                    gO.GetComponent<PlayerAIBase>().activeEnemyList.Remove(enemy);
                }
            }
            // player.GetComponent<PlayerBase>().activeEnemyList.Remove(enemy);
            // player.GetComponent<PlayerAIBase>().activeEnemyList.Remove(enemy);
            // if (enemy.GetComponent<EnemyBase>().beingAttackedByPlayerOnControl)
            // {
            // }
            // else if (!enemy.GetComponent<EnemyBase>().beingAttackedByPlayerOnControl)
            // {
            // }
            Destroy(enemy);
            //enemy.GetComponent<EnemyBase>().enemyState = EnemyBase.EnemyState.Die;
            GameManager.Instance.enemyCount--;

            Debug.Log("Enemy Dead");
        }
        else if (finalDamage < enemyHealth.HP)
        {
            //최종 계산
        enemyHealth.HP -= finalDamage;
        //Debug.Log("Current Enemy HP is : " + enemyHealth.HP);
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
