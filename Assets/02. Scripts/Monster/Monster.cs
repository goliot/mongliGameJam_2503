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
            //��� ó��
            //�ִϸ��̼� ����, ��ü �����ָ鼭 objectPool�� return
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

        float moveDistance = 1.5f;  // ���� Ƣ����� �Ÿ�
        float moveDuration = 0.8f;  // �̵� �ð�
        float returnDuration = 0.5f; // �ٽ� �������� �ð�
        float randomX = Random.Range(-0.5f, 0.5f); // �¿� ���� �̵� ����

        // ���� ��ġ ����
        Vector3 startPos = damageNum.transform.position;

        // Ƣ����� �� ������ X��ǥ�� ���ƿ� ��ǥ ��ġ ����
        Vector3 endPos = startPos + Vector3.up * moveDistance + new Vector3(randomX, 0, 0);
        damageNum.transform.DOMove(endPos, moveDuration)
                        .SetEase(Ease.OutQuad)  // �ε巴�� ���� Ƣ�������
                        .OnKill(() =>
                        {
                            // �������� �ִϸ��̼� ����
                            damageNum.transform.DOMove(startPos, returnDuration)
                                                .SetEase(Ease.InQuad) // �ε巴�� ��������
                                                .OnKill(() =>
                                                {
                                                    // ������ �Ŀ��� ��ü�� Ǯ�� ��ȯ�ϰų� ������ �� ����
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
        //���� : ��������
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, MonsterDataSO.chaseRange);

        //��� : ���ݹ���
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, MonsterDataSO.attackRange);

    }
}
