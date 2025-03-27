using UnityEngine;

public class MonsterState_AttackWait : MonsterState
{
    private float waitTime = 2f;  // ���� ��� �ð� (��)
    private float timeSpent = 0f;  // ��� �ð�

    private Transform playerTransform;  // �÷��̾��� ��ġ
    private float attackRange = 5f;  // ���� ���� (���� ��)
    private float chaseRange = 10f;  // ���� ���� (���� ��)

    public MonsterState_AttackWait(Monster _monster) : base(_monster, eMonsterState.waitAttack)
    {
        // Player ������Ʈ�� ã�Ƽ� playerTransform�� �Ҵ�
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter AttackWait");
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
        if (timeSpent >= waitTime)
        {
            // ���� ���� ���� �÷��̾ ������ ���� ���·� ��ȯ
            float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                // ���� ���� ���� �÷��̾ ������ ���� ���·� ����
                Debug.Log("Player in attack range, switching to Attack");
                monster.fsm.SetState(eMonsterState.attack);
            }
            // ���� ���� ���� �÷��̾ ������ ���� ���·� ��ȯ
            else if (distanceToPlayer <= chaseRange)
            {
                // ���� ���� ���� �÷��̾ ������ ���� ���·� ����
                Debug.Log("Player in chase range, switching to Walk");
                monster.fsm.SetState(eMonsterState.move);
            }
            // ���� �ٱ��̶�� idle ���·� ��ȯ
            else
            {
                // ���� �ۿ� �÷��̾ ������ idle ���·� ����
                Debug.Log("Player out of range, switching to Idle");
                monster.fsm.SetState(eMonsterState.idle);
            }
        }
    }
}
