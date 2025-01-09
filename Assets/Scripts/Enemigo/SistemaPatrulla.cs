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
        agent = GetComponent<NavMeshAgent>();
        foreach (Vector3 punto in ruta)
        {
            listadoPuntos.Add(punto);
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
        while(true) 
        {
            CalcularDestino();
            agent.SetDestination(destinoActual);
            yield return null;
        }
        
    }
    private void CalcularDestino()
    {
        destinoActual = listadoPuntos[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopAllCoroutines();
            main.ActivarCombate(other.transform);
            this.enabled = false;
        }
    }
}
