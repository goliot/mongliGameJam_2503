using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eMonsterState
{
    idle, //기본상태(분기점)
    move, //이동
    patrol, //캐릭터가 마을에서 돌아다니는용
    waitAttack, //공격이후 대기
    attack, //공격
    death, //필드에서 사망
}
public class MonsterState : FsmState<eMonsterState>
{
    protected Monster monster;

    public MonsterState(Monster _monster, eMonsterState _state) : base(_state)
    {
        monster = _monster;
    }

}