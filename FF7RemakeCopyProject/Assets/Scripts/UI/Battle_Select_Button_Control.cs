using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Select_Button_Control : MonoBehaviour
{
    public Canvas BattleCanvas;

    public GameObject Panel_Select;

    public GameObject BattlePanel_Attack;
    public GameObject BattlePanel_Ability;
    public GameObject BattlePanel_Skill;

    private Button Hard_Attack_Button;
    private Button Normal_Attack_Button;

    private Button Ability_Button1;
    private Button Ability_Button2;

    private Button Skill_Button1;
    private Button Skill_Button2;

    private


    // Start is called before the first frame update
    void Start()
    {
        #region Panel �ʱ�ȭ (BattlePanel_Attack Ȱ��ȭ ��Ű��)
        // 1. ���� �� BattlePanel_Attack �� Ȱ��ȭ ��Ų��.
        BattlePanel_Attack.SetActive(true);
        BattlePanel_Ability.SetActive(false);
        BattlePanel_Skill.SetActive(false);
        #endregion

        #region Panel ��ȯ
        /*if (BattlePanel_Attack.activeSelf == true)
        {
            //if (Input.GetKeyDown(KeyCode.P))
            if (GameManager.Instance.flowState == GameManager.GameFlow.Slow)
            {
                BattlePanel_Attack.SetActive(false);
                BattlePanel_Ability.SetActive(true);
                BattlePanel_Skill.SetActive(false);
            }
        }
        else if (BattlePanel_Ability.activeSelf == true)
        {
            //if (Input.GetKeyDown(KeyCode.P))
            if (GameManager.Instance.flowState == GameManager.GameFlow.Slow)
            {
                BattlePanel_Attack.SetActive(false);
                BattlePanel_Ability.SetActive(false);
                BattlePanel_Skill.SetActive(true);
                print("TimeDelayed");
            }
        }
        else if(BattlePanel_Skill.activeSelf == true)
        {
            if (GameManager.Instance.flowState == GameManager.GameFlow.Slow)
            //if (Input.GetKeyDown(KeyCode.P))
            {
                BattlePanel_Skill.SetActive(false);
                BattlePanel_Attack.SetActive(true);
                BattlePanel_Skill.SetActive(false);
                print("TimeDelayed");
            }
        }*/
        #endregion

        #region Button ��������
        

        Hard_Attack_Button = transform.GetChild(0).GetChild(0).GetComponent<Button>();        
        Normal_Attack_Button = transform.GetChild(0).GetChild(1).GetComponent<Button>();

        Ability_Button1 = transform.GetChild(1).GetChild(0).GetComponent<Button>();
        Ability_Button2 = transform.GetChild(1).GetChild(1).GetComponent<Button>();

        Skill_Button1 = transform.GetChild(2).GetChild(0).GetComponent<Button>();
        Skill_Button2 = transform.GetChild(2).GetChild(1).GetComponent<Button>();
        #endregion

        #region Button onClick Event �����ϱ�            
         Normal_Attack_Button.onClick.AddListener(use_Attack_Button2);        
        Hard_Attack_Button.onClick.AddListener(use_Attack_Button1);

        Ability_Button1.onClick.AddListener(Use_Ability_Button1);
        Ability_Button2.onClick.AddListener(Use_Ability_Button2);

        Skill_Button1.onClick.AddListener(Use_Skill_Button1);
        Skill_Button2.onClick.AddListener(Use_Skill_Button2);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            use_Attack_Button1();
        }
        Debug.Log("attack: " + BattlePanel_Attack.activeSelf);
        Debug.Log("ability: " + BattlePanel_Ability.activeSelf);
        Debug.Log("skill : " + BattlePanel_Skill.activeSelf);
        //////////////////////////////////////////////////////
        if (BattlePanel_Attack.activeSelf == true)
        {
            //���ο찡 �ƴ� ���¿��� p������ �����Ƽ Ȱ��ȭ
            //if(GameManager.Instance.flowState == GameManager.GameFlow.Slow)
            if(Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("SHIT");
                BattlePanel_Attack.SetActive(false);
                BattlePanel_Ability.SetActive(true);
                BattlePanel_Skill.SetActive(false);
            }
        }
        else if (BattlePanel_Ability.activeSelf == true)
        {
            //�����Ƽ�� Ȱ��ȭ�� ���¿��� p������ ���ݸ� Ȱ��ȭ, o������ ��ų�� Ȱ��ȭ
            if (Input.GetKeyDown(KeyCode.P))
            {
                BattlePanel_Attack.SetActive(true);
                BattlePanel_Ability.SetActive(false);
                BattlePanel_Skill.SetActive(false);
                print("TimeDelayed");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                BattlePanel_Attack.SetActive(false);
                BattlePanel_Ability.SetActive(false);
                BattlePanel_Skill.SetActive(true);
                print("TimeDelayed");
            }
        }
        else if (BattlePanel_Skill.activeSelf == true)
        {
            //��ų�� Ȱ��ȭ �� ���¿��� p ������ ���ݸ� Ȱ��ȭ
            //if (GameManager.Instance.flowState == GameManager.GameFlow.Slow)
            if (Input.GetKeyDown(KeyCode.P))
            {
                BattlePanel_Attack.SetActive(true);
                BattlePanel_Ability.SetActive(false);
                BattlePanel_Skill.SetActive(false);
                print("TimeDelayed");
            }
        }
    }

    void use_Attack_Button1()
    {
        print("use_Item_Button1 Active");
    }

    void use_Attack_Button2()
    {
        print("use_Item_Button2 Active");
    }
    void Use_Ability_Button1()
    {
        // // Ability_Panel Ȱ��ȭ
        print("Use_Ability_Button1 Active");
    }

    void Use_Ability_Button2()
    {
        print("Use_Ability_Button1 Active");
    }

    void Use_Skill_Button1()
    {
        print("Use_Skill_Button1 Active");
    }

    void Use_Skill_Button2()
    {
        print("Use_Skill_Button2 Active");
    }





}

