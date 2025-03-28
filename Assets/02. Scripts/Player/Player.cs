using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDataSO PlayerData;

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Min(PlayerData.MaxHp, value);
        }
    }
    private float _sheild;
    public float Sheild
    {
        get => _sheild;
        set
        {
            _sheild = Mathf.Min(PlayerData.MaxSheild, value);
        }
    }
    private float _bulletCount;
    public float BulletCount
    {
        get => _bulletCount;
        set
        {
            _bulletCount = Mathf.Min(value, PlayerData.MaxBullet);
        }
    }

    Coroutine _reloadCoroutine;

    private void Awake()
    {
        _health = PlayerData.MaxHp;
        _bulletCount = PlayerData.MaxBullet;
        _sheild = PlayerData.MaxSheild;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Debug.Log(collision.tag);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if(Input.GetMouseButtonDown(0) && _reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }
    }

    private void Reload()
    {
        _reloadCoroutine = StartCoroutine(CoReload());
    }

    IEnumerator CoReload()
    {
        while(_bulletCount < PlayerData.MaxBullet)
        {
            // cross hair 리로드 전용으로 바꾸기
            _bulletCount += 1;
            yield return new WaitForSeconds(0.1f);
        }
        _reloadCoroutine = null;
    }

    public void TakeDamage(float damage)
    {
        _sheild -= damage;

        if(_sheild <= 0)
        {
            _health += _sheild;
            _sheild = 0;
        }
        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO : Die 연출
    }
}