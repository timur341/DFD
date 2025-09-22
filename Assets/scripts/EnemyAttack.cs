using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ������ ������ (����� ������ ����� ��� Player)
    private Transform target;

    // ������ ������� �����
    public float attackRange = 1.5f;

    // ����� ����� �������
    public float timeBetweenAttacks = 1f;

    // ���������� ���������� �����
    public int damage = 10;

    // ������ ��������� �����
    private float nextAttackTime = 0f;

    void Start()
    {
        // ����� ������ �� ����
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        // �������� ������� ������
        if (target != null && Time.time >= nextAttackTime)
        {
            // ���������� �� ������
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // �������� ��������� � ������ �����
            if (distanceToTarget <= attackRange)
            {
                Attack(); // ������� ����
                nextAttackTime = Time.time + timeBetweenAttacks; // ������������� ������ ���������� �����
            }
        }
    }

    void Attack()
    {
        // ������ ��������� ����� ������
        Debug.Log("������ ����: " + damage); // ����� �������� ������ ��������/�������

        // ��� ��������� �����������, ��� ����� ����� ��������� Health
        var healthComponent = target.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage); // ����� ���� ���������
        }
    }
}