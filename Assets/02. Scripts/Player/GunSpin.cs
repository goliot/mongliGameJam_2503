using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpin : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Quaternion _targetRotation; // ���� �÷��̾��� ȸ�� ��ǥ

    [Header("Recoil Settings")]
    public float RecoilDistance = 0.2f; // �ݵ� �Ÿ�
    public float RecoilDuration = 0.05f; // �ݵ� ȿ�� ���� �ð�
    private Vector3 _originalPosition; // ���� ���� ��ġ
    private Coroutine _recoilCoroutine; // ���� ���� ���� �ݵ� �ڷ�ƾ

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalPosition = transform.localPosition; // �ʱ� ��ġ ����
    }

    private void Update()
    {
        // ���� �÷��̾��� ��� ���콺 ��ġ ��� ȸ�� ó��
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 dir = transform.localPosition - mousePosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        _targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = _targetRotation;

        // ���콺 ��ġ�� ���� flipX �� gunTip�� ��ġ ����
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

        // �߻� Ű �Է� �� �ݵ� ȿ��
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
    /// �ѱ� �ݵ� ȿ���� Ʈ����
    /// </summary>
    private void TriggerRecoil()
    {
        // �̹� ���� ���� �ݵ� �ڷ�ƾ�� �ִٸ� �ߴ�
        if (_recoilCoroutine != null)
            StopCoroutine(_recoilCoroutine);

        // ���ο� �ݵ� ȿ�� ����
        _recoilCoroutine = StartCoroutine(RecoilRoutine());
    }

    /// <summary>
    /// �ݵ� ȿ�� �ڷ�ƾ
    /// </summary>
    private IEnumerator RecoilRoutine()
    {
        Vector3 recoilPosition = _originalPosition + transform.right * RecoilDistance;

        // �ݵ� ��ġ�� �̵�
        float elapsedTime = 0f;
        while (elapsedTime < RecoilDuration)
        {
            transform.localPosition = Vector3.Lerp(_originalPosition, recoilPosition, elapsedTime / RecoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ�� ����
        elapsedTime = 0f;
        while (elapsedTime < RecoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, _originalPosition, elapsedTime / RecoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ݵ� ȿ�� ����
        transform.localPosition = _originalPosition;
        _recoilCoroutine = null;
    }
}