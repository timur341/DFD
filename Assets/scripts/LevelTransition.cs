using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevelName; // Имя следующего уровня

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            // Загружаем следующий уровень
            SceneManager.LoadScene(nextLevelName);
        }
    }
}