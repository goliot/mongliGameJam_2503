using DG.Tweening;
using UnityEngine;

public abstract class ItemRoot : MonoBehaviour
{
    public float Speed = 5f;

    [SerializeField] private EObjectType _objectType;

    public EObjectType ObjectType
    {
        get => _objectType;
    }

    public GameObject PlayerObject;
    private Tweener _moveTweener = null;
    private float _deactiveTimer = 0f;
    private float _deactiveTime = 3f;

    [Header("# VFX")]
    public GameObject ItemGetVFX;

    //public bool MagneticFlag = false;
    private bool _towardFlag = false;

    public abstract void Effect();

    private void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _deactiveTimer += Time.deltaTime;
        if (_deactiveTimer > _deactiveTime)
        {
            //Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, PlayerObject.transform.position) < 2f)
        {
            if (_moveTweener == null)
            {
                TowardPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Effect();
            GameObject itemGetVFX = Instantiate(ItemGetVFX, transform.position, Quaternion.identity);
            //itemGetVFX.GetComponent<AudioSource>().Play();
            PoolManager.Instance.ReturnObject(gameObject, ObjectType);
            GetComponent<AudioSource>().Play();
        }

        /*if (collision.CompareTag("DestroyZone"))
        {
            PoolManager.Instance.ReturnObject(gameObject, _objectType);
        }*/
    }

    public void TowardPlayer()
    {
        _towardFlag = true;
        Vector3 targetPos = PlayerObject.transform.position;

        Vector3[] path = new Vector3[]
        {
            transform.position,
            transform.position + (Vector3)Random.insideUnitCircle * 0.5f,
            PlayerObject.transform.position
        };
        _moveTweener = transform.DOPath(path, 0.2f, PathType.CatmullRom)
            .SetEase(Ease.InOutSine).OnComplete(() => transform.DOMove(PlayerObject.transform.position, 0.1f));
    }
}
