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
        // HD�� �̱�������
        instance = this;
    }

    void Start()
    {
        UIControlCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        // ���� �� ������ ��쿡(BattleStage)
        if (GameManager.Instance.flowState == GameManager.GameFlow.BattleStage)
        {
            // ���� UI�� ����, ������ ���� �ݱ�
            UIControlCanvas.SetActive(true);
            NonBattlePanel.SetActive(false);
            BattlePanel.SetActive(true);
        }
        // ������ �����̸鼭 �κ��丮 â�� ����� �ϴ� ���(Pause)
        else if (GameManager.Instance.flowState == GameManager.GameFlow.Pause)
        {
            // ������ UI�� ����, ������ ���� �ݱ�
                UIControlCanvas.SetActive(true);
                NonBattlePanel.SetActive(true);
                BattlePanel.SetActive(false);
            /*if (Input.GetKeyDown(KeyCode.P))
            {
            }*/
        }

        // �� �̿��� ���
        else
        {
            // ��� UI �� �ݱ�
            //UIControlCanvas.SetActive(false);
            NonBattlePanel.SetActive(false);
            //BattlePanel.SetActive(false);
        }
    }
}
