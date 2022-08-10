using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 땅을 밟으면 땅에 상태에 따라 절대방향 변화 or 코너로 전환 or battle스테이지 진입

public class Road : MonoBehaviour
{
    public enum Direction
    {
        Forward,
        Back,
        Left,
        Right
    }

    public Direction direction; //유니티 에디터에서 설정
    public bool isCorner; //에디터에서 설정
    public bool isBattleGround; //에디터에서 설정
   

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     //코너가 아니고 닿은 물체가 컨트롤 중인 플레이어일 때, 절대방향 설정
        
    //     if (!hit.collider.GetComponent<Road>().isCorner) //코너가 아닐 경우 절대 방향 설정
    //     {
    //         switch(direction)
    //         {
    //             case Direction.Forward:
    //                 GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Forward;
    //                 break;
    //             case Direction.Back:
    //                 GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Back;
    //                 break;
    //             case Direction.Left:
    //                 GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Left;
    //                 break;
    //             case Direction.Right:
    //                 GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Right;
    //                 break;
    //         }
    //     }

    //     else if (hit.collider.GetComponent<Road>().isCorner)
    //     {
    //         CameraManager.Instance.isCorner = true;
    //     }

    //     if (hit.collider.GetComponent<Road>().isBattleGround && GameManager.Instance.enemyCount > 0)
    //     {
    //         GameManager.Instance.flowState = GameManager.GameFlow.BattleStage;
    //     }
    // }
    private void OnTriggerEnter(Collider other)
    {
        PlayerBase playerBase = other.GetComponent<PlayerBase>();
        //코너가 아니고 닿은 물체가 컨트롤 중인 플레이어일 때, 절대방향 설정
        if (!isCorner
         && playerBase.playerState == PlayerBase.PlayerState.Active) //코너가 아닐 경우 절대 방향 설정
        {
            switch(direction)
            {
                case Direction.Forward:
                    GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Forward;
                    break;
                case Direction.Back:
                    GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Back;
                    break;
                case Direction.Left:
                    GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Left;
                    break;
                case Direction.Right:
                    GameManager.Instance.gameDirection = GameManager.GameDirectionGlobal.Right;
                    break;
            }
        }

        else if (isCorner
         && playerBase.playerState == PlayerBase.PlayerState.Active)
        {
            CameraManager.Instance.isCorner = true;
        }

        else if (isBattleGround && GameManager.Instance.enemyCount > 0)
        {
            GameManager.Instance.flowState = GameManager.GameFlow.BattleStage;
        }
    }
}
