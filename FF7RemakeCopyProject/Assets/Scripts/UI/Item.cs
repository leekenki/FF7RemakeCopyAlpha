using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Item 개개별의 Data를 다룬다.


public class Item : MonoBehaviour
{
    #region 개별 아이템이 보유해야 하는 정보변수 선언
    private string Item_Name;
    private string Item_Description_Info;
    private string Item_Property;
    private Sprite Item_Image;
    #endregion

    #region 개별아이템이 보유해야 하는 정보 변수 중 Dictionary를 위한 정보 변수 선언
    public Dictionary<string, int> Item_StatsData_Dic = new Dictionary<string, int>();   

    // Item 의 Stat 은 Dictionary 형태로 따로 관리한다.
    // Dictionary 의 명칭은 Item_StatsData_Dic 으로 한다.
    // Item_StatsData_Dic 에 들어갈 자료의 Key 값은 다음과 같다.
    private string Item_Physical_Damage;
    private string Item_Magic_Damage;
    private string Item_Number;
    private string Item_Equipmentable;
    private string Item_Consumable;
    private string Slot_Num;
    private string Item_Equiped_State;
    #endregion
      
    #region Item Class 에 매개변수를 설정하고, 설정된 형식에 따라 Item Class에 정보를 입력한다.
    public Item(string Item_Name, string Item_Description_Info, string Item_Property, Sprite Item_Image, Dictionary<string, int> Item_StatsData_Dic)
    {
        this.Item_Name = Item_Name;
        this.Item_Description_Info = Item_Description_Info;
        this.Item_Property = Item_Property;
        this.Item_Image = Item_Image;
        this.Item_StatsData_Dic = Item_StatsData_Dic;
    }
    #endregion

    #region 개별 item에 대한 정보를 받아 올 수 있도록 오버로딩(?) 시킨다.
    public Item(Item item)
    {
        this.Item_Name = item.Item_Name;
        this.Item_Description_Info = item.Item_Description_Info;
        this.Item_Property = item.Item_Property;
        this.Item_Image = item.Item_Image;
        this.Item_StatsData_Dic = item.Item_StatsData_Dic;
    }
    #endregion

    // private 에 담겨 있는 개별적인 자료에 접근하기 위하여 프로퍼티를 사용

    private void Start()
    {
        #region 개별 Class의 Item_StatsData_Dic 에  Data를 삽입한다.
        // Item_StatsData_Dic 에 쌍으로 구성된 Key, Value 값 넣기.
        Item_StatsData_Dic.Add(Item_Physical_Damage, 0);
        Item_StatsData_Dic.Add(Item_Magic_Damage, 0);
        Item_StatsData_Dic.Add(Item_Number, 0);
        Item_StatsData_Dic.Add(Item_Equipmentable, 0);
        Item_StatsData_Dic.Add(Item_Consumable, 0);
        Item_StatsData_Dic.Add(Slot_Num, 0);
        Item_StatsData_Dic.Add(Item_Equiped_State, 0);
        #endregion
    }

    private void Update()
    {

    }
}
