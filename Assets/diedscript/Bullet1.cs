using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 10f; // �������� ����
    public int damage = 20; // ����, ������� ������� ����

    private void Update()
    {
        // ������� ���� ������
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������, ����������� �� ���� � �������
        if (collision.CompareTag("Player"))
        {
            // �������� ��������� Health � ������ � ������� ��� ����
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // ���������� ���� ����� ������������
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // ���������� ���� ��� ������������ � �������������
            Destroy(gameObject);
        }
    }



}
