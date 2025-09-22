using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Impact Effects")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float lifetime = 3f;

    public int damage = 20; // ����, ������� ������� ����

    public void SetDamage(int amount)
    {
        damage = amount;
    }

    private void Start()
    {
        // �������������� ����������� ����� �������� �����
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �� ��������� �� �������� � ������ �������
        if (other.isTrigger || other.transform == transform.parent)
            return;

        // ������� ���� ���� � ���� ���� Health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // ����������� ������� ���������
        PlayImpactEffects();

        // ���������� ������
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