using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private SistemaCombate combate;
    private SistemaPatrulla patrulla;
    private Transform MainTarget;
    public int vida = 40;

    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public Transform MainTarget1 { get => MainTarget; }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Player"))
        {
            
            ActivarCombate(other.transform);
        }
    }

    public void ActivarCombate(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("El objetivo es null. No se puede activar el combate.");
            return;
        }

        MainTarget = target;
        Patrulla.enabled = false;
        combate.enabled = true;
        Debug.Log("Combate activado con objetivo: " + MainTarget.name);
    }

    public void ActivarPatrulla()
    {
        combate.enabled = false;
        patrulla.enabled = true;
    }

    public void TomarDaño(int cantidad)
    {
        vida -= cantidad;
        Debug.Log($"Enemigo recibió {cantidad} de daño. Vida restante: {vida}");

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("¡Enemigo derrotado!");
        Destroy(gameObject);
    }
}
