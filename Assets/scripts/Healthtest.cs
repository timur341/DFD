using UnityEngine;
using UnityEngine.UI; // �� ������ ���������� ��� ������ � UI

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Text healthText; // ������������� ���������� ��� ������ ��������

    // ����� ��� ���������� ������ ��������
    public void UpdateHealthText(int health)
    {
        if (healthText != null) // ���������, ��� ������ �� ����� ����
        {
            healthText.text = $" HP: {health}"; // ��������� �����
        }
        else
        {
            Debug.LogWarning("HealthText reference is missing!"); // ��������������, ���� ��������� ���� �� ���������
        }
    }

    // ������ ������������� (����� ������� �� ������� �������)
    private void ExampleUsage()
    {
        UpdateHealthText(100); // ��������� ����� "Health: 100"
    }
}