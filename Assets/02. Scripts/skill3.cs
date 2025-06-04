using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill3_SlowZone : MonoBehaviour
{
    public float radius = 10f;               // 둔화 범위
    public float slowFactor = 0.5f;         // 속도 감소율
    public float duration = 5f;             // 지속 시간

    private float timer = 0f;

    void Start()
    {
        // 설정된 시간 후 파괴
        Destroy(gameObject, duration);
    }

    void Update()
    {
        timer += Time.deltaTime;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hitColliders)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                // 슬로우가 새로 적용될 때만 출력
                if (!enemy.GetComponent<SpriteRenderer>().color.Equals(Color.cyan))
                    Debug.Log($"[Skill3] 감속 대상 적: {enemy.name}");
                enemy.ApplySlow(slowFactor, 5f); // 적에게 감속 효과 부여
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

