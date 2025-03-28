using DG.Tweening;
using UnityEngine;
using System.Collections;

public class Deco : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private DropItemDataSO dropItem;
    [SerializeField] private AudioClip[] cilps;

    private int _hitCounter = 3;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            //GetComponent<AudioSource>().PlayOneShot(cilps[0]);
            GetComponent<AudioSource>().Play();
            _sr.DOColor(Color.yellow, 0.1f).OnComplete(() => _sr.DOColor(Color.white, 0.1f));
            transform.DOShakePosition(0.1f, 0.01f);
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
        float rate = Random.value;
        if(rate < 0.2f)
        {
            GameObject go = PoolManager.Instance.GetObject(dropItem.Items[Random.Range(0, dropItem.Items.Length)]);
            go.transform.position = transform.position;
        }

        Destroy(gameObject);
    }
}