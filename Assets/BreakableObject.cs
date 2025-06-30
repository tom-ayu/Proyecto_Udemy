using UnityEngine;

public class CajaDestructible : MonoBehaviour, IDañable
{
    public GameObject efectoMuerte;

    public void RecibirDaño(int cantidad)
    {
        if (efectoMuerte != null)
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}