using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ��������
    private int currentHealth;

    public Transform spawnPoint; // ����� ������

    private void Start()
    {
        currentHealth = maxHealth; // ������������� �������� ��������
        Respawn(); // ��������� ������ �� �����-����� ��� ������
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� ��������

        if (currentHealth <= 0)
        {
            Die(); // ���� �������� <= 0, �������� ����� Die()
        }
    }

    private void Die()
    {
        Debug.Log("����� �����!"); // ������ ������ ������
        Destroy(gameObject); // ���������� ������ ������
        ReloadLevel(); // ������������ ������
    }

    private void ReloadLevel()
    {
        // �������� ������� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
       

        // ����� ������������ ����� ����� ����� � ������ Start()
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position; // ����������� ������ �� �����-�����
    }
}