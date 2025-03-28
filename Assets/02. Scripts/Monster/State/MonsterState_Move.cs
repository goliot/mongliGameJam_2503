using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MonsterState_Move : MonsterState
{

public MonsterState_Move(Monster _monster) : base(_monster, eMonsterState.move)
    {
    }

    //�̵� ����
    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Walk");

        monster.StartCoroutine(monster.SetExclamationObj());
    }

    //��� ����
    public override void Update()
    {
        base.Update();

        //�ʵ忡 ��ǥ���ϴ� ���Ͱ� �������
        if (monster.GetTargetPos() != null)
        {
            Vector3 direction = (monster.GetTargetPos().position - monster.transform.position).normalized;

            if (direction.x < 0)
                monster.GetSprite().flipX = true;
            else
                monster.GetSprite().flipX = false;

            monster.transform.position += direction * Time.deltaTime * monster.GetSpeed();

            float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetTargetPos().position);

            // ���� ���� ���� ������ �� attack ���·� ��ȯ
            if (distanceToPlayer <= monster.GetInfo().attackRange)
                monster.fsm.SetState(eMonsterState.attack);
        }
        else
        {
            // �÷��̾ ������ idle ���·� ��ȯ
            monster.fsm.SetState(eMonsterState.idle);
        }

        //TODO :: �������� �ٱ��̸� ���� �̵� ����
    }
}
