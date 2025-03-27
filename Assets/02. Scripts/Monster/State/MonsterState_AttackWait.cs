using UnityEngine;

public class MonsterState_AttackWait : MonsterState
{
    private float waitTime = 2f;  // 공격 대기 시간 (초)
    private float timeSpent = 0f;  // 경과 시간

    private Transform playerTransform;  // 플레이어의 위치
    private float attackRange = 5f;  // 공격 범위 (예시 값)
    private float chaseRange = 10f;  // 추적 범위 (예시 값)

    public MonsterState_AttackWait(Monster _monster) : base(_monster, eMonsterState.waitAttack)
    {
        // Player 오브젝트를 찾아서 playerTransform에 할당
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter AttackWait");
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
        if (timeSpent >= waitTime)
        {
            // 공격 범위 내에 플레이어가 있으면 공격 상태로 전환
            float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                // 공격 범위 내에 플레이어가 있으면 공격 상태로 변경
                Debug.Log("Player in attack range, switching to Attack");
                monster.fsm.SetState(eMonsterState.attack);
            }
            // 추적 범위 내에 플레이어가 있으면 추적 상태로 전환
            else if (distanceToPlayer <= chaseRange)
            {
                // 추적 범위 내에 플레이어가 있으면 추적 상태로 변경
                Debug.Log("Player in chase range, switching to Walk");
                monster.fsm.SetState(eMonsterState.move);
            }
            // 전부 바깥이라면 idle 상태로 전환
            else
            {
                // 범위 밖에 플레이어가 있으면 idle 상태로 변경
                Debug.Log("Player out of range, switching to Idle");
                monster.fsm.SetState(eMonsterState.idle);
            }
        }
    }
}
