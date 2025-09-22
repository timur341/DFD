using UnityEngine;
using System.Collections;  // ��������� ��� ������ � IEnumerator

public class EnemyDoT : MonoBehaviour
{
    // �������� ���� (������ Player)
    private Transform target;

    // ��������� �����
    public float attackRange = 1.5f;
    public float timeBetweenAttacks = 1f;
    public int damage = 10;

    // ��������� Damage over Time (DoT)
    public int dotDamage = 2;       // ���� � �������
    public float dotDuration = 3f;  // ������������ ������� (� ��������)

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
        Debug.Log($"������ ����: {damage}");

        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);  // �������� ����
            StartCoroutine(ApplyDot(healthComponent));  // ��������� DoT
        }
    }

    // �������� ��� DoT-�������
    private IEnumerator ApplyDot(Health health)
    {
        float endTime = Time.time + dotDuration;

        while (Time.time < endTime && health.currentHealth > 0)
        {
            yield return new WaitForSeconds(1f);  // ��� 1 �������
            health.TakeDamage(dotDamage);
            Debug.Log($"DoT: �������� {dotDamage} �����. ��������: {health.currentHealth}");
        }
    }
}