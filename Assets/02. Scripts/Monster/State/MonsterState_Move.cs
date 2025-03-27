using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MonsterState_Move : MonsterState
{
    private Transform playerTransform;
    private float attackRange = 5f;  // ���� ���� (���� ��)

public MonsterState_Move(Monster _monster) : base(_monster, eMonsterState.move)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    //�̵� ����
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter Move");
        monster.SetAnimator("Walk");
        
   
    }

    //��� ����
    public override void Update()
    {
        base.Update();
        Debug.Log($"Monster Move Update : to {playerTransform.position}");

        //�ʵ忡 ��ǥ���ϴ� ���Ͱ� �������
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - monster.transform.position).normalized;

            // direction�������� �̹��� ����
            if (direction.x < 0)
            {
                monster.GetSprite().flipX = true;  // �������� �̵��ϸ� �̹����� ������Ŵ
            }
            else
            {
                monster.GetSprite().flipX = false;  // ���������� �̵��ϸ� �̹����� �������
            }

            // ���� �̵�
            monster.transform.position += direction * Time.deltaTime * monster.GetSpeed();

            // �÷��̾�� ���� ���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

            // ���� ���� ���� ������ �� attack ���·� ��ȯ
            if (distanceToPlayer <= attackRange)
            {
                Debug.Log("Player in attack range, switching to Attack");
                monster.fsm.SetState(eMonsterState.attack);
            }
        }
        else
        {
            // �÷��̾ ������ idle ���·� ��ȯ
            monster.fsm.SetState(eMonsterState.idle);
        }

        //TODO :: �������� �ٱ��̸� ���� �̵� ����
    }
}
