using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MonsterState_Move : MonsterState
{
    private Transform playerTransform;

    public MonsterState_Move(Monster _monster) : base(_monster, eMonsterState.move)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    //�̵� ����
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter Move");

   
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
            //directon�������� �̹��� ����
            if (direction.x < 0)
            {
                monster.GetSprite().flipX = true;  // �������� �̵��ϸ� �̹����� ������Ŵ
            }
            else
            {
                monster.GetSprite().flipX = false;  // ���������� �̵��ϸ� �̹����� �������
            }
            monster.transform.position += direction * Time.deltaTime * monster.GetSpeed(); 
        }
        else
        {
            monster.fsm.SetState(eMonsterState.idle);
        }

        //TODO :: �������� �ٱ��̸� ���� �̵� ����
    }
}
