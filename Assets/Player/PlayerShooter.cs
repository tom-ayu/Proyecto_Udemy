using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour
{
    public float distanciaDisparo = 100f;
    public LayerMask capaObjetivo;
    public GameObject efectoImpacto;
    public GameObject prefabLaser;

    public float duracionLaser = 0.05f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Vector3 puntoFinal = ray.origin + ray.direction * distanciaDisparo;

        if (Physics.Raycast(ray, out hit, distanciaDisparo, capaObjetivo))
        {
            puntoFinal = hit.point;

            if (efectoImpacto != null)
                Instantiate(efectoImpacto, hit.point, Quaternion.LookRotation(hit.normal));

            IDañable dañable = hit.collider.GetComponent<IDañable>();
            if (dañable != null)
                dañable.RecibirDaño(1);
        }

        if (prefabLaser != null)
            StartCoroutine(MostrarLaser(ray.origin, puntoFinal));
    }

    IEnumerator MostrarLaser(Vector3 inicio, Vector3 fin)
    {
        GameObject laser = Instantiate(prefabLaser);
        LineRenderer lr = laser.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, inicio);
            lr.SetPosition(1, fin);
        }

        yield return new WaitForSeconds(duracionLaser);
        Destroy(laser);
    }
}