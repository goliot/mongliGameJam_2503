using DG.Tweening;
using UnityEngine;
using System;
using System.Collections;

public class DashAfterImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _targetSprite;
    public float DisappearTime;

    private void OnEnable()
    {
        Invoke("SetFalse", DisappearTime);
    }

    private void Update()
    {
        if(_targetSprite.flipX)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void SetFalse()
    {
        gameObject.SetActive(false);
    }
}