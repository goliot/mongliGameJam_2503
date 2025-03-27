using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMonsterState
{
    idle, //�⺻����(�б���)
    move, //�̵�
    patrol, //ĳ���Ͱ� �������� ���ƴٴϴ¿�
    waitAttack, //�������� ���
    attack, //����
    death, //�ʵ忡�� ���
}
public class MonsterState : FsmState<eMonsterState>
{
    protected Monster monster;

    public MonsterState(Monster _monster, eMonsterState _state) : base(_state)
    {
        monster = _monster;
    }

}