using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MonsterState_Move : MonsterState
{
    private Transform playerTransform;
    private float attackRange = 5f;  // 공격 범위 (예시 값)

public MonsterState_Move(Monster _monster) : base(_monster, eMonsterState.move)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    //이동 진입
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter Move");
        monster.SetAnimator("Walk");
        
   
    }

    //상시 실행
    public override void Update()
    {
        base.Update();
        Debug.Log($"Monster Move Update : to {playerTransform.position}");

        //필드에 목표로하는 몬스터가 없을경우
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - monster.transform.position).normalized;

            // direction방향으로 이미지 반전
            if (direction.x < 0)
            {
                monster.GetSprite().flipX = true;  // 왼쪽으로 이동하면 이미지를 반전시킴
            }
            else
            {
                monster.GetSprite().flipX = false;  // 오른쪽으로 이동하면 이미지를 원래대로
            }

            // 몬스터 이동
            monster.transform.position += direction * Time.deltaTime * monster.GetSpeed();

            // 플레이어와 몬스터 간의 거리 계산
            float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

            // 공격 범위 내에 들어왔을 때 attack 상태로 전환
            if (distanceToPlayer <= attackRange)
            {
                Debug.Log("Player in attack range, switching to Attack");
                monster.fsm.SetState(eMonsterState.attack);
            }
        }
        else
        {
            // 플레이어가 없으면 idle 상태로 전환
            monster.fsm.SetState(eMonsterState.idle);
        }

        //TODO :: 일정범위 바깥이면 추적 이동 중지
    }
}
