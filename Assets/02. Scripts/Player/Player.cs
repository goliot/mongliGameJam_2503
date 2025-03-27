using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDataSO PlayerData;

    private float _health;

    private void Awake()
    {
        _health = PlayerData.MaxHp;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO : Die ¿¬Ãâ
    }
}