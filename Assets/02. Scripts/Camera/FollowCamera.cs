using System.Collections;
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

    [Header("# Mouse Influence")]
    public float mouseInfluence = 2.0f; // 마우스 방향으로 밀리는 최대 거리
    public float maxDistance = 5.0f; // 플레이어와 최대 거리 제한

    private void Awake()
    {
        TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        _currentPosition = transform.position;

        // 카메라 흔들기 로직
        if (_shakeTimer > 0)
        {
            transform.position = _currentPosition + (Vector3)Random.insideUnitCircle * ShakeAmount;
            _shakeTimer -= Time.deltaTime;
            return;
        }

        // 기본적인 플레이어 추적 위치
        Vector3 targetPosition = TargetTransform.position + Offset;

        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D 게임일 경우 Z값 고정

        // 마우스 방향 벡터 계산 (플레이어 -> 마우스)
        Vector3 mouseDir = (mousePosition - TargetTransform.position).normalized;

        // 마우스 영향 적용 (최대 거리 제한)
        Vector3 mouseOffset = mouseDir * mouseInfluence;
        mouseOffset = Vector3.ClampMagnitude(mouseOffset, mouseInfluence); // 최대 영향 거리 제한

        // 최종 목표 위치 (마우스 영향 포함)
        Vector3 finalPosition = targetPosition + mouseOffset;

        // 플레이어와의 최대 거리 유지
        Vector3 cameraOffset = finalPosition - TargetTransform.position;
        cameraOffset = Vector3.ClampMagnitude(cameraOffset, maxDistance);

        finalPosition = TargetTransform.position + cameraOffset;

        // 부드럽게 이동
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
    }

    public void Shake()
    {
        isShaking = true;
        _shakeTimer = ShakeDuration;
    }
}
