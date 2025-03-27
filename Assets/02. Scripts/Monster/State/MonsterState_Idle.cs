using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class MonsterState_Idle : MonsterState
{

    private Transform playerTransform;

    public MonsterState_Idle(Monster _monster) : base(_monster, eMonsterState.idle)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void Enter()
    {
        base.Enter();
        monster.SetAnimator("Idle");
        Debug.Log("Monster Enter Idle");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("Monster Update Idle");

        float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

        // Player가 범위 내에 있으면 MoveState로 전환
        if (distanceToPlayer <= monster.GetdetectionRange())
        {
            Debug.Log("Player detected, switching to MoveState");
            monster.fsm.SetState(eMonsterState.move);
        }
    }
}
