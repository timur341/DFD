using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Объект игрока (игрок должен иметь тег Player)
    private Transform target;

    // Радиус ближней атаки
    public float attackRange = 1.5f;

    // Время между ударами
    public float timeBetweenAttacks = 1f;

    // Количество наносимого урона
    public int damage = 10;

    // Таймер следующей атаки
    private float nextAttackTime = 0f;

    void Start()
    {
        // Поиск игрока по тегу
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // Проверка наличия игрока
        if (target != null && Time.time >= nextAttackTime)
        {
            // Расстояние до игрока
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // Проверка попадания в радиус атаки
            if (distanceToTarget <= attackRange)
            {
                Attack(); // Наносим удар
                nextAttackTime = Time.time + timeBetweenAttacks; // Устанавливаем таймер следующего удара
            }
        }
    }

    void Attack()
    {
        // Логика нанесения урона игроку
        Debug.Log("Нанесён урон: " + damage); // Можно добавить эффект анимации/частицы

        // Для упрощения предположим, что игрок имеет компонент Health
        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage); // Нести урон персонажу
        }
    }
}