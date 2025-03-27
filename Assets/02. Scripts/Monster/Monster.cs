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

        // 초기 상태 설정
        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        // FSM 업데이트
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
        //SO로 데이터 만들어서 여기서 다 긁어올 수 있도록 ?
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
        // Gizmo 색상 설정 (반투명 빨간색)
        Gizmos.color = new Color(1f, 0f, 0f, 1f);  // 알파 값 0.2로 반투명하게 설정
        // 몬스터의 위치를 중심으로 detectionRange 범위 안에 원을 그립니다.
        Gizmos.DrawWireSphere(this.transform.position, detectionRange);
    }
}
