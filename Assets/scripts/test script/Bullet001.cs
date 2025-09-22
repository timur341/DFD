using UnityEngine;

public class Bullet001 : MonoBehaviour
{
    public float speed = 10f; // Скорость пули
    public int damage = 20; // Урон, который наносит пуля
    private Vector2 direction; // Направление движения пули

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Разворачиваем спрайт, если летим вправо
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        // Двигаем пулю в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, столкнулась ли пуля с игроком
        if (collision.CompareTag("Player"))
        {
            // Получаем компонент Health у игрока и наносим урон
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Уничтожаем пулю после попадания
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Уничтожаем пулю при столкновении с препятствием
            Destroy(gameObject);
        }
    }
}