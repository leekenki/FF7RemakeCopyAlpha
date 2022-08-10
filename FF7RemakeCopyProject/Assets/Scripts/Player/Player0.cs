using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//주인공
public class Player0 : PlayerBase
{
    // Start is called before the first frame update
    Player0Attack player0Attack;
    protected override void Start()
    {
        base.Start();
        player0Attack = GetComponent<Player0Attack>();
        playerState = PlayerState.Active; //주인공만 active 상태로 둠
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void CallAttackFunction(int num)
    {
        if (target)
        {
            switch (num)
            {
                case 0: //약공격
                    player0Attack.WeekAttack(target);
                    break;
                case 1: //강공격
                    player0Attack.StrongAttack(target);
                    break;
                case 2: //어빌리티
                    player0Attack.AbilityAttack(target);
                    break;
                case 3:
                    player0Attack.MateriaAttack(target);
                    break;
            }
        }
    }
}
