using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("# Player Chase")]
    [SerializeField] private Transform TargetTransform; // 따라갈 대상
    public Vector3 Offset;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [Header("# Camera Shake")]
    public bool isShaking = false;
    public float ShakeAmount = 0.1f;
    public float ShakeDuration = 0.5f;
    private float _shakeTimer = 0f;
    private Vector3 _currentPosition;

    private void Awake()
    {
        TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        _currentPosition = transform.position;

        if (_shakeTimer > 0)
        {
            transform.position = _currentPosition + (Vector3)Random.insideUnitCircle * ShakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            isShaking = false;
            Vector3 targetPosition = TargetTransform.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    void Shake()
    {
        isShaking = true;
        _shakeTimer = ShakeDuration;
    }
}