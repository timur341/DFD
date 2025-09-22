using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Impact Effects")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float lifetime = 3f;

    public int damage = 20; // Урон, который наносит пуля

    public void SetDamage(int amount)
    {
        damage = amount;
    }

    private void Start()
    {
        // Автоматическое уничтожение через заданное время
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Не реагируем на триггеры и самого стрелка
        if (other.isTrigger || other.transform == transform.parent)
            return;

        // Наносим урон если у цели есть Health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Проигрываем эффекты попадания
        PlayImpactEffects();

        // Уничтожаем снаряд
        Destroy(gameObject);
    }

    private void PlayImpactEffects()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        if (impactSound != null)
        {
            AudioSource.PlayClipAtPoint(impactSound, transform.position);
        }
    }
}