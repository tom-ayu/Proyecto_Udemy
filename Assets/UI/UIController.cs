using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject gameOverPanel;
    public GameObject playerObject;
    public Camera cameraGameOver;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MostrarGameOver();
        }
    }

    public void ActualizarVida(float vidaActual, float vidaMaxima)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = vidaMaxima;
            healthSlider.value = vidaActual;
        }
    }

    public void MostrarGameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        // Desactiva la cámara del jugador y activa la de Game Over
        if (playerObject != null)
        {
            Camera camJugador = playerObject.GetComponentInChildren<Camera>();
            if (camJugador != null) camJugador.gameObject.SetActive(false);
        }

        if (cameraGameOver != null) cameraGameOver.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BotonReiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BotonSalir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); // Asegúrate de que exista esta escena
    }

    public void OcultarGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Reactiva la cámara del jugador si existe
        if (playerObject != null)
        {
            Camera camJugador = playerObject.GetComponentInChildren<Camera>(true); // busca aunque esté desactivada
            if (camJugador != null)
            {
                camJugador.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se encontró la cámara del jugador.");
            }
        }

        // Desactiva la cámara del Game Over
        if (cameraGameOver != null)
            cameraGameOver.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f; // Reanuda el tiempo
    }
}