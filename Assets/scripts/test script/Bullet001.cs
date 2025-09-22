using UnityEngine;

public class Bullet001 : MonoBehaviour
{
    public float speed = 10f; // �������� ����
    public int damage = 20; // ����, ������� ������� ����
    private Vector2 direction; // ����������� �������� ����

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // ������������� ������, ���� ����� ������
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        // ������� ���� � �������� �����������
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ����������� �� ���� � �������
        if (collision.CompareTag("Player"))
        {
            // �������� ��������� Health � ������ � ������� ����
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // ���������� ���� ����� ���������
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // ���������� ���� ��� ������������ � ������������
            Destroy(gameObject);
        }
    }
}