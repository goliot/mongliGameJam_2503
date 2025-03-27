using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterDataSO MonsterDataSO;

    public MonsterFsm fsm;
    private MonsterFsmFactory fsmFactory;
    private Transform playerTarget;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField]
    private GameObject healthBarObj;
    [SerializeField]
    private Image healthBar;

    private float _health = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _health = MonsterDataSO.maxHealth;
    }

    void Start()
    {
        healthBarObj.SetActive(false);
        playerTarget = GameObject.FindGameObjectWithTag("Player")?.transform;
        MonsterFsmFactory fsmFactory = new MonsterFsmFactory();
        fsm = fsmFactory.Create(this);

        fsm.SetState(eMonsterState.idle);
    }

    void Update()
    {
        fsm.Update();

        if(_health <= 0)
        {
            //사망 처리
            //애니매이션 없이, 시체 보여주면서 objectPool로 return
            int partCount = Random.Range(2, 3);
            for (int i = 0; i < partCount; i++)
            {
                GameObject part = PoolManager.Instance.GetObject(EObjectType.ZombiePart);
                part.transform.position = transform.position;
            }
            PoolManager.Instance.ReturnObject(this.gameObject, EObjectType.Zombie);
        }
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

    public void TakeDamage(float damage = 10f)
    {
        int bloodCount = Random.Range(3, 10);
        healthBarObj.SetActive(true);
        SetAnimator("Hurt");
        for (int i = 0; i < bloodCount; i++)
        {
            GameObject blood = PoolManager.Instance.GetObject(EObjectType.Blood);
            blood.transform.position = transform.position;
        }
        _health -= damage;
        healthBar.fillAmount = _health / (float)MonsterDataSO.maxHealth;
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
