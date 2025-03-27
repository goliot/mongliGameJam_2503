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
        //���� �ִϰ� ����Ǵ� ���� �������� ���� �ɷ�
        monster.SetAnimator("Attack");
        //���� �ִϰ� ������ wait�� ������
        isAttackAnimationFinished = false;
    }

    public override void Update()
    {
        base.Update();

        //Vector3 direction = (playerTransform.position - monster.transform.position).normalized;
        //if (direction.x < 0)
        //{
        //    monster.GetSprite().flipX = true;  // �������� �̵��ϸ� �̹����� ������Ŵ
        //}
        //else
        //{
        //    monster.GetSprite().flipX = false;  // ���������� �̵��ϸ� �̹����� �������
        //}


        if (!isAttackAnimationFinished)
        {
            // �ִϸ��̼��� �������� Ȯ�� (�ִϸ��̼��� ������ normalizedTime�� 1 �̻�)
            AnimatorStateInfo stateInfo = monster.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            // ���� �ִϸ��̼��� �������� (normalizedTime�� 1 �̻�) Ȯ��
            if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
            {
                isAttackAnimationFinished = true;
                Debug.Log("Attack animation finished, switching to Attack_wait");

                // ������ ������ Attack_wait�� ���� ��ȯ
                monster.fsm.SetState(eMonsterState.waitAttack);
            }
        }

    }
}
