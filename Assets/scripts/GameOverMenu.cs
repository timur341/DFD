using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth;
    public Transform spawnPoint; // Точка спавна
    public GameObject defeatPanel;

    private void Start()
    {
        currentHealth = maxHealth; // Инициализация текущего здоровья
       // Respawn(); // Появление игрока на спавн-точке при старте
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        // Логика смерти игрока (например, перезапуск уровня или игра окончена)
        Debug.Log("Player has died!");
       // Respawn(); // Уничтожаем объект игрока
        //ReloadLevel(); // Перезагрузка уровня
    }

    void Update()
    {
        if (transform.position.y < -200f)
        {
            TakeDamage(100);
        }
        // Проверяем состояние здоровья
        if (maxHealth < 0)
        {
            // Включаем панель проигрыша
            defeatPanel.SetActive(true);

           
        }

    }
   // private void ReloadLevel()
   // {
        // Получаем текущую сцену
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1f;


        // После перезагрузки сцены игрок будет в методе Start()
  //  }

   // public void Respawn()
   // {
      //  transform.position = spawnPoint.position; // Перемещение игрока на спавн-точку
   // }
}
