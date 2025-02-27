using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;  
    public int daño = 10;          
    public float tiempoDeVida = 5f; 

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);  
    }

    private void Update()
    {
        transform.position += transform.forward * velocidad * Time.deltaTime;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo")) 
        {
            other.GetComponent<Enemigo>()?.TomarDaño(daño); 
            Destroy(gameObject);
        }
    }
}
