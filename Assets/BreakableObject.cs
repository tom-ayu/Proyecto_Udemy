using UnityEngine;

public class CajaDestructible : MonoBehaviour, IDa�able
{
    public GameObject efectoMuerte;
    public AudioClip explosionSound;

    public void RecibirDa�o(int cantidad)
    {
        // Sonido
        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // FX
        if (efectoMuerte != null)
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}