using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//에너미 등장 지점을 정하고 그 곳에서 instantiate;
public class EnemyGod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject enemy0; //에디터에서 프리팹 넣기
    public GameObject enemy1;

    // Update is called once per frame
    public bool instantiateComplete = false;
    void Update()
    {
        // (!instantiateComplete)
        // {

        // }
    }
}
