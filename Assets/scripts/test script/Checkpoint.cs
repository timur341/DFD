using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private ParticleSystem activationParticles;

    [Header("Audio")]
    [SerializeField] private AudioClip activateSound;

    private SpriteRenderer spriteRenderer;
    private bool isActive = false;
    private Transform playerSpawnPoint; // Кэшируем spawnPoint игрока

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inactiveSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            // Получаем Health игрока
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth == null) return;

            // Кэшируем spawnPoint игрока
            playerSpawnPoint = playerHealth.spawnPoint;

            // Создаем новый GameObject для точки возрождения
            GameObject newSpawnPoint = new GameObject("SpawnPoint_" + gameObject.name);
            newSpawnPoint.transform.position = transform.position;

            // Подменяем spawnPoint в Health
            playerHealth.spawnPoint = newSpawnPoint.transform;

            // Активируем визуал
            isActive = true;
            spriteRenderer.sprite = activeSprite;

            // Запускаем эффекты
            if (activationParticles != null)
                activationParticles.Play();

            if (activateSound != null)
                AudioSource.PlayClipAtPoint(activateSound, transform.position);

            Debug.Log($"Checkpoint activated! New spawn: {newSpawnPoint.transform.position}");
        }
    }

    // Очищаем созданные объекты при уничтожении
    private void OnDestroy()
    {
        if (playerSpawnPoint != null && playerSpawnPoint.name.StartsWith("SpawnPoint_"))
        {
            Destroy(playerSpawnPoint.gameObject);
        }
    }
}