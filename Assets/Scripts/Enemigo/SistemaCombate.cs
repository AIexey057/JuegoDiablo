using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    [SerializeField] private Enemigo main;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float velocidadCombate;
    [SerializeField] private float distanciadCombate;
    private void Awake()
    {
        main.Combate = this;
        
    }
    private void OnEnable()
    {
        agent.speed = velocidadCombate;
        agent.stoppingDistance = distanciadCombate;

    }
    
    private void Update()
    {
        if (main.MainTarget1 != null && agent.CalculatePath(main.MainTarget1.position, new NavMeshPath()))
        {
            agent.SetDestination(main.MainTarget1.position);
        }
        else
        {
            main.ActivarPatrulla();
        }
        
    }

}
