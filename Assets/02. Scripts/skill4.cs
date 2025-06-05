using UnityEngine;
using System.Collections;

public class Skill4_SatelliteStrike : MonoBehaviour
{
    public float delay = 2f;                   // 폭격 대기 시간
    public float radius = 4f;                  // 폭발 범위
    public int damage = 100;                   // 데미지
    public GameObject explosionEffect;         // 폭발 이펙트 프리팹

    void Start()
    {
        Debug.Log("[Skill4] 위성 폭격 생성됨 @ " + transform.position);
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("[Skill4] 폭발 발생 @ " + transform.position);

        // 폭발 이펙트 생성
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 충돌 감지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        Debug.Log("[Skill4] 감지된 오브젝트 수: " + hits.Length);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("[Skill4] 적 감지됨: " + hit.name + " (Enemy)");

                enemy.TakeDamage(damage);
                SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.red; // 시각적으로 표시 (빨간색)
                }
            }
            else
            {
                Debug.Log("[Skill4] 감지됨 (Enemy 아님): " + hit.name);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}