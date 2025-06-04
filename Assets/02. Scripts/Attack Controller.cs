using UnityEngine;

public class AttackController : MonoBehaviour
{
    private CrosshairManager crosshairManager;
    private Camera mainCamera;
    private AudioSource audioSource;

    public float attackSpeed = 2.5f;
    private float nextAttackTime = 0f;

    public float criticalChance = 0.3f;
    public int attackDamage = 10;
    public int criticalDamage = 20;

    [Header("Audio Clips")]
    public AudioClip attackSound;
    public AudioClip criticalSound;

    void Start()
    {
        crosshairManager = FindFirstObjectByType<CrosshairManager>();
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextAttackTime)
            {
                bool isCritical = Random.value < criticalChance;
                TryAttack(isCritical);
                PlayAttackSound(isCritical);
                nextAttackTime = Time.time + (1f / attackSpeed);
            }
        }
    }

    void TryAttack(bool isCritical)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                int damageToDeal = isCritical ? criticalDamage : attackDamage;

                enemy.TakeDamage(damageToDeal);

                crosshairManager.ShowHitMarker(isCritical);
            }
        }
    }

    void PlayAttackSound(bool isCritical)
    {
        float randomSemitone = Random.Range(-1f, 1f);
        float pitch = Mathf.Pow(1.059463f, randomSemitone);
        audioSource.pitch = pitch;

        if (isCritical)
        {
            audioSource.PlayOneShot(criticalSound);
        }
        else
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}
