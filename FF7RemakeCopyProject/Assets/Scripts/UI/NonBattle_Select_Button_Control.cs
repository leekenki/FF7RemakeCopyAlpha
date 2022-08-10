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
        #region Panel 초기화
        // 1. 시작 Panel_Select 에서 아무것도 선택되지 않았을 경우, 어빌리티, 스킬, 아이템 패널을 비활성화 시킨다.
        Panel_Select.SetActive(true);
        Item_Info.SetActive(false);
        Panel_Ability_Selected.SetActive(true);
        Panel_Skill_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);

        #endregion

        #region Button 가져오기
        Button_Ability_Selected = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        Button_Skill_Selected = transform.GetChild(0).GetChild(1).GetComponent<Button>();
        Button_Item_Selected = transform.GetChild(0).GetChild(2).GetComponent<Button>();
        #endregion

        #region Button onClick Event 연결하기
        Button_Ability_Selected.onClick.AddListener(Click_Ability_Button);
        Button_Skill_Selected.onClick.AddListener(Click_Skill_Button);
        Button_Item_Selected.onClick.AddListener(Click_Item_Button);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {

    }


    // 만약 Panel_Select 에서 Ability Button 을 클릭하는 경우
    void Click_Ability_Button()
    {
        // Ability_Panel 활성화
        Panel_Ability_Selected.SetActive(true);
        // 나머지 패널 막아놓기
        Panel_Skill_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);
        
        // 함수 불러와서 넣기
        print("Ability Button Active");
    }

    // 만약 Panel_Select 에서 Skill Button 을 클릭하는 경우

    void Click_Skill_Button()
    {
        // Skill_Panel 활성화
        Panel_Skill_Selected.SetActive(true);
        // 나머지 패널 막아놓기
        Panel_Ability_Selected.SetActive(false);
        Panel_Item_Selected.SetActive(false);

        
        print("Skill Button Active");
    }

    // 만약 Panel+Select 에서 Item Button 을 클릭하는 경우
    void Click_Item_Button()
    {
        // Item_Panel 활성화
        Panel_Item_Selected.SetActive(true);
        // 나머지 패널 막아놓기
        Panel_Ability_Selected.SetActive(false);
        Panel_Skill_Selected.SetActive(false);    
       
        print("Item Button Active");
    }



}
