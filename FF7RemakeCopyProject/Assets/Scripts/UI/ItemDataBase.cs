using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ������ ���� �����Ͽ� ������ �ִ� Class �� �ʿ��ϴ�.
// 1. Class �� List ������, �԰�� Item �� �ڷ���� ������ ������, �ڷ����
// 2. ��ġ�ϴ� ���� Item �� Value ���� �����Ѵ�.

public class ItemDataBase : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 1. ��� �� �Ҹ�Ǵ� �������� �����ִ� �Լ� �ۼ��ϱ�
    void Consuming_Item()
    {
        #region �ӽ��ּ�
        // 1. Player �� ������ ��
        // if (GameManager.Instance.flowState != GameManager.GameFlow.BattleStage)
        // {
        //      // 2. ��Ȱ��ȭ�� ���� UI���� 
        //      if(Panel_Item_Selected.isActiveAndEnabled == true)
        //      {
        //          // 3. ��밡���� �Ҹ� �������̰� �� ���� 1�̸鼭��, ������ 0���� ū �����۸��� Ȯ���Ͽ� 
        //          foreach(string Key in Item_StatsData_Dic.Keys)
        //          {
        //              if(Key == Item_Consumable && (Item_StatsData_Dic[Item_Consumable] == 1) && (Item_StatsData_Dic[Item_Number] > 0))
        //              {
        //                  // ������ ���Ű�� ������,
        //                  if(Input.GetKeyDown("Fire1"))
        //                  {
        //                      // �Ҹ� �������� ������ 1 ���ҽ�Ų��.
        //                      Item_StatsData_Dic[Item_Number] -= 1;
        //
        //                      // �Ҹ� �������� �����Ӽ��� ����Ͽ� �������� ȿ���Լ��� �ߵ��ϵ��� �Ѵ�.
        //                      Use_Effect_Item();
        //                  }
        //              }
        //          }          
        //      }
        // }
        #endregion

    }
    #endregion

    #region 2. ������ ȹ�� �� ������ �ֱ� => ������ ȹ�� �Լ� �ۼ�
    void Add_Item_Inventory()
    {
        // �� ���� �� �������̶� �ε����ų� �浹�ϴ� ���(�ڽ����) or ���� �� ������ ����Ǹ� �������� ȹ���Ѵ�. 

        // ���� �κ��丮�� Item�� �ִ� ���,
        // ==> ������ �����۵鿡 �̹� �⺻������ 0,0,0,0,0,0,0,0,0 ���� �� �� �ֱ� ������ ���� �ٲ������ ? ==> No. Num �� �ٲ��ָ� �˴ϴ�.

        // ���� ��� 
        // ������ �������� ������ �����ϰ� ���� �ʽ��ϴ�.
        // �׷��� Item ���� ������ �����Ӽ��� ������ ���� ��������.....
        // �������� �����Ѵ�.

        // ȹ���� �������� Item_StatsData_Dic �� ������ �߰��ȴ�.
        // �̶� Equipmentable, Comsumable �� �������. ������ ������ �߰��Ǹ� �ȴ�.

    }
    #endregion

    #region 3. ������ ���� �� �ɸ��Ϳ� �ֱ� => �ɸ��� �� ������ ���� ���� �Է��ϱ�
    void Equip_Chracter_Item()
    {

    }
    #endregion

    // 4. ������ ���� �� �������� ����ȿ�� �÷��̾�� ���� => �Լ� �ۼ�
    // -- ����� �������� �����ϴ� ���, �������� ��� PlayerAttackDamage�� ���ݴɷ¿� ���Ѵ�.

    // 5. �Ҹ� ������ ��� �� ȿ������ �Լ� �ۼ��ϱ�
    // -- �Ҹ��� �������� ���뿡 ���� �ٸ� ȿ���� ������.

    void Use_Effect_Item()
    {

    }
}
