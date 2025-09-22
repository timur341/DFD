using UnityEngine;

public class EnemyShootg : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public Transform firePoint; // �����, ������ ����� �������� ����
    public float fireRate = 1f; // ������� ��������
    public float detectionRange = 10f; // ��������� ����������� ������

    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        // ������� ������ �� ���� "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ���������, ��������� �� ����� � �������� ��������� �����������
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
        // ������� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // ���������� ����������� � ������
        Vector2 direction = (player.position - firePoint.position).normalized;

        // �������� ����������� � ������ ����
        Bullet001 bulletScript = bullet.GetComponent<Bullet001>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
    }
}