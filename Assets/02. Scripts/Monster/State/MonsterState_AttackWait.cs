using UnityEngine;

public class MonsterState_AttackWait : MonsterState
{
    private float timeSpent = 0f;  // 경과 시간

    public MonsterState_AttackWait(Monster _monster) : base(_monster, eMonsterState.waitAttack)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Idle");
        // 대기 시간을 리셋
        timeSpent = 0f;
    }

    public override void Update()
    {
        base.Update();

        // 경과 시간 증가
        timeSpent += Time.deltaTime;

        // 일정 시간이 경과하면 상태를 전환
        if (timeSpent >= monster.GetInfo().waitTime)
        {
            // 공격 범위 내에 플레이어가 있으면 공격 상태로 전환
            float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetTargetPos().position);

            if (distanceToPlayer <= monster.GetInfo().attackRange)
                monster.fsm.SetState(eMonsterState.attack);

            // 추적 범위 내에 플레이어가 있으면 추적 상태로 변경
            else if (distanceToPlayer <= monster.GetInfo().chaseRange)
                monster.fsm.SetState(eMonsterState.move);

            // 전부 바깥이라면 idle 상태로 전환
            else
                monster.fsm.SetState(eMonsterState.idle);
        }
    }
}
