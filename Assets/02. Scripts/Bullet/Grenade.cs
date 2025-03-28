using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;  // ���� ����
    public float explosionForce = 700f; // ���� ��
    public float explosionDelay = 3f;   // ���� ������

    private bool hasExploded = false; // �ߺ� ���� ����

    public List<EObjectType> Parts;

    void OnEnable()
    {
        // ���� �ð��� ������ �����ϵ��� ����
        Invoke("Explode", explosionDelay);
        for(int i=0; i<Parts.Count; i++)
        {
            GameObject go = PoolManager.Instance.GetObject(Parts[i]);
            go.transform.position = transform.position;
        }
        Explode();
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
        PoolManager.Instance.ReturnObject(transform.parent.gameObject, EObjectType.Grenade);
    }

    void OnDrawGizmosSelected()
    {
        // ���� ������ �ð������� ǥ��
        Gizmos.color = new Color(1, 0, 0, 0.5f); // ������ ������
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
