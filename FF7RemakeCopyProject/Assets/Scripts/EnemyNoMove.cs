using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoMove : MonoBehaviour
{
    #region 4속성
    public enum Elements
    {
        Fire, //화
        Water, //수
        Electro, //전
        Earth //토
    }
    public Elements enemyElement; //현재 적의 속성, 에디터 창에서 설정해줌
    #endregion
    
    #region Enemy State
    public enum EnemyState
    {
        Idle, //활성화 되기 전
        Follow, //플레이어 따라감
        GetHit, //피격
        Attack, // playerAttack 클레스에서 공격 불러서 공격함
        ReadyToAttack,
        Die
    }
    public EnemyState enemyState = EnemyState.Idle;
    #endregion
    
    GameObject target;
    GameObject[] playerArr = new GameObject[4];
    Vector3 dirFromEnemyToTarget; //적에서 타깃 방향
    public float enemySpeed = 3; //이동속도
    void Awake()
    {
    }
    void Start()
    {
        playerArr = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        //Debug.Log("Enemy state is : " + enemyState);
        // 컨트롤 중인 플레이어를 타깃으로 설정
        for (int i = 0 ; i < playerArr.Length; i++)
        {
            //플레이어가 들어있는 배열의 원소에 달려있는 PlayerBase의 상태가 active면
            if (playerArr[i] != null
             && playerArr[i].gameObject.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Active)
            {
                target = playerArr[i].gameObject;
            }
        }

        if (target) //타겟으로의 방향 업데이트
        {
            dirFromEnemyToTarget = target.transform.position - transform.position;
            dirFromEnemyToTarget.Normalize();
        }

        switch (enemyState)
        {
            case (EnemyState.Idle):
                UpdateIdle();
                break;
            case (EnemyState.Follow):
                UpdateFollow();
                break;
            case (EnemyState.GetHit):
                UpdateGetHit();
                break;
            case (EnemyState.ReadyToAttack):
                UpdateReadyToAttack();
                break;
            case (EnemyState.Attack):
                UpdateAttack();
                break;
            case (EnemyState.Die):
                UpdateDie();
                break;

        }
    }
    void UpdateIdle()
    {
        if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
            enemyState = EnemyState.Follow;
    }
    public float attackRange = 2f; // 에너미 형태에 따라 공격 범위 달라짐
    public static bool startFollow = false;
    void UpdateFollow() 
    {
        if (startFollow)
        {
            startFollow = false;
            GameManager.Instance.enemyCount++;
        }
        // 타깃 방향으로 회전
        transform.rotation
         = Quaternion.Lerp(transform.rotation,
          Quaternion.LookRotation(dirFromEnemyToTarget),
           Time.deltaTime * 10);
        //임시@!@!@!@!!@#!@#$%@#$%$#@%^$
        //transform.position += dirFromEnemyToTarget * enemySpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange) //공격 범위 안에 들어오면
        {
            enemyState = EnemyState.ReadyToAttack;
        }

    }
    float currentTime = 0;
    void UpdateReadyToAttack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 2)
        {
            enemyState = EnemyState.Attack;
            currentTime = 0;
        }
    }
    int tempDamage = 100; //프로토타입 임시 데미지;
    public bool beingAttackedByPlayerOnControl = false;
    void UpdateAttack()
    {
        if (target.GetComponent<PlayerHP>().HP > tempDamage)
        {
            target.GetComponent<PlayerHP>().HP -= tempDamage;
        }
        else if (target.GetComponent<PlayerHP>().HP <= tempDamage)
        {
            
            target.GetComponent<PlayerBase>().playerState = PlayerBase.PlayerState.Die;
        }
        //프로토타입 임시용 , 한 대 때리고 
        enemyState = EnemyState.Follow;
    }
    void UpdateGetHit()
    {
        //프로토타입 때는 임시로 투명큐브로
        
    }

    // //DamageManager에서 보냄
    // virtual public int Damage
    // {
    //     get {return Damage;}
    //     set 
    //     {
    //         Damage = value;

    //     }
    // }
    void UpdateDie()
    {
        
        Destroy(gameObject);
    }

}
