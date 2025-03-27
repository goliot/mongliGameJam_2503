using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Quaternion _targetRotation; // 로컬 플레이어의 회전 목표

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
        // 로컬 플레이어의 경우 마우스 위치 기반 회전 처리
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 dir = transform.localPosition - mousePosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        _targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = _targetRotation;

        // 마우스 위치에 따라 flipX 및 gunTip의 위치 반전
        if (mousePosition.x > transform.position.x)
        {
            _spriteRenderer.flipY = true;
            //pv.RPC("SortingOrderControl", RpcTarget.AllBuffered, 4);
        }
        else
        {
            _spriteRenderer.flipY = false;
            //pv.RPC("SortingOrderControl", RpcTarget.AllBuffered, 6);
        }

        // 발사 키 입력 시 반동 효과
        if (Input.GetMouseButtonDown(0))
        {
            TriggerRecoil();
        }
    }

    /*[PunRPC]
    void FlipYRPC(bool flipFlag) => spriteRenderer.flipY = flipFlag;

    [PunRPC]
    void SortingOrderControl(int x) => spriteRenderer.sortingOrder = x;*/

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