using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Gun;
    public PlayerDataSO PlayerData;
    public GameObject ReloadIcon;
    public bool IsReloading = false;

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
    private int _bulletCount;
    public int BulletCount
    {
        get => _bulletCount;
        set
        {
            _bulletCount = Mathf.Clamp(value, 0, PlayerData.MaxBullet);
          
        }
    }

    Coroutine _reloadCoroutine;

    private void Awake()
    {
        _health = PlayerData.MaxHp;
        _bulletCount = PlayerData.MaxBullet;
        _sheild = PlayerData.MaxSheild;

        UIManager.Instance.SetHealthBar(_health, PlayerData.MaxHp);
        UIManager.Instance.SetSheildBar(_sheild, PlayerData.MaxSheild);
        UIManager.Instance.SetBulletCount((int)_bulletCount);
        UIManager.Instance.SetMaxBulletCount(PlayerData.MaxBullet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Debug.Log(collision.tag);
        }
        if(collision.CompareTag("Item"))
        {
            GetComponents<AudioSource>()[1].Play();
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
        if(GameManager.Instance.IsGameOver)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.R) && _reloadCoroutine == null)
        {
            Reload();
        }
    }

    public void Reload()
    {
        _reloadCoroutine = StartCoroutine(CoReload());
    }

    IEnumerator CoReload()
    {
        IsReloading = true;
        ReloadIcon.SetActive(true);
        while(_bulletCount < PlayerData.MaxBullet)
        {
            // cross hair 리로드 전용으로 바꾸기
            _bulletCount += 1;
            UIManager.Instance.SetBulletCount(_bulletCount);
            yield return new WaitForSeconds(0.1f);
        }
        Gun.GetComponent<AudioSource>().Play();
        ReloadIcon.SetActive(false);
        IsReloading = false;
        _reloadCoroutine = null;
    }

    public void TakeDamage(float damage)
    {
        _sheild -= damage;
        GetComponents<AudioSource>()[0].Play();

        if(_sheild <= 0)
        {
            _health += _sheild;
            _sheild = 0;
        }
        Camera.main.GetComponent<FollowCamera>().Shake(0.5f);
        UIManager.Instance.SetHealthBar(Health, PlayerData.MaxHp);
        UIManager.Instance.SetSheildBar(Sheild, PlayerData.MaxSheild);
        UIManager.Instance.ShowPlayerMask();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }
}