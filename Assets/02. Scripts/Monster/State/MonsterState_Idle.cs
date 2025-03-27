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
        //Idle 애니를 여기서 실행해주자

        Debug.Log("Monster Enter Idle");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Monster Update Idle");

        float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

        // Player가 범위 내에 있으면 MoveState로 전환
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Player detected, switching to MoveState");
            monster.fsm.SetState(eMonsterState.move);
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmo 색상 설정 (반투명 빨간색)
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);  // 알파 값 0.2로 반투명하게 설정
        // 몬스터의 위치를 중심으로 detectionRange 범위 안에 원을 그립니다.
        Gizmos.DrawWireSphere(monster.transform.position, detectionRange);
    }
}
