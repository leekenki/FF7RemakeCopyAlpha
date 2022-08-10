using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HD : MonoBehaviour
{
    public static HD instance;

    public GameObject UIControlCanvas;
    public GameObject NonBattlePanel;    
    public GameObject BattlePanel;

    private void Awake()
    {
        // HD는 싱글톤으로
        instance = this;
    }

    void Start()
    {
        UIControlCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        // 전투 중 상태인 경우에(BattleStage)
        if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
        {
            // 전투 UI만 열고, 나머지 전부 닫기
            UIControlCanvas.SetActive(true);
            NonBattlePanel.SetActive(false);
            BattlePanel.SetActive(true);
        }
        // 비전투 상태이면서 인벤토리 창을 열어야 하는 경우(Pause)
        else if (GameManager.Instance.flowState == GameManager.GameFlow.Pause)
        {
            // 비전투 UI만 열고, 나머지 전부 닫기
                UIControlCanvas.SetActive(true);
                NonBattlePanel.SetActive(true);
                BattlePanel.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.P))
            {
            }*/
        }

        // 그 이외의 경우
        else
        {
            // 모든 UI 다 닫기
            //UIControlCanvas.SetActive(false);
            NonBattlePanel.SetActive(false);
            //BattlePanel.SetActive(false);
        }
    }
}
