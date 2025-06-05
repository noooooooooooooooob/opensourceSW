using UnityEngine;
using System.Collections;

public class Skill4_SatelliteStrike : MonoBehaviour
{
    public float delay = 2f;                   // ���� ��� �ð�
    public float radius = 4f;                  // ���� ����
    public int damage = 100;                   // ������
    public GameObject explosionEffect;         // ���� ����Ʈ ������

    void Start()
    {
        Debug.Log("[Skill4] ���� ���� ������ @ " + transform.position);
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("[Skill4] ���� �߻� @ " + transform.position);

        // ���� ����Ʈ ����
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // �浹 ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        Debug.Log("[Skill4] ������ ������Ʈ ��: " + hits.Length);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("[Skill4] �� ������: " + hit.name + " (Enemy)");

                enemy.TakeDamage(damage);
                SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.red; // �ð������� ǥ�� (������)
                }
            }
            else
            {
                Debug.Log("[Skill4] ������ (Enemy �ƴ�): " + hit.name);
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