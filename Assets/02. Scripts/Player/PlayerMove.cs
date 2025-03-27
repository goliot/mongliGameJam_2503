using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed;

    private Animator _animator;
    private SpriteRenderer _sr;

    private float _h;
    private float _v;
    private Vector3 _direction;
    private bool _lastFlipX; // 마지막 방향 저장

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
        Move();
        SetAnimation();
    }

    private void GetInput()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _direction = Vector3.Normalize(new Vector3(_h, _v, 0));
    }

    private void Move()
    {
        transform.Translate(_direction * Speed * Time.deltaTime);
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
