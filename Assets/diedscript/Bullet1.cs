using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 10f; // Скорость пули
    public int damage = 20; // Урон, который наносит пуля

    private void Update()
    {
        // Двигаем пулю вперед
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, столкнулась ли пуля с игроком
        if (collision.CompareTag("Player"))
        {
            // Получаем компонент Health у игрока и наносим ему урон
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Уничтожаем пулю после столкновения
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Уничтожаем пулю при столкновении с препятствиями
            Destroy(gameObject);
        }
    }



}
