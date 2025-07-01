using UnityEngine;
using UnityEngine.AI;

public class EnemyShooterAI : MonoBehaviour, IDañable
{
    [Header("Objetivo y Movimiento")]
    public Transform objetivo;
    public float distanciaDisparo = 10f;
    public float tiempoEntreDisparos = 1.5f;
    public LayerMask capaObjetivo;

    [Header("Disparo")]
    public GameObject laserPrefab; // Prefab con LineRenderer activo
    public float duracionLaser = 0.1f;

    [Header("Vida y Efectos")]
    public int vida = 3;
    public GameObject efectoImpacto;

    private NavMeshAgent agente;
    private float tiempoUltimoDisparo;

    void Start()
    {
        if (objetivo == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null) objetivo = jugador.transform;
        }

        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        // Movimiento y rotación
        if (distancia > 1.5f)
        {
            agente.isStopped = false;
            agente.SetDestination(objetivo.position);
        }
        else
        {
            agente.isStopped = true;
        }

        Vector3 direccion = objetivo.position - transform.position;
        direccion.y = 0f;
        if (direccion.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(direccion);

        // Disparo
        if (distancia <= distanciaDisparo && Time.time >= tiempoUltimoDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        Vector3 origen = transform.position + Vector3.up * 1.2f;
        Vector3 direccion = (objetivo.position - origen).normalized;

        Ray ray = new Ray(origen, direccion);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaDisparo, capaObjetivo))
        {
            // Efecto impacto
            if (efectoImpacto != null)
            {
                GameObject impacto = Instantiate(efectoImpacto, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impacto, 1f); // Se destruye automáticamente después de 1 segundo
            }

            // Disparo visual (prefab con LineRenderer)
            if (laserPrefab != null)
            {
                GameObject laser = Instantiate(laserPrefab);
                LineRenderer lr = laser.GetComponent<LineRenderer>();
                if (lr != null)
                {
                    lr.SetPosition(0, origen);
                    lr.SetPosition(1, hit.point);
                }
                Destroy(laser, duracionLaser);
            }

            // Aplicar daño
            IDañable dañable = hit.collider.GetComponent<IDañable>();
            if (dañable != null)
            {
                dañable.RecibirDaño(1);
            }
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            Destroy(other.gameObject); // comportamiento personalizado
    }
}