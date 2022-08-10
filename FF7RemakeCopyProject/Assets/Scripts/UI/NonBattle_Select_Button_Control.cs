using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonBattle_Select_Button_Control : MonoBehaviour
{
    public static NonBattle_Select_Button_Control Instance;

    public GameObject Panel_Select;
    public GameObject Item_Info;
    public GameObject Panel_Ability_Selected;
    public GameObject Panel_Skill_Selected;
    public GameObject Panel_Item_Selected;

    private Button Button_Ability_Selected;
    private Button Button_Skill_Selected;
    private Button Button_Item_Selected;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        #region Panel �ʱ�ȭ
        // 1. ���� Panel_Select ���� �ƹ��͵� ���õ��� �ʾ��� ���, �����Ƽ, ��ų, ������ �г��� ��Ȱ��ȭ ��Ų��.
        Panel_Select.SetActive(true);
        Item_Info.SetActive(false);
        Panel_Ability_Selected.SetActive(true);
        Panel_Skill_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);

        #endregion

        #region Button ��������
        Button_Ability_Selected = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        Button_Skill_Selected = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        Button_Item_Selected = transform.GetChild(0).GetChild(2).GetComponent<Button>();
        #endregion

        #region Button onClick Event �����ϱ�
        Button_Ability_Selected.onClick.AddListener(Click_Ability_Button);
        Button_Skill_Selected.onClick.AddListener(Click_Skill_Button);
        Button_Item_Selected.onClick.AddListener(Click_Item_Button);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {

    }


    // ���� Panel_Select ���� Ability Button �� Ŭ���ϴ� ���
    void Click_Ability_Button()
    {
        // Ability_Panel Ȱ��ȭ
        Panel_Ability_Selected.SetActive(true);
        // ������ �г� ���Ƴ���
        Panel_Skill_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);
        
        // �Լ� �ҷ��ͼ� �ֱ�
        print("Ability Button Active");
    }

    // ���� Panel_Select ���� Skill Button �� Ŭ���ϴ� ���

    void Click_Skill_Button()
    {
        // Skill_Panel Ȱ��ȭ
        Panel_Skill_Selected.SetActive(true);
        // ������ �г� ���Ƴ���
        Panel_Ability_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);

        
        print("Skill Button Active");
    }

    // ���� Panel+Select ���� Item Button �� Ŭ���ϴ� ���
    void Click_Item_Button()
    {
        // Item_Panel Ȱ��ȭ
        Panel_Item_Selected.SetActive(true);
        // ������ �г� ���Ƴ���
        Panel_Ability_Selected.SetActive(false);
        Panel_Skill_Selected.SetActive(false);    
       
        print("Item Button Active");
    }



}
