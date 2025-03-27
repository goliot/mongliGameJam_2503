using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterFsm fsm;
    public MonsterFsmFactory fsmFactory;

    public GameObject Target;
    public float attackRange;
    
    [SerializeField]
    private float moveSpeed;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        MonsterFsmFactory fsmFactory = new MonsterFsmFactory();
        fsm = fsmFactory.Create(this);

        // 초기 상태 설정
        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        // FSM 업데이트
        fsm.Update();
    }

    public void GetInfo()
    {
        //SO로 데이터 만들어서 여기서 다 긁어올 수 있도록 ?
    }

    public SpriteRenderer GetSprite()
    {
        return spriteRenderer;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}
