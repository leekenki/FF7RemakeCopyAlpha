using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] playerArr = new GameObject[4]; //플레이어들 담을 그릇
    GameObject playerOnControl;
    void Start()
    {
        playerArr = GameObject.FindGameObjectsWithTag("Player"); //플레이어를 찾아서 집어넣는다
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0 ; i < playerArr.Length; i++)
        {
            //플레이어가 들어있는 배열의 원소에 달려있는 PlayerBase의 상태가 active면
            if (playerArr[i] != null && playerArr[i].gameObject.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Active)
            {
                playerOnControl = playerArr[i].gameObject;
            }
        }
        if (playerOnControl)
        {
            PlayerBase playerBase = playerOnControl.GetComponent<PlayerBase>();
            if (playerBase.target)
            {
                transform.position = Vector3.Lerp(transform.position, playerBase.target.transform.position, Time.deltaTime * 8);
            }
            else
            Debug.Log("player target is NULL");
        }
        
        
    }
    
}
