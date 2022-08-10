using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider Example_Item)
    {
        NonBattle_Select_Button_Control.Instance.Item_Info.SetActive(true);
        Destroy(gameObject);
        
    }
}
