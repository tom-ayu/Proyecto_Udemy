using UnityEngine;

public class CajaDestructible : MonoBehaviour, IDa�able
{
    public GameObject efectoMuerte;

    public void RecibirDa�o(int cantidad)
    {
        if (efectoMuerte != null)
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}