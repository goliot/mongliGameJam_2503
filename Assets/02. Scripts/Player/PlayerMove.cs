using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 15f;  // 대시 속도
    public float dashDuration = 0.2f;  // 대시 지속 시간
    public float dashCooldown = 1f;  // 대시 쿨타임
    private float lastDashTime;
    public GameObject AfterImage;

    private PlayerDataSO _playerData;
    private Animator _animator;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    private float _h;
    private float _v;
    private Vector3 _direction;
    private bool _lastFlipX; // 마지막 방향 저장

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
            _direction = Vector2.right * transform.localScale.x; // 정지 상태일 때 방향 유지

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

        if (_direction.x != 0) // X축으로 움직일 때만 방향 업데이트
        {
            _lastFlipX = _direction.x < 0;
        }

        _sr.flipX = _lastFlipX; // 마지막 방향 유지
    }
}