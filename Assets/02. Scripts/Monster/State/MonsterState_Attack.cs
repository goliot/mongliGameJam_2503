using UnityEngine;
using UnityEngine.Rendering;

public class MonsterState_Attack : MonsterState
{
    public MonsterState_Attack(Monster _monster) : base(_monster, eMonsterState.attack)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }
}
