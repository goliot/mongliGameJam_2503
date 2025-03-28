using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("# Player Chase")]
    [SerializeField] private Transform TargetTransform; // ���� ���
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
    public float mouseInfluence = 2.0f; // ���콺 �������� �и��� �ִ� �Ÿ�
    public float maxDistance = 5.0f; // �÷��̾�� �ִ� �Ÿ� ����

    private void Awake()
    {
        TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        _currentPosition = transform.position;

        // ī�޶� ���� ����
        if (_shakeTimer > 0)
        {
            transform.position = _currentPosition + (Vector3)Random.insideUnitCircle * ShakeAmount;
            _shakeTimer -= Time.deltaTime;
            return;
        }

        // �⺻���� �÷��̾� ���� ��ġ
        Vector3 targetPosition = TargetTransform.position + Offset;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D ������ ��� Z�� ����

        // ���콺 ���� ���� ��� (�÷��̾� -> ���콺)
        Vector3 mouseDir = (mousePosition - TargetTransform.position).normalized;

        // ���콺 ���� ���� (�ִ� �Ÿ� ����)
        Vector3 mouseOffset = mouseDir * mouseInfluence;
        mouseOffset = Vector3.ClampMagnitude(mouseOffset, mouseInfluence); // �ִ� ���� �Ÿ� ����

        // ���� ��ǥ ��ġ (���콺 ���� ����)
        Vector3 finalPosition = targetPosition + mouseOffset;

        // �÷��̾���� �ִ� �Ÿ� ����
        Vector3 cameraOffset = finalPosition - TargetTransform.position;
        cameraOffset = Vector3.ClampMagnitude(cameraOffset, maxDistance);

        finalPosition = TargetTransform.position + cameraOffset;

        // �ε巴�� �̵�
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
    }

    public void Shake()
    {
        isShaking = true;
        _shakeTimer = ShakeDuration;
    }
}
