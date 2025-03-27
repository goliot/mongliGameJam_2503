using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFsmFactory
{
    public MonsterFsm Create(Monster _monster)
    {
        MonsterFsm _fsm = new MonsterFsm(_monster);
        //�Ϲ�
        _fsm.AddFsm(new MonsterState_Idle(_monster));
        //�̵�
        _fsm.AddFsm(new MonsterState_Move(_monster));
        //����
        _fsm.AddFsm(new MonsterState_Attack(_monster));
        //���ݴ��
        //�ʿ䰡 ��������?
        //���
        _fsm.AddFsm(new MonsterState_Death(_monster));

        return _fsm;
    }
}
