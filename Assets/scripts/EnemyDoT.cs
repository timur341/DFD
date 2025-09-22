using UnityEngine;
using System.Collections;  // Добавляем для работы с IEnumerator

public class EnemyDoT : MonoBehaviour
{
    // Основная цель (обычно Player)
    private Transform target;

    // Параметры атаки
    public float attackRange = 1.5f;
    public float timeBetweenAttacks = 1f;
    public int damage = 10;

    // Параметры Damage over Time (DoT)
    public int dotDamage = 2;       // Урон в секунду
    public float dotDuration = 3f;  // Длительность эффекта (в секундах)

    private float nextAttackTime = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target != null && Time.time >= nextAttackTime)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                Attack();
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
    }

    void Attack()
    {
        Debug.Log($"Нанесён урон: {damage}");

        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);  // Основной урон
            StartCoroutine(ApplyDot(healthComponent));  // Запускаем DoT
        }
    }

    // Корутина для DoT-эффекта
    private IEnumerator ApplyDot(Health health)
    {
        float endTime = Time.time + dotDuration;

        while (Time.time < endTime && health.currentHealth > 0)
        {
            yield return new WaitForSeconds(1f);  // Ждём 1 секунду
            health.TakeDamage(dotDamage);
            Debug.Log($"DoT: Нанесено {dotDamage} урона. Здоровье: {health.currentHealth}");
        }
    }
}