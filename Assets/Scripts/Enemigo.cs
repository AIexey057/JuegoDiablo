using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform ruta;

    private NavMeshAgent agent;

    List<Vector3> listadoPuntos = new List<Vector3>();//tiene longitud variable pero un array es fijo no lo puedes cambiar
    private Vector3 destinoActual;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (Vector3 punto in ruta)
        {
            listadoPuntos.Add(punto);
        }
        CalcularDestino();
    }
    void Start()
    {
        StartCoroutine(PatrullarYEsperar());
    }

  private IEnumerator PatrullarYEsperar()
    {
        agent.SetDestination(destinoActual);
        yield return null;
    }
   private void CalcularDestino()
    {
        destinoActual = listadoPuntos[0];
    }
}
