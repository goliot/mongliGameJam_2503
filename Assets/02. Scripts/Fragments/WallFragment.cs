using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float ForcePower;
    public float Timer;
    public float DisappearTime = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Timer = DisappearTime;
        _rb.AddForce(new Vector3(Random.Range(-1, 1), 1, 0) * ForcePower, ForceMode2D.Impulse);
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            PoolManager.Instance.ReturnObject(gameObject, EObjectType.WallFragment);
        }
    }
}