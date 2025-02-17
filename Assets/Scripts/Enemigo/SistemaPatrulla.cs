using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaPatrulla : MonoBehaviour
{
    [SerializeField] private Enemigo main;

    [SerializeField] private Transform ruta;

    [SerializeField] private NavMeshAgent agent;
   
    [SerializeField] private float velocidadPatrulla;

    List<Vector3> listadoPuntos = new List<Vector3>();//tiene longitud variable pero un array es fijo no lo puedes cambiar
    private int indiceActual = -1;  
    private Vector3 destinoActual;
    private void Awake()
    {
        main.Patrulla = this;
        foreach (Transform punto in ruta)
        {
            listadoPuntos.Add(punto.position);
        }
        CalcularDestino();     
    }
    void Start()
    {
        
        
    }
    private void OnEnable()
    {
        StartCoroutine(PatrullarYEsperar());
    }

  
        private IEnumerator PatrullarYEsperar()
        {
            while (true)
            {
                agent.SetDestination(destinoActual);
                yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
                yield return new WaitForSeconds(1f);
                CalcularDestino(); 
            }
        }


    
    private void CalcularDestino()
    {
        indiceActual++;
        if(indiceActual >= listadoPuntos.Count)
        {
            indiceActual = 0;
        }
        destinoActual = listadoPuntos[indiceActual];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado. Iniciando combate.");
            StopAllCoroutines();
            main.ActivarCombate(other.transform);
            this.enabled = false;
        }
    }
}
