using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, ������ ����� �������� ����
    public float fireRate = 1f; // ������� ��������
    public float detectionRange = 10f; // ������ ����������� ������
    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        // ����� ������ � ����� "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ���������, ��������� �� ����� � �������� ������� �����������
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        // ������� ���� � ����� �������� � �������� �����������
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
