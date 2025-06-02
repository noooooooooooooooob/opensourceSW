using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public int damage;
    public float speed;
    public float attackRange;
    Animator animator;
    SpriteRenderer spritetrenderer;
    Transform target;
    bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spritetrenderer = GetComponent<SpriteRenderer>();
        animator.SetTrigger("Run");
        target = GameObject.FindGameObjectWithTag("base").transform;
        if(target.position.x - transform.position.x < 0)
        {
            spritetrenderer.flipX = true; // Face left
        }
        else
        {
            spritetrenderer.flipX = false; // Face right
        }
    }
    void Update()
    {
        if (target != null && !isAttacking)
        {
            MoveTowardsTarget();
        }
    }
    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // if (Vector3.Distance(transform.position, target.position) <= attackRange)
        // {
        //     Attack();
        // }
    }
    void Attack()
    {
        Vector2 diff = target.position - transform.position;

        if (diff.y > 1.0f)
        {
            animator.SetTrigger("AttackUp");
        }
        else if (diff.y < -1.0f)
        {
            animator.SetTrigger("AttackDown");
        }
        else if (diff.x >= 0)
        {
            spritetrenderer.flipX = false; // Face right
            animator.SetTrigger("Attack");
        }
        else if (diff.x < 0)
        {
            spritetrenderer.flipX = true; // Face left
            animator.SetTrigger("Attack");
        }

        isAttacking = true;
    }
    public void DoDamage()
    {
        if (target != null)
        {
            // Assuming the target has a method to take damage
            Base baseTarget = target.GetComponent<Base>();
            if (baseTarget != null)
            {
                baseTarget.TakeDamage(damage);
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            Die();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("base"))
        {
            Attack();
        }
    }
    void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f); // Delay to allow death animation to play
    }

}
