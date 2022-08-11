using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player0Attack : PlayerAttackBase
{
    #region 근기 꺼
    // (1) 단축키 눌렀을 때 로그 뜨는지 확인
    // 1. 약공격, 어빌리티, 마테리얼어택의 단축키를 지정한다.
    // 2. 만약 지정한 attack, ability, materialA 키가 눌린다면, 로그가 뜬다.
    //    - 지정한 함수를 호출한다. (함수 내부에 로그가 있음으로)


    // (2) 애니메이션 찾고 그에 맞는 모션 구현
    // (3) 약공격은 - 왼쪽버튼 눌렀을 때 호출 (호출까지는 이미 구현되있음)
    // (4) 약공격 모션, 어빌리티 마테리아스킬 3가지
    // (5) 공격판정 어떻게 할지, 무기가 직접 적에게 닿았을 때 성공.
    // (6) ui 통해서 호출했을 때 로그가 뜨는지 확인하기 (보완)
    // (7) 피격판정, 데미지 등 일단 무시
    #endregion

    // 때리는 것이 가능한 적과 나의 거리 ( 무기휘둘렀을 때의 반경이 적절하게 고려되야함 )
    public float Attackable_Distance;
    

    private void Update()
    {
        // 1. 공격해서 범위 안에 적이 없는 경우 WeekAttack 은 함수는 발동하지만
        //    적에게 데미지가 들어가지 않는 것임
        // 2. 공격해서 범위 안에 적이 있는 경우 WeekAttack 함수가 발동하며, 데미지도 함께 들어갑니다. 
        //    에너미는 PlayerBase에서 일정 조건을 만족 시, List에 존재하는 적의 수 (0, 1, 2 등 index 넘버로 정의됨)
        //    
    }


    public override void WeekAttack(GameObject target)
    {
        #region 영재 주석 ( 강공격 )
        //버튼을 누를 때 시간을 재기 시작하여 0.5초이상 누르고 1초내에 떼어 다시 0.5초 내에 버튼을 누르면 강공격을 한다.
        // 0.5초 미만에서 떼면 0초~0.5초 사이에 몇번을 누르던 약공격한번,
        //1초 넘게 누르고 있으면 최초 약 공격 한번만
        //0.5초 ~ 1초 사이동안 누르기 성공 후 0.5초 내에 버튼을 누르면 강공격 0.5초 이후에 버튼을 누르면 약공격
        // if(Physics.Raycast(transform.position, transform.forward, out hitInfo))
        // {
        //     Debug.Log("임시 weekattack"); 
        //     if (hitInfo.collider.tag == "Enemy")
        //     {
        //         int randomDamage = UnityEngine.Random.Range(100, 200);
        //         DamageManager.Instance.CalculateDamage(gameObject, randomDamage, target.GetComponent<EnemyHealth>());
        //     }
        // }
        //int randomDamage = UnityEngine.Random.Range(100, 200);
        //DamageManager.Instance.CalculateDamage(gameObject, randomDamage, target.GetComponent<EnemyHealth>());
        #endregion

       
        Debug.Log("WeekAttack");
    }


    public override void AbilityAttack(GameObject target)
    {
        Debug.Log("AbilityAttack");
    }
    
    public override void MateriaAttack(GameObject target)
    {
        Debug.Log("MateriaAttack");
    }
    
}
