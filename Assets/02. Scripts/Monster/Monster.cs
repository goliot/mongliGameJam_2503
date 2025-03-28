using DG.Tweening;
using System.Collections;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField]
    private GameObject ExclamationObj;

    private float _health = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _health = MonsterDataSO.maxHealth;
        healthBarObj.SetActive(false);
        ExclamationObj.SetActive(false);
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
        if(GameManager.Instance.IsGameOver)
        {
            return;
        }
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
            UIManager.Instance.ReduceMonsterCount();
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
        GameObject damageNum = PoolManager.Instance.GetObject(EObjectType.DamageNum);
        Show(damageNum, damage);

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

    private void Show(GameObject damageNum, float damage)
    {
        damageNum.transform.position = healthBarObj.transform.position;
        damageNum.GetComponent<TextMeshPro>().text = damage.ToString();

        float moveDistance = 1.5f;  // 위로 튀어오를 거리
        float moveDuration = 0.8f;  // 이동 시간
        float returnDuration = 0.5f; // 다시 내려오는 시간
        float randomX = Random.Range(-0.5f, 0.5f); // 좌우 랜덤 이동 범위

        // 시작 위치 저장
        Vector3 startPos = damageNum.transform.position;

        // 튀어오른 후 랜덤한 X좌표로 돌아올 목표 위치 설정
        Vector3 endPos = startPos + Vector3.up * moveDistance + new Vector3(randomX, 0, 0);
        damageNum.transform.DOMove(endPos, moveDuration)
                        .SetEase(Ease.OutQuad)  // 부드럽게 위로 튀어오르게
                        .OnKill(() =>
                        {
                            // 내려오는 애니메이션 시작
                            damageNum.transform.DOMove(startPos, returnDuration)
                                                .SetEase(Ease.InQuad) // 부드럽게 내려오기
                                                .OnKill(() =>
                                                {
                                                    // 내려온 후에는 객체를 풀로 반환하거나 삭제할 수 있음
                                                    PoolManager.Instance.ReturnObject(damageNum, EObjectType.DamageNum);
                                                });
                        });
    }

    public IEnumerator SetExclamationObj()
    {
        ExclamationObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        ExclamationObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(GetInfo().attackPower);
        }
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
