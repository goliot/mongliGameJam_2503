using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MonsterState_Move : MonsterState
{

public MonsterState_Move(Monster _monster) : base(_monster, eMonsterState.move)
    {
    }

    //이동 진입
    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Walk");

        monster.StartCoroutine(monster.SetExclamationObj());
    }

    //상시 실행
    public override void Update()
    {
        base.Update();

        //필드에 목표로하는 몬스터가 없을경우
        if (monster.GetTargetPos() != null)
        {
            Vector3 direction = (monster.GetTargetPos().position - monster.transform.position).normalized;

            if (direction.x < 0)
                monster.GetSprite().flipX = true;
            else
                monster.GetSprite().flipX = false;

            monster.transform.position += direction * Time.deltaTime * monster.GetSpeed();

            float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetTargetPos().position);

            // 공격 범위 내에 들어왔을 때 attack 상태로 전환
            if (distanceToPlayer <= monster.GetInfo().attackRange)
                monster.fsm.SetState(eMonsterState.attack);
        }
        else
        {
            // 플레이어가 없으면 idle 상태로 전환
            monster.fsm.SetState(eMonsterState.idle);
        }

        //TODO :: 일정범위 바깥이면 추적 이동 중지
    }
}
