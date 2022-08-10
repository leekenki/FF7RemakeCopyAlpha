using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//player state가 inactive일 때
public class PlayerAIBase : MonoBehaviour
{
    public enum PlayerAIState
    {
        Idle, //컨트롤 중인 플레이어 따라감
        Battle, //배틀
        OnControl //플레이어가 직접 조종 중일 때, AI입장에서는 비활성화 상태

    }
    public PlayerAIState playerAIState;
    public enum AutoFightState
    {
        ReadyToAttack,
        NormalAttack,
        Skill0
    }

    //직업
    public enum PlayerType
    {
        Player0,
        Player1,
        Player2
    }
    public PlayerType playerType;

    NavMeshAgent agent;
    GameObject[] playerArr = new GameObject[4]; //플레이어들 담을 그릇
    PlayerAttackBase playerAttack;
    virtual protected void Start()
    {
        playerAttack = GetComponent<PlayerAttackBase>();
        // switch (playerType)
        // {
        //     case PlayerType.Player0:
        //         playerAttack = new Player0Attack();
        //         break;
        //     case PlayerType.Player1:
        //         //프로토타입때만 임시로 player0로 통일
        //         playerAttack = new Player1Attack();
        //         break;
        //     case PlayerType.Player2:
        //         //프로토타입때만 임시로 player0로 통일
        //         playerAttack = new Player2Attack();
        //         break;
        // }

        agent = GetComponent<NavMeshAgent>();
        // playerAIState = PlayerAIState.Idle;
        playerArr = GameObject.FindGameObjectsWithTag("Player"); //플레이어를 찾아서 집어넣는다
        
    }
    GameObject playerOnControl;
    GameObject target;
    public List<GameObject> activeEnemyList = new List<GameObject>();
    GameObject[] tempEnemyArr;

    virtual protected void Update()
    {
        for (int i = 0 ; i < playerArr.Length; i++)
        {
            //플레이어가 들어있는 배열의 원소에 달려있는 PlayerBase의 상태가 active면
            if (playerArr[i] != null && playerArr[i].gameObject.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Active)
            {
                playerOnControl = playerArr[i].gameObject;
            }
        }
        switch (playerAIState)
        {
            case PlayerAIState.Idle:
                UpdateIdle();
                break;

            case PlayerAIState.Battle:
                UpdateBattle();
                break;

            case PlayerAIState.OnControl:
                // agent.velocity = Vector3.zero;
                // agent.enabled = false;
                UpdateOnControl();
                break;
        }

        if (GameManager.Instance.enemyCount <= 0)
        {
            target = null;
        }
    }
    public bool goAhead = false; //플레이어 앞질러 감, 너무 앞지르면 멈춰서서 기다림
    public bool followLate = true;  // 플레이어 뒤에서 따라감
    
    protected void UpdateIdle()
    {
        if (!agent.enabled) //navMesh 꺼져 있으면 켜서 따라가라
            agent.enabled = true;

        if (followLate && !goAhead) //뒤 따라가야 하면
        {
            agent.destination = playerOnControl.transform.position; //컨트롤 중인 플레이어 목적지로
            
        }
        else if (!followLate && goAhead) // 플레이어 앞질러서 가는 경우
        {
            //목적지: 플레이어의 위치 + 게임 진행의 목적지 방향 * 거리
        }

        //배틀스테이지고 플레이어가 타깃 없을 때
        if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
        {
            if (!target) //타겟이 없을 때만
            {
                // find로 enemy 찾고 임시 배열에 넣는다.
                // tempEnemyArr = GameObject.FindGameObjectsWithTag("Enemy"); 
                // for (int i = 0; i < tempEnemyArr.Length; i++)
                // {
                //     //리스트에 임시 배열에 해당하는 원소가 없으면, 즉 새로운 원소라면 리스트에 추가
                //     if (!activeEnemyList.Contains(tempEnemyArr[i])) 
                //     {
                //         activeEnemyList.Add((tempEnemyArr[i]));
                //     }
                // }
                // //랜덤으로 타겟 지정
                // target = activeEnemyList[UnityEngine.Random.Range(0, activeEnemyList.Count)];
                
                // agent.destination = target.transform.position;
                // Debug.Log("Target is NULL");
                FindTarget(); //전투 시작할 때 한 번 타겟 정한다.
            }
            else if (target)
            {
                playerAIState = PlayerAIState.Battle; // 적 때리기
            }
        }
    }
    protected void FindTarget()
    {
        // find로 enemy 찾고 임시 배열에 넣는다.
        tempEnemyArr = GameObject.FindGameObjectsWithTag("Enemy"); 
        for (int i = 0; i < tempEnemyArr.Length; i++)
        {
            //리스트에 임시 배열에 해당하는 원소가 없으면, 즉 새로운 원소라면 리스트에 추가
            if (!activeEnemyList.Contains(tempEnemyArr[i])) 
            {
                activeEnemyList.Add((tempEnemyArr[i]));
            }
        }
        if (activeEnemyList.Count > 0)
        {
            //랜덤으로 타겟 지정
            target = activeEnemyList[UnityEngine.Random.Range(0, activeEnemyList.Count)];
        }
        
    }
    float currentTime = 0f;
    float attackDelay = 3f;
    Vector3 directionFromPlayerToEnemy;
    protected void UpdateBattle()
    {
        if (target)
        {
            currentTime += Time.deltaTime;
            
            agent.destination = target.transform.position;
            directionFromPlayerToEnemy = new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            directionFromPlayerToEnemy.Normalize();
            transform.forward = directionFromPlayerToEnemy; 

            //일정 시간 간격으로 공격
            if (currentTime > attackDelay)
            {
                target.GetComponent<EnemyBase>().beingAttackedByPlayerOnControl = false;
                playerAttack.WeekAttack(target);
                currentTime = 0;
            }
            
        }
        else if (!target)
        {
            if (GameManager.Instance.flowState == GameManager.GameFlow.Idle)
            {
                playerAIState = PlayerAIState.Idle;
            }

            FindTarget();

        }
    }
    protected void UpdateOnControl()
    {
        if (agent.enabled)
        {
            agent.enabled = false;
        }
    }
}
