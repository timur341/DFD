using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject coinPrefab; // ������ ������
    public int numberOfCoins = 3; // ���������� �����, ������� ��������
    public float dropRadius = 1f; // ������, � ������� ����� �������� ������

    public int health = 2;
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy (gameObject);
        DropCoins();
    }
    void DropCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * dropRadius;
            dropPosition.y = transform.position.y; // ���������, ��� ������ �� ������ ����
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }


}
