using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBase : MonoBehaviour
{
    // 이근기 임시 변수
    


    // 플레이어 공격 관리, 함수만 만들어 놓고, 플레이어base 혹은 ai에서 알아서 부르도록
    // Start is called before the first frame update
    #region 프로토타입 임시
    protected Ray ray;
    protected RaycastHit hitInfo;
    virtual public void WeekAttack(GameObject target)
    {
        
    }
    protected GameObject explosionFactory;
    protected void Start()
    {
        explosionFactory = (GameObject)Resources.Load("Exploson4");
    }
    #endregion
    
    protected bool attacking = false;
    float currentTime = 0 ;
    IEnumerator IEMakeSlowDuringStrongAttack(GameObject target) //순간적으로 느리게
    {
        GameObject explosion = Instantiate (explosionFactory);
        explosion.transform.position = target.transform.position;
        GameManager.Instance.MakeSlow();
        yield return new WaitForSecondsRealtime(0.9f); // timescale 조절하기 때문에 실제 시간으로
        GameManager.Instance.BringItBackFromSlow();
        int randomDamage = UnityEngine.Random.Range(300, 400);
        DamageManager.Instance.CalculateDamage(gameObject, randomDamage, target.GetComponent<EnemyHealth>());
    }
    virtual public void StrongAttack(GameObject target)
    {
        Debug.Log("StrongAttack");
        //공격, 피깎기(아래 코루틴에서) 
        StartCoroutine(IEMakeSlowDuringStrongAttack(target));
    }
    virtual public void AbilityAttack(GameObject target)
    {
        //
        Debug.Log("AbilityAttack");
    }
    virtual public void MateriaAttack(GameObject target)
    {
        //
        Debug.Log("MateriaAttack");
    }
    private void Update()
    {
        
    }
    IEnumerator IEAttack()
    {
        currentTime = 0;
        // 1초 안에 무기가 적에 닿으면 공격 성공
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            attacking = true;
            yield return null;
        }
        attacking = false;
    }

}
