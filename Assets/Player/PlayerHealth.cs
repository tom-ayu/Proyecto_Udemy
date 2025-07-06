using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerHealth : MonoBehaviour, IDañable
{
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject resumeButton;

    private UIController uiController;

    void Start()
    {
        currentHealth = maxHealth;
        uiController = FindObjectOfType<UIController>();
        uiController.ActualizarVida(currentHealth, maxHealth);
    }

    public void RecibirDaño(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiController.ActualizarVida(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Morir();
    }

    void Morir()
    {
        if (uiController != null)
            uiController.MostrarPause();
            resumeButton.SetActive(false);

        PlayerController control = GetComponent<PlayerController>();
        if (control != null) control.enabled = false;

        PlayerShooter shooter = GetComponent<PlayerShooter>();
        if (shooter != null) shooter.enabled = false;
    }
}