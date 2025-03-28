using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 5f;  // 폭발 범위
    public float explosionForce = 700f; // 폭발 힘
    public float explosionDelay = 3f;   // 폭발 딜레이

    private bool hasExploded = false; // 중복 폭발 방지

    void Start()
    {
        // 일정 시간이 지나면 폭발하도록 설정
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // 폭발 효과 (예: 파티클, 사운드)
        Debug.Log("Boom!");

        // 주변 오브젝트 감지
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // 만약 적 오브젝트라면 데미지를 주는 로직 추가 가능
        }

        // 수류탄 오브젝트 제거
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // 폭발 범위를 시각적으로 표시
        Gizmos.color = new Color(1, 0, 0, 0.5f); // 반투명 빨간색
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
