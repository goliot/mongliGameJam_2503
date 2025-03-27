using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterDataSO MonsterDataSO;

    public MonsterFsm fsm;
    private MonsterFsmFactory fsmFactory;
    private Transform playerTarget;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        MonsterFsmFactory fsmFactory = new MonsterFsmFactory();
        fsm = fsmFactory.Create(this);

        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        fsm.Update();
    }

    public MonsterDataSO GetInfo()
    {
        return MonsterDataSO;
    }

    public Transform GetTargetPos()
    {
        return playerTarget;
    }

    public float GetchaseRangeRange()
    {
        return MonsterDataSO.chaseRange;
    }

    public void SetAnimator(string clipName)
    {
        animator.Play(clipName);
    }

    public SpriteRenderer GetSprite()
    {
        return spriteRenderer;
    }

    public float GetSpeed()
    {
        return MonsterDataSO.moveSpeed;
    }

    public void TakeDamage()
    {
        SetAnimator("Hurt");
        return;
    }

    private void OnDrawGizmos()
    {
        //빨강 : 추적범위
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, MonsterDataSO.chaseRange);

        //노랑 : 공격범위
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, MonsterDataSO.attackRange);

    }
}
