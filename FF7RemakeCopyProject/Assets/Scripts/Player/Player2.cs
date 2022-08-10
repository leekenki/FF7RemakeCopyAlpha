using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주인공
public class Player2 : PlayerBase
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerState = PlayerState.Inactive; //주인공만 active 상태로 둠
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
