using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFsm : Fsm<eMonsterState>
{
    public Monster monster;

    public MonsterFsm(Monster _monster)
    {
        monster = _monster;
    }

    public override void Update()
    {
        base.Update();
    }

    public bool IsState(eMonsterState _state)
    {
        if (curState.getState.Equals(_state))
            return true;

        if (nextState.getState.Equals(_state))
            return true;

        return false;
    }
}
