using UnityEngine;
using System.Collections; // ��������� ��� ������������� IEnumerator

public class MovingBlockWithDifferentSpeeds : MonoBehaviour
{
    public Transform pointA;      // ��������� ����� (����)
    public Transform pointB;      // �������� ����� (���)
    public float fallSpeed = 5f;  // �������� ������� ����
    public float riseSpeed = 2f;  // �������� ������� �����
    public float damageAngleThreshold = 45f; // ����, ��� ������� ��������� ���� (������)
    public float pauseAtTopDuration = 2f; // ����� �������� �� ����� A � ��������

    private Vector3 targetPosition;
    private bool isFalling = true; // true = ������ ����, false = ����������� �����
    private bool isPaused = false; // ���� �����

    void Start()
    {
        targetPosition = pointB.position;
    }

    void Update()
    {
        // ���� �� ����� - �� ���������
        if (isPaused) return;

        float currentSpeed = isFalling ? fallSpeed : riseSpeed;

        // ������� �������� � ������� �������
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );

        // ���� ���������� ����� A ��� B, ������ �����������
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (targetPosition == pointB.position)
            {
                targetPosition = pointA.position;
                isFalling = false; // ������ ���� �����������
            }
            else
            {
                // ��� ���������� ����� A ��������� �����
                StartCoroutine(PauseAtTop());
            }
        }
    }

    // �������� ��� ����� �� ����� A
    private IEnumerator PauseAtTop()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseAtTopDuration);

        // ����� ����� ���������� �������� ����
        targetPosition = pointB.position;
        isFalling = true;
        isPaused = false;
    }

    // ������� ������ ��� ������������ ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������, ��� ���� ������ ������
            if (IsHitFromTop(collision))
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerHealth._maxHealth);
                    Debug.Log("����� ��������� ������!");
                }
            }
        }
    }

    // ���������, ��� ������������ ��������� ������ �����
    private bool IsHitFromTop(Collision2D collision)
    {
        // ��� ������� �������� � ������������
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // ��������� ���� ����� �������� �������� � �������� �����
            float angle = Vector2.Angle(contact.normal, Vector2.up);

            // ���� ���� ������ ���������� ��������, ������ ���� ������
            if (angle <= damageAngleThreshold)
            {
                return true;
            }
        }
        return false;
    }
}