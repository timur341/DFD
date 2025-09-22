using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    // Минимальная дистанция видимости игрока
    public float viewRadius = 5f;

    // Игровой объект игрока
    private Transform playerTransform;

    void Start()
    {
        // Получаем ссылку на объект игрока по тегу
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Определяем расстояние до игрока
            float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Если игрок попал в зону видимости, поворачиваемся к нему
            if (distToPlayer <= viewRadius)
            {
                LookAtPlayer();
            }
        }
    }

    // Функция для вращения врага к игроку
    void LookAtPlayer()
    {
        // Определение вектора направления к игроку
        Vector2 dir = playerTransform.position - transform.position;
        dir.Normalize();

        // Расчет угла поворота
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Установка нового вращения объекта
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}