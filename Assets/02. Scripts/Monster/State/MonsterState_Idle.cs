using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class MonsterState_Idle : MonsterState
{
    private float detectionRange = 10f;
    private Transform playerTransform;

    public MonsterState_Idle(Monster _monster) : base(_monster, eMonsterState.idle)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void Enter()
    {
        base.Enter();
        //Idle �ִϸ� ���⼭ ����������

        Debug.Log("Monster Enter Idle");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Monster Update Idle");

        float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

        // Player�� ���� ���� ������ MoveState�� ��ȯ
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Player detected, switching to MoveState");
            monster.fsm.SetState(eMonsterState.move);
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmo ���� ���� (������ ������)
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);  // ���� �� 0.2�� �������ϰ� ����
        // ������ ��ġ�� �߽����� detectionRange ���� �ȿ� ���� �׸��ϴ�.
        Gizmos.DrawWireSphere(monster.transform.position, detectionRange);
    }
}
