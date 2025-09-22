using System.Collections;
using UnityEngine;

public class EnemyShooting1 : MonoBehaviour
{
    // Игрок (цель)
    public Transform playerTransform;

    // Префаб снаряда
    public GameObject bulletPrefab;

    // Скорость полета снаряда
    public float bulletSpeed = 8f;

    // Интервал стрельбы
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (playerTransform != null && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Определяем горизонтальное направление игрока относительно врага
        bool shootRight = playerTransform.position.x > transform.position.x;

        // Нормализуем вектор вправо/влево
        Vector2 direction = shootRight ? Vector2.right : Vector2.left;

        // Создаем снаряд
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Получаем компонент Rigidbody2D снаряда
        Rigidbody2D rbBullet = bulletInstance.GetComponent<Rigidbody2D>();

        // Запускаем снаряд в нужном направлении
        rbBullet.velocity = direction * bulletSpeed;
    }
}