using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 15f;  // ��� �ӵ�
    public float dashDuration = 0.2f;  // ��� ���� �ð�
    public float dashCooldown = 1f;  // ��� ��Ÿ��
    private float lastDashTime;
    public GameObject AfterImage;

    private PlayerDataSO _playerData;
    private Animator _animator;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    private float _h;
    private float _v;
    private Vector3 _direction;
    private bool _lastFlipX; // ������ ���� ����

    [Header("# State")]
    private bool _isDashing = false;

    private void Awake()
    {
        _playerData = GetComponent<Player>().PlayerData;
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            return;
        }
        GetInput();
        Move();
        SetAnimation();
    }

    private void GetInput()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _direction = Vector3.Normalize(new Vector3(_h, _v, 0));

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
        }
    }

    private void Move()
    {
        if(_isDashing)
        {
            return;
        }

        transform.Translate(_direction * _playerData.NowSpeed * Time.deltaTime);
    }

    private void Dash()
    {
        _isDashing = true;
        lastDashTime = Time.time;

        if (_direction == Vector3.zero)
            _direction = Vector2.right * transform.localScale.x; // ���� ������ �� ���� ����

        _rb.linearVelocity = _direction * dashSpeed;
        AfterImage.SetActive(true);
        //AfterImage.GetComponent<GhostTrailEffect>().ShowGhost();
        Invoke("StopDash", dashDuration);
    }

    private void StopDash()
    {
        _isDashing = false;
        AfterImage.SetActive(false);
        //AfterImage.GetComponent<DashAfterImage>().ChaseTarget();
        _rb.linearVelocity = Vector2.zero;
    }

    private void SetAnimation()
    {
        _animator.SetBool("IsRun", _direction != Vector3.zero);

        if (_direction.x != 0) // X������ ������ ���� ���� ������Ʈ
        {
            _lastFlipX = _direction.x < 0;
        }

        _sr.flipX = _lastFlipX; // ������ ���� ����
    }
}