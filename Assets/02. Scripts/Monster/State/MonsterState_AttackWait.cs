using UnityEngine;

public class MonsterState_AttackWait : MonsterState
{
    private float timeSpent = 0f;  // ��� �ð�

    public MonsterState_AttackWait(Monster _monster) : base(_monster, eMonsterState.waitAttack)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Idle");
        // ��� �ð��� ����
        timeSpent = 0f;
    }

    public override void Update()
    {
        base.Update();

        // ��� �ð� ����
        timeSpent += Time.deltaTime;

        // ���� �ð��� ����ϸ� ���¸� ��ȯ
        if (timeSpent >= monster.GetInfo().waitTime)
        {
            // ���� ���� ���� �÷��̾ ������ ���� ���·� ��ȯ
            float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetTargetPos().position);

            if (distanceToPlayer <= monster.GetInfo().attackRange)
                monster.fsm.SetState(eMonsterState.attack);

            // ���� ���� ���� �÷��̾ ������ ���� ���·� ����
            else if (distanceToPlayer <= monster.GetInfo().chaseRange)
                monster.fsm.SetState(eMonsterState.move);

            // ���� �ٱ��̶�� idle ���·� ��ȯ
            else
                monster.fsm.SetState(eMonsterState.idle);
        }
    }
}
