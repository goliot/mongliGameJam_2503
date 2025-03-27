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


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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

        if(Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(10f);
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

    public void TakeDamage()
    {
        SetAnimator("Hurt");
        return;
    }

    public void TakeDamage(float damage)
    {
        healthBarObj.SetActive(true);
        SetAnimator("Hurt");
        MonsterDataSO.health -= damage;
        healthBar.fillAmount = MonsterDataSO.health / (float)MonsterDataSO.maxHealth; 

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
