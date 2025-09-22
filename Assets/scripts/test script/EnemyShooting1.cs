using System.Collections;
using UnityEngine;

public class EnemyShooting1 : MonoBehaviour
{
    // ����� (����)
    public Transform playerTransform;

    // ������ �������
    public GameObject bulletPrefab;

    // �������� ������ �������
    public float bulletSpeed = 8f;

    // �������� ��������
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (playerTransform != null && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // ���������� �������������� ����������� ������ ������������ �����
        bool shootRight = playerTransform.position.x > transform.position.x;

        // ����������� ������ ������/�����
        Vector2 direction = shootRight ? Vector2.right : Vector2.left;

        // ������� ������
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // �������� ��������� Rigidbody2D �������
        Rigidbody2D rbBullet = bulletInstance.GetComponent<Rigidbody2D>();

        // ��������� ������ � ������ �����������
        rbBullet.velocity = direction * bulletSpeed;
    }
}