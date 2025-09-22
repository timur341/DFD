using UnityEngine;
using UnityEngine.UI; // Не забудь подключить для работы с UI

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Text healthText; // Сериализуемая переменная для текста здоровья

    // Метод для обновления текста здоровья
    public void UpdateHealthText(int health)
    {
        if (healthText != null) // Проверяем, что ссылка на текст есть
        {
            healthText.text = $" HP: {health}"; // Обновляем текст
        }
        else
        {
            Debug.LogWarning("HealthText reference is missing!"); // Предупреждение, если текстовое поле не назначено
        }
    }

    // Пример использования (можно вызвать из другого скрипта)
    private void ExampleUsage()
    {
        UpdateHealthText(100); // Установит текст "Health: 100"
    }
}