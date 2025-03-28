using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;  // ���� ����
    public float explosionForce = 700f; // ���� ��
    public float explosionDelay = 3f;   // ���� ������

    private bool hasExploded = false; // �ߺ� ���� ����

    void Start()
    {
        // ���� �ð��� ������ �����ϵ��� ����
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // ���� ȿ�� (��: ��ƼŬ, ����)
        Debug.Log("Boom!");

        // �ֺ� ������Ʈ ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // ���� �� ������Ʈ��� �������� �ִ� ���� �߰� ����
        }

        // ����ź ������Ʈ ����
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // ���� ������ �ð������� ǥ��
        Gizmos.color = new Color(1, 0, 0, 0.5f); // ������ ������
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
