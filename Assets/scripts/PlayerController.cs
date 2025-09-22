using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth;

    public Transform spawnPoint; // Точка спавна

    private void Start()
    {
        currentHealth = maxHealth; // Инициализация текущего здоровья
        Respawn(); // Появление игрока на спавн-точке при старте
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье

        if (currentHealth <= 0)
        {
            Die(); // Если здоровье <= 0, вызываем метод Die()
        }
    }

    private void Die()
    {
        Debug.Log("Игрок погиб!"); // Логика смерти игрока
        Destroy(gameObject); // Уничтожаем объект игрока
        ReloadLevel(); // Перезагрузка уровня
    }

    private void ReloadLevel()
    {
        // Получаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
       

        // После перезагрузки сцены игрок будет в методе Start()
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position; // Перемещение игрока на спавн-точку
    }
}