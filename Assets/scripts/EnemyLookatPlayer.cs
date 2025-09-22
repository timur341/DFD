using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    // ����������� ��������� ��������� ������
    public float viewRadius = 5f;

    // ������� ������ ������
    private Transform playerTransform;

    void Start()
    {
        // �������� ������ �� ������ ������ �� ����
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // ���������� ���������� �� ������
            float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // ���� ����� ����� � ���� ���������, �������������� � ����
            if (distToPlayer <= viewRadius)
            {
                LookAtPlayer();
            }
        }
    }

    // ������� ��� �������� ����� � ������
    void LookAtPlayer()
    {
        // ����������� ������� ����������� � ������
        Vector2 dir = playerTransform.position - transform.position;
        dir.Normalize();

        // ������ ���� ��������
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // ��������� ������ �������� �������
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}