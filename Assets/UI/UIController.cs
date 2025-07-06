using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    [Header("Sección objetos compartidos")]
    public GameObject playerObject;
    public Camera cameraPause;
    [Header("Sección UI")]
    public Slider sliderVida;
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject victoryText;
    [Header("Sección audio")]
    public AudioClip soundPause;
    public AudioClip soundRestart;
    public AudioClip soundExit;
    public AudioClip backgroundMusic;
    public AudioClip victoryMusic;


    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MostrarPause();
        }
    }
    public void ActualizarVida(int actual, int max)
    {
        if (sliderVida != null)
        {
            sliderVida.maxValue = max;
            sliderVida.value = actual;
        }
    }

    public void CambiarVolumen(float valor)
    {
        if (audioSource != null)
        {
            audioSource.volume = valor;
        }
    }

    public void MostrarPause()
    {
        Time.timeScale = 0f;

        if (soundPause != null)
            audioSource.PlayOneShot(soundPause);

        if (pausePanel != null) pausePanel.SetActive(true);

        PlayerShooter shooter = playerObject.GetComponent<PlayerShooter>();
        if (shooter != null) shooter.enabled = false;

        if (playerObject != null)
        {
            Camera camJugador = playerObject.GetComponentInChildren<Camera>();
            if (camJugador != null) camJugador.gameObject.SetActive(false);
        }

        if (cameraPause != null)
            cameraPause.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reanudar()
    {
        if (soundPause != null)
            audioSource.PlayOneShot(soundPause);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        PlayerShooter shooter = playerObject.GetComponent<PlayerShooter>();
        if (shooter != null) shooter.enabled = true;

        if (playerObject != null)
        {
            Camera camJugador = playerObject.GetComponentInChildren<Camera>(true);
            if (camJugador != null)
                camJugador.gameObject.SetActive(true);
        }

        if (cameraPause != null)
            cameraPause.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    IEnumerator ReiniciarConSonido()
    {
        if (soundRestart != null)
        {
            double tiempoInicio = AudioSettings.dspTime;
            audioSource.PlayOneShot(soundRestart);
            yield return new WaitUntil(() => AudioSettings.dspTime >= tiempoInicio + soundRestart.length);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator SalirConSonido()
    {
        if (soundExit != null)
        {
            double tiempoInicio = AudioSettings.dspTime;
            audioSource.PlayOneShot(soundExit);
            yield return new WaitUntil(() => AudioSettings.dspTime >= tiempoInicio + soundExit.length);
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void EjecutarReiniciar()
    {
        StartCoroutine(ReiniciarConSonido());
    }

    public void EjecutarSalir()
    {
        StartCoroutine(SalirConSonido());
    }

    public void MostrarVictoria()
    {
        if (victoryText != null)
            victoryText.SetActive(true);

        if (resumeButton != null)
            resumeButton.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (victoryMusic != null && audioSource != null)
        {
            audioSource.clip = victoryMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (cameraPause != null)
            cameraPause.gameObject.SetActive(true);

        if (playerObject != null)
        {
            PlayerShooter shooter = playerObject.GetComponent<PlayerShooter>();
            if (shooter != null) shooter.enabled = false;

            Camera camJugador = playerObject.GetComponentInChildren<Camera>();
            if (camJugador != null) camJugador.gameObject.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0.2f;
    }
}