using UnityEngine;
using System.Collections; // Добавлено для использования IEnumerator

public class MovingBlockWithDifferentSpeeds : MonoBehaviour
{
    public Transform pointA;      // Начальная точка (верх)
    public Transform pointB;      // Конечная точка (низ)
    public float fallSpeed = 5f;  // Скорость падения вниз
    public float riseSpeed = 2f;  // Скорость подъема вверх
    public float damageAngleThreshold = 45f; // Угол, при котором наносится урон (сверху)
    public float pauseAtTopDuration = 2f; // Время задержки на точке A в секундах

    private Vector3 targetPosition;
    private bool isFalling = true; // true = падает вниз, false = поднимается вверх
    private bool isPaused = false; // Флаг паузы

    void Start()
    {
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Если на паузе - не двигаемся
        if (isPaused) return;

        float currentSpeed = isFalling ? fallSpeed : riseSpeed;

        // Плавное движение к целевой позиции
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );

        // Если достигнута точка A или B, меняем направление
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (targetPosition == pointB.position)
            {
                targetPosition = pointA.position;
                isFalling = false; // Теперь блок поднимается
            }
            else
            {
                // При достижении точки A запускаем паузу
                StartCoroutine(PauseAtTop());
            }
        }
    }

    // Корутина для паузы на точке A
    private IEnumerator PauseAtTop()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseAtTopDuration);

        // После паузы продолжаем движение вниз
        targetPosition = pointB.position;
        isFalling = true;
        isPaused = false;
    }

    // Убиваем игрока при столкновении сверху
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Проверяем, что удар пришел сверху
            if (IsHitFromTop(collision))
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerHealth._maxHealth);
                    Debug.Log("Игрок раздавлен блоком!");
                }
            }
        }
    }

    // Проверяем, что столкновение произошло сверху блока
    private bool IsHitFromTop(Collision2D collision)
    {
        // Для каждого контакта в столкновении
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Вычисляем угол между нормалью контакта и вектором вверх
            float angle = Vector2.Angle(contact.normal, Vector2.up);

            // Если угол меньше порогового значения, значит удар сверху
            if (angle <= damageAngleThreshold)
            {
                return true;
            }
        }
        return false;
    }
}