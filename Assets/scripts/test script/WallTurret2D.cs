using UnityEngine;
using System.Collections;

public class WallTurret2D : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private int damage = 1;
    [SerializeField] private Vector2 shootDirection = Vector2.left;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool detectPlayer = true;
    [SerializeField] private bool detectEnemies = false;

    [Header("Visual Effects")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip shootSound;

    private float nextFireTime;
    private Transform target;
    private bool targetInRange;

    private void Start()
    {
        // Нормализуем направление стрельбы
        shootDirection = shootDirection.normalized;

        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    private void Update()
    {
        FindTarget();

        if (targetInRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void FindTarget()
    {
        targetInRange = false;

        // Определяем какие объекты искать
        string targetTag = "";
        if (detectPlayer && detectEnemies) targetTag = "Player|Enemy";
        else if (detectPlayer) targetTag = "Player";
        else if (detectEnemies) targetTag = "Enemy";

        if (string.IsNullOrEmpty(targetTag)) return;

        // Ищем все цели в радиусе
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, detectionRange, targetLayer);

        foreach (Collider2D potentialTarget in targets)
        {
            if (potentialTarget.CompareTag("Player") || potentialTarget.CompareTag("Enemy"))
            {
                target = potentialTarget.transform;
                targetInRange = true;
                break;
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null) return;

        // Создаем снаряд
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Направление стрельбы (либо в цель, либо заданное направление)
        Vector2 direction = targetInRange && target != null
            ? (target.position - firePoint.position).normalized
            : shootDirection;

        // Задаем скорость снаряду
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

        // Настраиваем урон снаряда
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDamage(damage);
        }

        // Эффекты выстрела
        PlayShootEffects();
    }

    private void PlayShootEffects()
    {
        // Эффект дульного вспышки
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Звук выстрела
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Отрисовка радиуса обнаружения в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Отрисовка направления стрельбы
        Gizmos.color = Color.yellow;
        Vector2 direction = shootDirection.normalized;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * 2f);
    }
}