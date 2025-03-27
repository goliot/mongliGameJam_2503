using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class MonsterState_Idle : MonsterState
{
    public MonsterState_Idle(Monster _monster) : base(_monster, eMonsterState.idle)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Idle");
    }

    public override void Update()
    {
        base.Update();

        float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetTargetPos().position);

        // Player가 범위 내에 있으면 MoveState로 전환
        if (distanceToPlayer <= monster.GetchaseRangeRange())
            monster.fsm.SetState(eMonsterState.move);
    }
}
