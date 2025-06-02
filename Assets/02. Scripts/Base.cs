using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public int HP = 100;
    public Image hpBar;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (hpBar != null)
        {
            StartCoroutine(HitAnimation());
            hpBar.DOFillAmount((float)HP / 100f, 0.5f).SetEase(Ease.InOutQuad);
        }
        if (HP <= 0)
        {
            Die();
        }
    }
    IEnumerator HitAnimation()
    {
        spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(() =>
        {
            spriteRenderer.DOColor(Color.white, 0.1f);
        });
        yield return null;
    }
    void Die()
    {
        Debug.Log("Base destroyed!");
        Destroy(gameObject);
    }
}
