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

        // �ʱ� ���� ����
        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        // FSM ������Ʈ
        fsm.Update();
    }

    public void GetInfo()
    {
        //SO�� ������ ���� ���⼭ �� �ܾ�� �� �ֵ��� ?
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
