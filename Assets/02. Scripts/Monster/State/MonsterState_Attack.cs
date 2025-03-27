using UnityEngine;
using UnityEngine.Rendering;

public class MonsterState_Attack : MonsterState
{
    private bool isAttackAnimationFinished = false;

    public MonsterState_Attack(Monster _monster) : base(_monster, eMonsterState.attack)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Monster Enter Attack");
        //공격 애니가 실행되는 순간 데미지가 들어가는 걸로
        monster.SetAnimator("Attack");
        //공격 애니가 끝나면 wait로 보내자
        isAttackAnimationFinished = false;
    }

    public override void Update()
    {
        base.Update();

        //Vector3 direction = (playerTransform.position - monster.transform.position).normalized;
        //if (direction.x < 0)
        //{
        //    monster.GetSprite().flipX = true;  // 왼쪽으로 이동하면 이미지를 반전시킴
        //}
        //else
        //{
        //    monster.GetSprite().flipX = false;  // 오른쪽으로 이동하면 이미지를 원래대로
        //}


        if (!isAttackAnimationFinished)
        {
            // 애니메이션이 끝났는지 확인 (애니메이션이 끝나면 normalizedTime이 1 이상)
            AnimatorStateInfo stateInfo = monster.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            // 공격 애니메이션이 끝났는지 (normalizedTime이 1 이상) 확인
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                isAttackAnimationFinished = true;
                Debug.Log("Attack animation finished, switching to Attack_wait");

                // 공격이 끝나면 Attack_wait로 상태 전환
                monster.fsm.SetState(eMonsterState.waitAttack);
            }
        }

    }
}
