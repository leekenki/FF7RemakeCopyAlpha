using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 개별 아이템 정보를 전부 취합하여 가지고 있는 Class 가 필요하다.
// 1. Class 는 List 구조로, 입고된 Item 의 자료명만을 가지고 있으며, 자료명이
// 2. 일치하는 개별 Item 의 Value 값을 조정한다.

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

    #region 1. 사용 시 소모되는 아이템을 없애주는 함수 작성하기
    void Consuming_Item()
    {
        #region 임시주석
        // 1. Player 가 비전투 중
        // if (GameManager.Instance.flowState != GameManager.GameFlow.BattleStage)
        // {
        //      // 2. 비활성화된 전투 UI에서 
        //      if(Panel_Item_Selected.isActiveAndEnabled == true)
        //      {
        //          // 3. 사용가능한 소모성 아이템이고 그 값이 1이면서도, 수량이 0보다 큰 아이템만을 확인하여 
        //          foreach(string Key in Item_StatsData_Dic.Keys)
        //          {
        //              if(Key == Item_Consumable && (Item_StatsData_Dic[Item_Consumable] == 1) && (Item_StatsData_Dic[Item_Number] > 0))
        //              {
        //                  // 아이템 사용키를 누르면,
        //                  if(Input.GetKeyDown("Fire1"))
        //                  {
        //                      // 소모성 아이템의 수량을 1 감소시킨다.
        //                      Item_StatsData_Dic[Item_Number] -= 1;
        //
        //                      // 소모성 아이템의 고유속성을 사용하여 아이템의 효과함수가 발동하도록 한다.
        //                      Use_Effect_Item();
        //                  }
        //              }
        //          }          
        //      }
        // }
        #endregion

    }
    #endregion

    #region 2. 아이템 획득 시 아이템 넣기 => 아이템 획득 함수 작성
    void Add_Item_Inventory()
    {
        // 비 전투 시 아이템이랑 부딪히거나 충돌하는 경우(박스까기) or 전투 시 전투가 종료되면 아이템을 획득한다. 

        // 만약 인벤토리에 Item이 있는 경우,
        // ==> 각각의 아이템들에 이미 기본값으로 0,0,0,0,0,0,0,0,0 으로 다 들어가 있기 때문에 값을 바꿔줘야한 ? ==> No. Num 만 바꿔주면 됩니다.

        // 없는 경우 
        // 장비류의 아이템은 복수로 보유하고 있지 않습니다.
        // 그러나 Item 들은 각각의 고유속성을 가지고 있을 것임으로.....
        // 고유값이 들어가야한다.

        // 획득한 아이템은 Item_StatsData_Dic 에 수량이 추가된다.
        // 이때 Equipmentable, Comsumable 은 상관없다. 아이템 수량만 추가되면 된다.

    }
    #endregion

    #region 3. 아이템 장착 시 케릭터에 넣기 => 케릭터 별 아이템 장착 정보 입력하기
    void Equip_Chracter_Item()
    {

    }
    #endregion

    // 4. 아이템 적용 시 아이템의 스텟효과 플레이어에게 전달 => 함수 작성
    // -- 장비형 아이템을 착용하는 경우, 아이템의 장비를 PlayerAttackDamage의 공격능력에 더한다.

    // 5. 소모성 아이템 사용 시 효과적용 함수 작성하기
    // -- 소모형 아이템의 내용에 따라 다른 효과를 가진다.

    void Use_Effect_Item()
    {

    }
}
