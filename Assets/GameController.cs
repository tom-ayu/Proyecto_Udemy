using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Enemigos")]
    public GameObject enemyGroup2;
    public float tiempoEspera = 7f;

    [Header("UI")]
    public UIController uiController;

    private bool victoriaActivada = false;

    void Start()
    {
        if (enemyGroup2 != null)
        {
            enemyGroup2.SetActive(false);
            Invoke(nameof(ActivarObjeto), tiempoEspera);
        }
    }

    void Update()
    {
        if (!victoriaActivada && enemyGroup2 != null && enemyGroup2.activeSelf &&
            enemyGroup2.transform.childCount == 0)
        {
            victoriaActivada = true;
            if (uiController != null)
                uiController.MostrarVictoria();
        }
    }

    void ActivarObjeto()
    {
        if (enemyGroup2 != null)
            enemyGroup2.SetActive(true);
    }
}