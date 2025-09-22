using UnityEngine;

public class EnemyShootg : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда будут вылетать пули
    public float fireRate = 1f; // Частота стрельбы
    public float detectionRange = 10f; // Дальность обнаружения игрока

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        // Находим игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Проверяем, находится ли игрок в пределах дальности обнаружения
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Определяем направление к игроку
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Передаем направление в скрипт пули
        Bullet001 bulletScript = bullet.GetComponent<Bullet001>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}