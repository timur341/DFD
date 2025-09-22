using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда будут вылетать пули
    public float fireRate = 1f; // Частота стрельбы
    public float detectionRange = 10f; // Радиус обнаружения игрока
    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        // Найти объект с тегом "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Проверяем, находится ли игрок в пределах радиуса обнаружения
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
        // Создаем пулю в точке стрельбы с заданной ориентацией
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
