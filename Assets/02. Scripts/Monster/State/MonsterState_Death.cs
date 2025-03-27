using UnityEngine;

public class MonsterState_Death : MonsterState
{
    public MonsterState_Death(Monster _monster) : base(_monster, eMonsterState.death)
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
