using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterFsm fsm;
    public MonsterFsmFactory fsmFactory;
    public GameObject Target;

    
    [SerializeField]
    private float moveSpeed;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float detectionRange = 10f;
    private float attackRange;
    private Transform playerTransform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        MonsterFsmFactory fsmFactory = new MonsterFsmFactory();
        fsm = fsmFactory.Create(this);

        // �ʱ� ���� ����
        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        // FSM ������Ʈ
        fsm.Update();

        if(Input.GetKeyDown(KeyCode.V))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetAnimator("Death");
        }

    }

    public void GetInfo()
    {
        //SO�� ������ ���� ���⼭ �� �ܾ�� �� �ֵ��� ?
    }

    public float GetdetectionRange()
    {
        return detectionRange;
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
        return moveSpeed;
    }

    public void TakeDamage()
    {
        SetAnimator("Hurt");
        return;
    }

    private void OnDrawGizmos()
    {
        // Gizmo ���� ���� (������ ������)
        Gizmos.color = new Color(1f, 0f, 0f, 1f);  // ���� �� 0.2�� �������ϰ� ����
        // ������ ��ġ�� �߽����� detectionRange ���� �ȿ� ���� �׸��ϴ�.
        Gizmos.DrawWireSphere(this.transform.position, detectionRange);
    }
}
