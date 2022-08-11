using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3가지 상태(active, inactive, die), 기본 움직임만 구현
public class PlayerBase : MonoBehaviour
{
    public enum PlayerState
    {
        Active,
        Inactive,
        Die
    }
    public PlayerState playerState;
    Vector3 direction;
    Vector3 directionInCorner;
    public float moveSpeed;
    public float walkSpeed = 5f;
    public float runSpeed = 6f;
    public float gravity = -9f;
    public float jumpPower = 3f;

    CharacterController cc;

    virtual protected void Start()
    {
        moveSpeed = walkSpeed;
        cc = GetComponent<CharacterController>();
        playerState = PlayerState.Inactive; // 플레이어 부모클레스는 inactive, 상속받은 주인공만 active로
    }
    float x, y, z;
    float finalX, finalZ;
    bool isJumping = false; //점프중인가
    int jumpCount = 0; //점프 몇 번 했나
    virtual protected void Move()
    {
        if (canMove)
        {

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            //점프를 2번 미만으로 한 상태에서 스페이스를 누르면 y값 증가
            //땅에 붙어있는 상태에서 첫 번째 점프 가능
            if (cc.collisionFlags == CollisionFlags.Below && Input.GetKeyDown(KeyCode.Space) && !isJumping && jumpCount == 0)
            {
                isJumping = true;
                y = jumpPower; //중력값을 계속 받고 있기 때문에 += 가 아닌 아예 =로 초기화
                jumpCount ++;
            }
            //이미 한번 점프를 한 상태에서, 총 점프횟수가 2미만일 때 점프 누르면 이단점프 가능,
            // 위의 if 문 실행 후 순차적으로 중복실행될 수 있기 때문에 else if 로
            else if (isJumping && jumpCount < 2 && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                y += jumpPower;
                jumpCount ++;
            }
            //점프중이든 아니든 땅에 내려오면
            else if (cc.collisionFlags == CollisionFlags.Below)
            {
                y = 0;
                jumpCount = 0;
                isJumping = false;
            }
            y += gravity * Time.deltaTime;


            direction = new Vector3 (x, 0, z); //집어넣음
            direction.Normalize();
            // if (direction.magnitude > 0)
            // {
            //     transform.forward = new Vector3(direction.x, 0, direction.z); //방향을 맞춘다
            // }
            direction.y = y;

            if (Input.GetKey(KeyCode.R))
            {
                moveSpeed = runSpeed;
            }
            else
                moveSpeed = walkSpeed;

            //길이 코너가 아닐 때
            if (!CameraManager.Instance.isCorner)
            {
                if (direction.x != 0 || direction.z != 0) //움직일 때만, (중력값 때문에 dir.magnitude 사용 X)
                {
                    transform.forward = new Vector3(direction.x, 0, direction.z); //방향을 맞춘다
                }
                
                //방향이 바뀌면 0.몇초 동안 속도를 줄인다. (체크중인 방향과 플레이어의 방향이 다르면 속도 줄임)
                if (tempDirection.x != transform.forward.x || tempDirection.z != transform.forward.z)
                {
                    StartCoroutine(IESlowDownSpeed());
                }
                else if (tempDirection == transform.forward)
                {
                    walkSpeed = 5;
                }
                direction.Normalize();

                
                cc.Move(direction * moveSpeed * Time.deltaTime);
                //cc.SimpleMove(direction * moveSpeed * Time.deltaTime);
            }
            else if (CameraManager.Instance.isCorner) //코너일 때 카메라랑 연동
            {
                //wasd로 방향만 설정하고, w누를 때만 앞으로 가기
                Vector3 camForwardAngle = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                Vector3 forwardAngle =new Vector3(transform.forward.x, 0, transform.forward.z);
                //카메라와 앞방향과 플레이어의 앞방향 사이의 각도가 아주 적을 때
                // if(Vector3.Angle(camForwardAngle, forwardAngle) < 10)
                // {
                //     // w를 누르면 카메라의 앞방향을 가고 싶다// 쿼터니언 먼저 곱하고 백터 곱하기
                //     // w 누르면 월드 좌표의 z방향을 가던 것을 카메라의 앞방향으로 가고 싶다. (x,0,z) 
                //     // direction = Quaternion.Euler(0,Vector3.Angle(camForwardAngle,forwardAngle),0) * direction;
                //     transform.forward = camForwardAngle;
                // }
                // a d누르면 각도 점차 회전, w 누를 때만 앞으로
                //transform.forward = Vector3.Lerp()
                if (Input.GetKey(KeyCode.A))
                {
                    timeForRotateScale += Time.deltaTime;
                    //a를 누르면 좌측으로 살짝회전
                    transform.forward = Quaternion.Euler(0, -timeForRotateScale * 100, 0) * transform.forward;
                }else timeForRotateScale = 0;
                if (Input.GetKey(KeyCode.D))
                {
                    timeForRotateScale += Time.deltaTime;
                    //d를 누르면 우측으로 살짝회전
                    transform.forward = Quaternion.Euler(0, timeForRotateScale * 100, 0) * transform.forward;
                }else timeForRotateScale = 0;
                
                if (Input.GetKey(KeyCode.W))
                {
                    walkSpeed = 5;
                }
                else walkSpeed = 0;
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.forward *= -1;
                    // 회전하는 모션
                    // 몇초 기다리고
                    // 카메라 회전
                }

                cc.Move(transform.forward * walkSpeed * Time.deltaTime);
            }
            // direction.Normalize();
            tempDirection = transform.forward;
            // cc.Move(direction * moveSpeed * Time.deltaTime);\
        }
    }
    GameObject camPositionObject;
    Vector3 direction2;
    Vector3 tempDirection;
    float timeForRotateScale = 0;
    //bool isLookingXFirst = false; //대각선을 보고 있나
    //캐릭터가 회전해서 속도가 줄어진 위치부터 얼마나 앞을 가는지 체크하기 위한 위치 변수
    //Vector3 FirstPositionLookingX; 
    // IEnumerator IECheckXDistance()
    // {
    //     while (Vector3.Distance(FirstPositionLookingX, transform.position) < 0.6f)
    //     {
    //         yield return null;
    //     }
    //     walkSpeed = 3;
    // }
    IEnumerator IESlowDownSpeed()
    {
        //Debug.Log("Slow Down");
        walkSpeed = 4f;
        yield return new WaitForSeconds(0.8f);

    }

    Vector3 lastDir;
    public bool canMove = true;
    protected virtual void Update()
    {
        Move();

        switch(playerState)
        {
            case PlayerState.Active:
                UpdateActive();
                break;
            case PlayerState.Inactive:
                canMove = false;
                break;
            case PlayerState.Die:
                Destroy(gameObject, 1);
                //죽는 거 나중에 보충
                break;
        }
        //if active, 공격 가능

        if (isCountingAttackTime)
        {
            attackTime += Time.deltaTime;
        }
    }
    
    public List<GameObject> activeEnemyList = new List<GameObject>();
    GameObject[] tempEnemyArr;
    float nearestDistanceToEnemy = 100;
    float distanceToEnemy;
    public GameObject target; //타겟팅 UI에서 이용해야 하기 때문에 public
    protected virtual void UpdateActive()
    {
        if (GameManager.Instance.flowState == GameManager.GameFlow.Idle
            || GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
            {
                canMove = true;
            }
        //타겟이 없을 때 타겟 찾기
        if (!target)
        {
            // 배틀페이즈에서, 수시로 가까운 적을 체크
            if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
            {
                // find로 enemy 찾고 임시 배열에 넣는다.
                tempEnemyArr = GameObject.FindGameObjectsWithTag("Enemy"); 
                for (int i = 0; i < tempEnemyArr.Length; i++)
                {
                    //리스트에 임시 배열에 해당하는 원소가 없으면, 즉 새로운 원소라면 리스트에 추가
                    if (!activeEnemyList.Contains(tempEnemyArr[i])) 
                    {
                        //Debug.Log("new member");
                        activeEnemyList.Add((tempEnemyArr[i]));
                    }
                }
                //for (int i = 0; i < activeEnemyList.Count; i++)
                foreach (GameObject enemy in activeEnemyList)
                {
                    distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //error: already destroied
                    if (distanceToEnemy <= nearestDistanceToEnemy)
                    {
                        nearestDistanceToEnemy = distanceToEnemy;
                        target = enemy;
                    }
                }
                // for (int i = 0; i < activeEnemyList.Count; i++)
                // {
                //     distanceToEnemy = Vector3.Distance(transform.position, activeEnemyList[i].transform.position); //error: already destroied
                //     if (distanceToEnemy <= nearestDistanceToEnemy)
                //     {
                //         nearestDistanceToEnemy = distanceToEnemy;
                //         target = activeEnemyList[i];
                //     }
                // }
                if (activeEnemyList.Count == 1)
                {
                    target = activeEnemyList[0];
                }
                if (GameManager.Instance.enemyCount <= 0)
                {
                    target = null;
                }
            }
        }
        //타겟이 있을 때 공격 가능
        else if (target)
        {
            //타겟이 있는 상태고 적이 2마리 이상 있을 때 탭을 누르면 타겟 바꾸기
            if (Input.GetKeyDown(KeyCode.Tab) && activeEnemyList.Count >= 2)
            {
                target = activeEnemyList[UnityEngine.Random.Range(0, activeEnemyList.Count)];
            }
            
            // if(Input.GetButtonDown("Fire1"))
            // {
            //     target.GetComponent<EnemyBase>().beingAttackedByPlayerOnControl = true;
            //     CallAttackFunction();
            // }
            #region 약공격, 강공격
            if (Input.GetButtonDown("Fire1") && normalAttackCount <= 0)
            {
                CallAttackFunction(0); //약공격
                isCountingAttackTime = true;
                normalAttackCount++;
            }
            //첫 번째 공격 후 손가락을 뗐는데 0.5초 미만이면 강공격 발동조건 X
            else if (Input.GetButtonUp("Fire1") && normalAttackCount <= 1 && attackTime < 0.5f)
            {
                isCountingAttackTime = false;
                normalAttackCount = 0;
                attackTime = 0;
            }
            // 버튼을 뗐는데 이미 한번 공격한 상황이고 시간이 적절하면
            else if (Input.GetButtonUp("Fire1") && normalAttackCount <= 1 && (0.5f < attackTime && attackTime <= 1.4f))
            {
                attackTime = 0;
            }
            //강공격 발동조건 갖춘 상태에서 0.5초 이내에 버튼을 누르면 강공격 호출
            else if(Input.GetButtonDown("Fire1") && normalAttackCount >= 1 && attackTime <= 0.5f) 
            {
                CallAttackFunction(1);
                normalAttackCount = 0;
                attackTime = 0;
            }
            //두번째 공격 타이밍 놓치면 약공격 취급
            else if(Input.GetButtonDown("Fire1") && normalAttackCount >= 1 && attackTime > 0.5f) 
            {
                CallAttackFunction(0); //약공격
                normalAttackCount = 0;
                attackTime = 0;
            }
            #endregion
            
        }
        
         //게임 매니저에 현재 오브젝트가 컨트롤중이라고 알림
        GameManager.Instance.playerOnControl = gameObject;
    }
    public int normalAttackCount = 0;
    public float attackTime = 0;
    public bool isCountingAttackTime = false;
    virtual protected void CallAttackFunction(int num){}
    public bool canAttack = true;
}
