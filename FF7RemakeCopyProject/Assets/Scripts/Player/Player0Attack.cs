using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player0Attack : PlayerAttackBase
{
    // 플레이어 공격 관리, 함수만 만들어 놓고, 플레이어base 혹은 ai에서 알아서 부르도록
    // Start is called before the first frame update
    #region 프로토타입 임시
    public override void WeekAttack(GameObject target)
    {
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
        int randomDamage = UnityEngine.Random.Range(100, 200);
        DamageManager.Instance.CalculateDamage(gameObject, randomDamage, target.GetComponent<EnemyHealth>());
    }
    #endregion
    
}
