using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;
    public float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            FragmentWithWall();
            PoolManager.Instance.ReturnObject(gameObject, EObjectType.Bullet);
        }

        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage(Damage);
            PoolManager.Instance.ReturnObject(gameObject, EObjectType.Bullet);
        }
    }

    private void FragmentWithWall()
    {
        for(int i=0; i<3; i++)
        {
            GameObject go = PoolManager.Instance.GetObject(EObjectType.WallFragment);
            go.transform.position = transform.localPosition + Vector3.up;
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
