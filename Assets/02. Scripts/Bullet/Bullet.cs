using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            PoolManager.Instance.ReturnObject(gameObject, EObjectType.Bullet);
        }

        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage();
            PoolManager.Instance.ReturnObject(gameObject, EObjectType.Bullet);
        }
    }

    private void Update()
    {
        // 이동
        transform.Translate(Speed * Time.deltaTime * Vector3.right);

        // 회전
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
