using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFsmFactory
{
    public MonsterFsm Create(Monster _monster)
    {
        MonsterFsm _fsm = new MonsterFsm(_monster);
        //일반
        _fsm.AddFsm(new MonsterState_Idle(_monster));
        //이동
        _fsm.AddFsm(new MonsterState_Move(_monster));
        //공격
        _fsm.AddFsm(new MonsterState_Attack(_monster));
        //공격대기
        //필요가 없을지도?
        //사망
        _fsm.AddFsm(new MonsterState_Death(_monster));

        return _fsm;
    }
}
