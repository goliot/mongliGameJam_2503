using DG.Tweening;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [SerializeField] private EObjectType type;

    public int maxBounce;	// 팅기는 횟수

    public float xForce;	// x축 힘 (더 멀리)
    public float yForce;	// Y축 힘 (더 높이)
    public float gravity;	// 중력 (떨어지는 속도 제어)

    private Vector2 direction;
    private int currentBounce = 0;
    private bool isGrounded = true;

    private float maxHeight;
    private float currentheight;

    public Transform cartridge;
    public Transform shadow;

    private Vector2 _originalShadowScale;
    private Color _originColor;

    private void Awake()
    {
        _originalShadowScale = shadow.transform.localScale;
        _originColor = cartridge.GetComponent<SpriteRenderer>().color;
    }

    void OnEnable()
    {
        InitVariable();
        currentheight = Random.Range(yForce - 1, yForce);
        maxHeight = currentheight;
        cartridge.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-50, 50), ForceMode2D.Impulse);
        Initialize(new Vector2(Random.Range(-xForce, xForce), Random.Range(-xForce, xForce)));
    }

    private void InitVariable()
    {
        currentBounce = 0;
        isGrounded = false;
        cartridge.GetComponent<SpriteRenderer>().color = _originColor;
    }

    void Update()
    {

        if (!isGrounded)
        {
            currentheight += -gravity * Time.deltaTime;
            cartridge.position += new Vector3(0, currentheight, 0) * Time.deltaTime;
            transform.position += (Vector3)direction * Time.deltaTime;

            float totalVelocity = Mathf.Abs(currentheight) + Mathf.Abs(maxHeight);
            float scaleXY = Mathf.Abs(currentheight) / totalVelocity;
            shadow.localScale = Vector2.one * Mathf.Clamp(scaleXY, 0.5f, 1.0f);

            CheckGroundHit();
        }
    }

    void Initialize(Vector2 _direction)
    {
        isGrounded = false;
        maxHeight /= 1.5f;
        direction = _direction;
        currentheight = maxHeight;
        currentBounce++;
    }

    void CheckGroundHit()
    {
        if (cartridge.position.y < shadow.position.y)
        {
            cartridge.position = shadow.position;
            shadow.localScale = _originalShadowScale;

            if (currentBounce < maxBounce)
            {
                Initialize(direction / 1.5f);
            }
            else
            {
                isGrounded = true;
                cartridge.GetComponent<Rigidbody2D>().angularVelocity = 0;
                cartridge.GetComponent<SpriteRenderer>().DOColor(new Color(0, 0, 0, 0), 30f).SetEase(Ease.InExpo).OnComplete(() => ReleaseToPool());
            }
        }
    }

    void ReleaseToPool()
    {
        PoolManager.Instance.ReturnObject(gameObject, type);
    }
}