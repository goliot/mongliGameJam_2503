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
        //���� �ִϰ� ����Ǵ� ���� �������� ���� �ɷ�
        monster.SetAnimator("Attack");
        isAttackAnimationFinished = false;
        monster.GetTargetPos().GetComponent<Player>().TakeDamage(monster.GetInfo().attackPower);
        Debug.Log($"Monster Give Damge : {monster.GetInfo().attackPower}");
    }

    public override void Update()
    {
        base.Update();

        Vector3 direction = (monster.GetTargetPos().position - monster.transform.position).normalized;
        if (direction.x < 0)
            monster.GetSprite().flipX = true;
        else
            monster.GetSprite().flipX = false;


        if (!isAttackAnimationFinished)
        {
            // �ִϸ��̼��� �������� Ȯ�� (�ִϸ��̼��� ������ normalizedTime�� 1 �̻�)
            AnimatorStateInfo stateInfo = monster.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                isAttackAnimationFinished = true;
                monster.fsm.SetState(eMonsterState.waitAttack);
            }
        }

    }
}
