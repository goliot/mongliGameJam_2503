using DG.Tweening;
using UnityEngine;

public class Deco : MonoBehaviour
{
    private SpriteRenderer _sr;

    private int _hitCounter = 3;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            _sr.DOColor(Color.yellow, 0.2f);
            _hitCounter--;

            PoolManager.Instance.ReturnObject(collision.gameObject, EObjectType.Bullet);

            if(_hitCounter <= 0)
            {
                BreakRoutine();
            }
        }
    }

    private void BreakRoutine()
    {
        Destroy(gameObject);
    }
}