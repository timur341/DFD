using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int _maxHealth = 100;
    public int currentHealth;
    public Transform spawnPoint;
    public GameObject defeatPanel;

    [SerializeField] private HealthUI healthUI; // —сылка на HealthUI

    private void Start()
    {
        currentHealth = _maxHealth;
        Respawn();

        if (healthUI != null)
            healthUI.UpdateHealthText(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthUI != null)
            healthUI.UpdateHealthText(currentHealth);

        if (currentHealth <= 0)
        {
            defeatPanel.SetActive(true);
            //Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        Respawn();
        ReloadLevel();
    }

    void Update()
    {
        if (transform.position.y < -200f)
        {
            TakeDamage(100);
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}
