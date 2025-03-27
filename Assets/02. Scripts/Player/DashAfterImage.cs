using DG.Tweening;
using UnityEngine;
using System;
using System.Collections;

public class DashAfterImage : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private SpriteRenderer _targetSprite;
    private SpriteRenderer _sr;

    private bool _enableDistanceCheck = false;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _targetSprite = _target.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _enableDistanceCheck = false;
        _sr.flipX = _targetSprite.flipX;
        transform.position = _target.position;
        StartCoroutine(EnableDistanceCheck());
    }

    private IEnumerator EnableDistanceCheck()
    {
        yield return null; // �� ������ ��ٸ�
        yield return new WaitForSeconds(0.05f); // �߰��� 0.05�� �� ��ٸ�
        _enableDistanceCheck = true;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref velocity, smoothTime);

        // �Ÿ� üũ �� ��Ȱ��ȭ
        if (_enableDistanceCheck && Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
}