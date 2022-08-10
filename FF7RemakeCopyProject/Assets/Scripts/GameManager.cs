using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameFlow 등 관리
public class GameManager : MonoBehaviour
{
    public enum GameFlow
    {
        Idle, //캐릭터 이동, 적 없을 때
        BattleStage, //적과 전투 중
        Pause, //컷신, 대화창, 인벤토리 창 등 잠시 멈춰야 할 때
        Slow, //동료 캐릭터로 컨트롤 변경, 동료 스킬, 물약 등 .. 느려짐
        AttackSlow, //공격 이펙트로 인한 순간 느려지는 상태
        GameOver
    }
    public GameFlow flowState;
    
    public static GameManager Instance;

    #region 현재의 게임 진행방향
    public enum GameDirectionGlobal
    {
        Forward,
        Back,
        Left,
        Right
    }
    // public GameDirectionGlobal gameDirection;
    public GameDirectionGlobal gameDirection = GameDirectionGlobal.Forward;
    #endregion

    public int enemyCount; //현재 생성되어 있는 살아있는 적 수

    void Awake()
    {
        Instance = this; // 게임 매니저는 싱글톤으로
    }
    void Start()
    {
        flowState = GameFlow.Idle; //임시로 idle, 알파나 베타 때 바꿈
    }

    // Update is called once per frame
    public bool isPause = false;
    GameFlow tempFlow;
    public GameObject playerOnControl;
    void Update()
    {
        if (flowState == GameFlow.BattleStage)
        {
            CameraManager.Instance.cameraState = CameraManager.CameraState.BattleStage;
            
        }
        if (flowState != GameFlow.Slow && Input.GetKeyDown(KeyCode.P))
        {            
            MakeSlow();
            /* 패널 열기 */
        }
        else if (flowState == GameFlow.Slow && Input.GetKeyDown(KeyCode.P))
        {
            BringItBackFromSlow();            
            /* 패널 닫기 */
        }

        if (flowState != GameFlow.Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            tempFlow = flowState; //직전 상태 임시 저장
                                  //Time.timeScale = 0.05f;
            Time.timeScale = 0f;
            flowState = GameFlow.Pause;
            playerOnControl.GetComponent<PlayerBase>().canMove = false;
            /* 패널 열기 */
        }
        else if (flowState == GameFlow.Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            playerOnControl.GetComponent<PlayerBase>().canMove = true;
            flowState = tempFlow; //임시 저장한 flow로 되돌림
            /* 패널 닫기 */
        }



        if (enemyCount <= 0)
        {
            flowState = GameFlow.Idle;
        }
    }
    public void MakeSlow()
    {
        tempFlow = flowState; //직전 상태 임시 저장
        //Time.timeScale = 0.05f;
        Time.timeScale = 0.05f;
        flowState = GameFlow.Slow;
        playerOnControl.GetComponent<PlayerBase>().canMove = false;
    }
    public void BringItBackFromSlow()
    {
        Time.timeScale = 1;
        playerOnControl.GetComponent<PlayerBase>().canMove = true;
        flowState = tempFlow; //임시 저장한 flow로 되돌림
    }
}
