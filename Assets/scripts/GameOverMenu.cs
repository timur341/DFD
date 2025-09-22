using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public int maxHealth = 100; // ������������ ��������
    private int currentHealth;
    public Transform spawnPoint; // ����� ������
    public GameObject defeatPanel;

    private void Start()
    {
        currentHealth = maxHealth; // ������������� �������� ��������
       // Respawn(); // ��������� ������ �� �����-����� ��� ������
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ��������� ��������
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        // ������ ������ ������ (��������, ���������� ������ ��� ���� ��������)
        Debug.Log("Player has died!");
       // Respawn(); // ���������� ������ ������
        //ReloadLevel(); // ������������ ������
    }

    void Update()
    {
        if (transform.position.y < -200f)
        {
            TakeDamage(100);
        }
        // ��������� ��������� ��������
        if (maxHealth < 0)
        {
            // �������� ������ ���������
            defeatPanel.SetActive(true);

           
        }

    }
   // private void ReloadLevel()
   // {
        // �������� ������� �����
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1f;


        // ����� ������������ ����� ����� ����� � ������ Start()
  //  }

   // public void Respawn()
   // {
      //  transform.position = spawnPoint.position; // ����������� ������ �� �����-�����
   // }
}
