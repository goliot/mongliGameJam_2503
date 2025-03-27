using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Quaternion _targetRotation; // 로컬 플레이어의 회전 목표

    [SerializeField] private Transform Muzzle;

    [Header("Recoil Settings")]
    public float RecoilDistance = 0.2f; // 반동 거리
    public float RecoilDuration = 0.05f; // 반동 효과 지속 시간
    private Vector3 _originalPosition; // 총의 원래 위치
    private Coroutine _recoilCoroutine; // 현재 실행 중인 반동 코루틴

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalPosition = transform.localPosition; // 초기 위치 저장
    }

    private void Update()
    {
        Spin();

        // 발사 키 입력 시 반동 효과 실행
        if (Input.GetMouseButtonDown(0))
        {
            TriggerRecoil();
        }
    }

    private void Spin()
    {
        // 마우스 위치 계산 (월드 좌표 변환)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // 2D 게임이므로 Z 좌표 고정

        // 방향 벡터 계산
        Vector2 dir = transform.position - mousePosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 총 회전 적용
        _targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = _targetRotation;

        // 마우스 위치에 따라 flipY 및 총구 위치 반전
        bool isRightSide = mousePosition.x > transform.position.x;
        _spriteRenderer.flipY = isRightSide;  // 마우스 위치에 따라 총 회전

        // Muzzle 위치 반전 (로컬 좌표 사용)
        Vector3 localMuzzlePos = Muzzle.localPosition;
        localMuzzlePos.y = Mathf.Abs(localMuzzlePos.y) * (isRightSide ? -1 : 1);
        Muzzle.localPosition = localMuzzlePos;
    }

    /// <summary>
    /// 총기 반동 효과를 트리거
    /// </summary>
    private void TriggerRecoil()
    {
        // 이미 실행 중인 반동 코루틴이 있다면 중단
        if (_recoilCoroutine != null)
            StopCoroutine(_recoilCoroutine);

        // 새로운 반동 효과 시작
        _recoilCoroutine = StartCoroutine(RecoilRoutine());
    }

    /// <summary>
    /// 반동 효과 코루틴
    /// </summary>
    private IEnumerator RecoilRoutine()
    {
        Vector3 recoilPosition = _originalPosition + transform.right * RecoilDistance;

        // 반동 위치로 이동
        float elapsedTime = 0f;
        while (elapsedTime < RecoilDuration)
        {
            transform.localPosition = Vector3.Lerp(_originalPosition, recoilPosition, elapsedTime / RecoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 원래 위치로 복귀
        elapsedTime = 0f;
        while (elapsedTime < RecoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, _originalPosition, elapsedTime / RecoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 반동 효과 종료
        transform.localPosition = _originalPosition;
        _recoilCoroutine = null;
    }
}