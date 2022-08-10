using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public enum CameraState
    {
        Idle,
        BattleStage
    }
    public CameraState cameraState = CameraState.Idle;
    public static CameraManager Instance;
    void Awake()
    {
        Instance = this;
    }
    GameObject[] playerArr = new GameObject[4]; //플레이어들 담을 그릇
    GameObject playerOnControl;

    static float screenHeight; // 화면의 세로길이 meter단위
    static float meterPerPixel; // 한 픽셀이 몇 미터
    static float screenWidth; // 화면의 가로길이 meter단위
    float screenBoxLeftPoint;
    float screenBoxRightPoint;
    void Start()
    {
        playerArr = GameObject.FindGameObjectsWithTag("Player"); //플레이어를 찾아서 집어넣는다

        screenHeight = Camera.main.orthographicSize * 2; //미터단위
        meterPerPixel = screenHeight / Screen.height;
        screenWidth = Screen.width * meterPerPixel; //미터단위

        screenBoxLeftPoint = screenWidth * 0.3f;
        screenBoxRightPoint = screenWidth * 0.7f;
    }


    Vector3 playerPositionOnScreen; //카메라 상의 플레이어 위치 좌표, 죄측 하단이 0,0,0, meter단위로 변환해야
    void Update()
    {
        for (int i = 0 ; i < playerArr.Length; i++)
        {
            //플레이어가 들어있는 배열의 원소에 달려있는 PlayerBase의 상태가 active면
            if (playerArr[i] != null && playerArr[i].gameObject.GetComponent<PlayerBase>().playerState == PlayerBase.PlayerState.Active)
            {
                playerOnControl = playerArr[i].gameObject;
                playerPositionOnScreen = Camera.main.WorldToScreenPoint(playerOnControl.transform.position) * meterPerPixel; //1920 1080
            }
        }
        
        switch(cameraState)
        {
            case CameraState.Idle:
                UpdateIdle();
                break;
            case CameraState.BattleStage:
                UpdateBattleStage();
                break;
        }
        //배틀 스테이지는 코너 취급
        if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
            isCorner = true;
    }
    Vector3 playerLastPosition; //임시로 저장할 위치
    //카메라가 플레이어 따라갈 때 앞뒤 거리
    public float distanceFromPlayerToCamera = 5f;
    public float cameraHeight = 5f;
    // public bool isCorner;
    public bool isCorner = false;
    public bool hasItStartedCornerCamera = false;
    Vector3 cameraPosition; // 여기에 변경되는 값 저장하고 최종적으로 transform.position = cameraPosition
    
    public GameObject cube; //임시, 코너에서 카메라가 실제로 따라가는 위치
    Vector3 playerBackPositionWhileCorner;
    IEnumerator IEFollowCharacterLate()
    {
        yield return new WaitForSeconds(1f);
        transform.position = playerBackPositionWhileCorner;

    }
    void UpdateIdle()
    {
        if (!isCorner) //코너가 아닌 일반 길이면 절대방향으로
        {
            switch (GameManager.Instance.gameDirection)
            {
                case GameManager.GameDirectionGlobal.Forward:
                    transform.forward = Vector3.forward;
                    break;
                case GameManager.GameDirectionGlobal.Back:
                    transform.forward = Vector3.back;
                    break;
                case GameManager.GameDirectionGlobal.Left:
                    transform.forward = Vector3.left;
                    break;
                case GameManager.GameDirectionGlobal.Right:
                    transform.forward = Vector3.right;
                    break;
            }
            // 플레이어 포지션(y, z만) - 절대방향 * 거리
            cameraPosition
            = new Vector3(transform.position.x, playerOnControl.transform.position.y, playerOnControl.transform.position.z)
             - transform.forward * distanceFromPlayerToCamera
             + transform.up * cameraHeight;
            
            // 화면의 좌우 범위 벗어나면 그만큼 더하거나 뺀다
            if (playerPositionOnScreen.x < screenBoxLeftPoint) //왼쪽을width *  벗어나면
            {
                //카메라의 x좌표를 플레이어가 벗어난 만큼 뺀다
                cameraPosition.x -= Mathf.Abs(playerPositionOnScreen.x - screenBoxLeftPoint);
            }
            else if (playerPositionOnScreen.x > screenBoxRightPoint)
            {   
                //카메라의 x좌표를 플레이어가 벗어난 만큼 더한다
                cameraPosition.x += Mathf.Abs(playerPositionOnScreen.x - screenBoxRightPoint);
            }
            
            transform.position = cameraPosition;
            transform.eulerAngles = new Vector3(40, 0, 0);
        }
        else // 코너라면 
        {
            //카메라가 뒤늦게 따라가야 할 위치
            playerBackPositionWhileCorner = playerOnControl.transform.position - playerOnControl.transform.forward * distanceFromPlayerToCamera
                     + playerOnControl.transform.up * (cameraHeight-2);
            transform.eulerAngles = new Vector3(20, 0, 0);
            //플레이어 뒤쪽에 기준을 두고 그 기준을 0.몇 초 뒤에 따라간다
            //코너길 처음 진입할 때 한 번만 카메라를 플레이어 뒤에 위치시킨다
            // if (!hasItStartedCornerCamera)
            // {
            //     hasItStartedCornerCamera = true;
            //     transform.position = playerBackPositionWhileCorner;
            // }
            transform.LookAt(playerOnControl.transform.position);
            //transform.position += (playerBackPositionWhileCorner - transform.position) * 1.3f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, playerBackPositionWhileCorner, Time.deltaTime * 2);
            cube.transform.position = playerBackPositionWhileCorner; ////////////////////임시
        }

    }
    GameObject player_sTarget;
    //이 거리 이내에 있으면 적을 바라보고 밖이면 플레이어 바라봄
    public float lookTargetRange = 5f;
    float distanceFromPlayerToEnemey;
    Vector3 dirFromCameraToTarget;
    Vector3 dirFromCameraToMiddlePosition;
    Vector3 cameraPosition2; //SetBattleCameraPositionWithTarget 에서 위친
    void SetBattleCameraPositionWithTarget(Vector3 camPosition)
    {
        //플레이어가 박스를 벗어나려고 할 때 좌우로 움직임
        camPosition = new Vector3(transform.position.x, playerOnControl.transform.position.y, playerOnControl.transform.position.z)
             - transform.forward * distanceFromPlayerToCamera;

        // 화면의 좌우 범위 벗어나면 그만큼 더하거나 뺀다
        if (playerPositionOnScreen.x < screenBoxLeftPoint) //왼쪽을width *  벗어나면
        {
            //카메라의 x좌표를 플레이어가 벗어난 만큼 뺀다
            camPosition.x -= Mathf.Abs(playerPositionOnScreen.x - screenBoxLeftPoint);
        }
        else if (playerPositionOnScreen.x > screenBoxRightPoint)
        {   
            //카메라의 x좌표를 플레이어가 벗어난 만큼 더ㅇ한다
            camPosition.x += Mathf.Abs(playerPositionOnScreen.x - screenBoxRightPoint);
        }
        
        transform.position = camPosition;
    }
    Vector3 MiddlePosition(GameObject playerOnControl, GameObject target)
    {
        Vector3 middlePosition;
        middlePosition = 0.5f * new Vector3(playerOnControl.transform.position.x + target.transform.position.y,
         playerOnControl.transform.position.x + target.transform.position.y,
          playerOnControl.transform.position.z + target.transform.position.z);

        return middlePosition;
    }
    void UpdateBattleStage()
    {
        player_sTarget = playerOnControl.GetComponent<PlayerBase>().target;
        // 플레이어와 타겟의 거리가 n 이하일 때(방향은 타겟쪽으로, 플레이어의 움직임에 따라 좌우로, 위치는 플레이어 좌우 박스 벗어날 때만)
        // 플레이어와 타겟의 거리가 n초과할 때 (방향을 플레이어 쪽으로 , 위치는 플레이어 좌우 박스 벗어날 때만, 타겟이 화면 벗어나려고
        // 하면 플레이어 -> 타겟방향 보는 위치로)
        //카메라 회전:
        //타겟이랑 플레이어 가까울때: 스킬 안쓰면 적을 바라보고, 스킬 쓸 때만 플레이어 바라봄
        //타겟이랑 플레이어랑 적당한 거리: 적과 플레이어 중간 지점을 바라봄

        //타겟이 카메라 벗어났을 때, 카메라를 플레이어 등 위에 위치시키고 중간 지점 
        //카메라 위치 : 피격 시 다른 법칙 싹 다 무시하고 플레이어 밀려나는 방향과 동일하게 위치 이동, 플레이어 바라봄
        //플레이어와 타겟 일정 각도 유지하도록 이동 -> c

        
        // if (player_sTarget) //타겟이 지정되어 있으면
        // {
        //     dirFromCameraToMiddlePosition = MiddlePosition(playerOnControl, player_sTarget) - transform.position;
        //     dirFromCameraToMiddlePosition.Normalize();
        //     // dirFromCameraToTarget = player_sTarget.transform.position - transform.position;
        //     // dirFromCameraToTarget.Normalize();
        //     // distanceFromPlayerToEnemey = Vector3.Distance(playerOnControl.transform.position, player_sTarget.transform.position);
        //     // // 플레이어랑 타겟이랑 거리가 멀면 플레이어 플레이어 보도록
        //     // if (distanceFromPlayerToEnemey > lookTargetRange)
        //     // {
        //     //     transform.rotation
        //     //      = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirFromCameraToTarget), 2f * Time.deltaTime);

        //     // }
        //     // else if (distanceFromPlayerToEnemey < lookTargetRange) //가까우면
        //     // {
        //     //     cameraPosition
        //     //      = new Vector3(transform.position.x, playerOnControl.transform.position.y, playerOnControl.transform.position.z)
        //     //     - transform.forward * distanceFromPlayerToCamera
        //     //     + transform.up * cameraHeight;
        
        //     // // 화면의 좌우 범위 벗어나면 그만큼 더하거나 뺀다
        //     // if (playerPositionOnScreen.x < screenBoxLeftPoint) //왼쪽을width *  벗어나면
        //     // {
        //     //     //카메라의 x좌표를 플레이어가 벗어난 만큼 뺀다
        //     //     cameraPosition.x -= Mathf.Abs(playerPositionOnScreen.x - screenBoxLeftPoint);
        //     // }
        //     // else if (playerPositionOnScreen.x > screenBoxRightPoint)
        //     // {   
        //     //     //카메라의 x좌표를 플레이어가 벗어난 만큼 더한다
        //     //     cameraPosition.x += Mathf.Abs(playerPositionOnScreen.x - screenBoxRightPoint);
        //     // }
        //     // transform.rotation
        //     //      = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirFromCameraToTarget), 2f * Time.deltaTime);
        //     // }
        //     SetBattleCameraPositionWithTarget(cameraPosition2); //플레이어가 좌우로 벗어날 때만 좌우로 움직이기

        //     // transform.rotation
        //     //       = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirFromCameraToMiddlePosition), 2f * Time.deltaTime);
        //     transform.LookAt(MiddlePosition(playerOnControl, player_sTarget));

        // }
        //카메라가 뒤늦게 따라가야 할 위치
            playerBackPositionWhileCorner = playerOnControl.transform.position - playerOnControl.transform.forward * distanceFromPlayerToCamera
                     + playerOnControl.transform.up * (cameraHeight-2);
            transform.eulerAngles = new Vector3(20, 0, 0);
            //플레이어 뒤쪽에 기준을 두고 그 기준을 0.몇 초 뒤에 따라간다
            //코너길 처음 진입할 때 한 번만 카메라를 플레이어 뒤에 위치시킨다
            if (!hasItStartedCornerCamera)
            {
                hasItStartedCornerCamera = true;
                transform.position = playerBackPositionWhileCorner;
            }
            transform.LookAt(playerOnControl.transform.position);
            //transform.position += (playerBackPositionWhileCorner - transform.position) * 1.3f * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, playerBackPositionWhileCorner, Time.deltaTime*2);
            cube.transform.position = playerBackPositionWhileCorner; ////////////////////임시
        
        //transform.position = cameraPosition;
        //transform.eulerAngles = new Vector3(40, 0, 0);
    }
    // 배틀 스테이지, 타겟이 있을 때 카메라 위치
    
}