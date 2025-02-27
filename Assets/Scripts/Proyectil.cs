using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 3f;
    public int daño = 10;
    public float tiempoDeVida = 5f;
    private Vector3 direccion;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    private void Update()
    {
        transform.position += direccion * velocidad * Time.deltaTime;  
    }

    public void ConfigurarDireccion(Vector3 nuevaDireccion)
    {
        direccion = nuevaDireccion; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            other.GetComponent<Enemigo>()?.TomarDaño(daño);
            Destroy(gameObject);
        }
        if (other.CompareTag("Pared"))
        {
            Destroy(gameObject);
        }
    }
}

