using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(5f, 5f);

    [Header("Опционально")]
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private bool drawGizmos = true;

    private float timer;
    private int currentEnemyCount = 0;

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // Добавляем компонент-слушатель к врагу
        EnemyDeathListener listener = newEnemy.AddComponent<EnemyDeathListener>();
        listener.Initialize(this);
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}

// Вспомогательный компонент для отслеживания смерти врага
public class EnemyDeathListener : MonoBehaviour
{
    private EnemySpawner spawner;

    public void Initialize(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnDestroy()
    {
        // Если объект уничтожается и спавнер ещё существует
        if (spawner != null)
        {
            spawner.OnEnemyDestroyed();
        }
    }
}