using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. Item �������� Data�� �ٷ��.


public class Item : MonoBehaviour
{
    #region ���� �������� �����ؾ� �ϴ� �������� ����
    private string Item_Name;
    private string Item_Description_Info;
    private string Item_Property;
    private Sprite Item_Image;
    #endregion

    #region ������������ �����ؾ� �ϴ� ���� ���� �� Dictionary�� ���� ���� ���� ����
    public Dictionary<string, int> Item_StatsData_Dic = new Dictionary<string, int>();   

    // Item �� Stat �� Dictionary ���·� ���� �����Ѵ�.
    // Dictionary �� ��Ī�� Item_StatsData_Dic ���� �Ѵ�.
    // Item_StatsData_Dic �� �� �ڷ��� Key ���� ������ ����.
    private string Item_Physical_Damage;
    private string Item_Magic_Damage;
    private string Item_Number;
    private string Item_Equipmentable;
    private string Item_Consumable;
    private string Slot_Num;
    private string Item_Equiped_State;
    #endregion
      
    #region Item Class �� �Ű������� �����ϰ�, ������ ���Ŀ� ���� Item Class�� ������ �Է��Ѵ�.
    public Item(string Item_Name, string Item_Description_Info, string Item_Property, Sprite Item_Image, Dictionary<string, int> Item_StatsData_Dic)
    {
        this.Item_Name = Item_Name;
        this.Item_Description_Info = Item_Description_Info;
        this.Item_Property = Item_Property;
        this.Item_Image = Item_Image;
        this.Item_StatsData_Dic = Item_StatsData_Dic;
    }
    #endregion

    #region ���� item�� ���� ������ �޾� �� �� �ֵ��� �����ε�(?) ��Ų��.
    public Item(Item item)
    {
        this.Item_Name = item.Item_Name;
        this.Item_Description_Info = item.Item_Description_Info;
        this.Item_Property = item.Item_Property;
        this.Item_Image = item.Item_Image;
        this.Item_StatsData_Dic = item.Item_StatsData_Dic;
    }
    #endregion

    // private �� ��� �ִ� �������� �ڷῡ �����ϱ� ���Ͽ� ������Ƽ�� ���

    private void Start()
    {
        #region ���� Class�� Item_StatsData_Dic ��  Data�� �����Ѵ�.
        // Item_StatsData_Dic �� ������ ������ Key, Value �� �ֱ�.
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
