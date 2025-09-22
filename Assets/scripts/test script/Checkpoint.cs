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
    private Transform playerSpawnPoint; // �������� spawnPoint ������

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inactiveSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            // �������� Health ������
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth == null) return;

            // �������� spawnPoint ������
            playerSpawnPoint = playerHealth.spawnPoint;

            // ������� ����� GameObject ��� ����� �����������
            GameObject newSpawnPoint = new GameObject("SpawnPoint_" + gameObject.name);
            newSpawnPoint.transform.position = transform.position;

            // ��������� spawnPoint � Health
            playerHealth.spawnPoint = newSpawnPoint.transform;

            // ���������� ������
            isActive = true;
            spriteRenderer.sprite = activeSprite;

            // ��������� �������
            if (activationParticles != null)
                activationParticles.Play();

            if (activateSound != null)
                AudioSource.PlayClipAtPoint(activateSound, transform.position);

            Debug.Log($"Checkpoint activated! New spawn: {newSpawnPoint.transform.position}");
        }
    }

    // ������� ��������� ������� ��� �����������
    private void OnDestroy()
    {
        if (playerSpawnPoint != null && playerSpawnPoint.name.StartsWith("SpawnPoint_"))
        {
            Destroy(playerSpawnPoint.gameObject);
        }
    }
}