using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SistemaCombate : MonoBehaviour
{
    [SerializeField] private Enemigo main;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float velocidadCombate;
    private void Awake()
    {
        main.Combate = this;
        
    }
    private void OnEnable()
    {
        agent.speed = velocidadCombate;

    }
    void Start()
    {
       
    }
    private void Update()
    {
        
        agent.SetDestination(main.MainTarget1.position);
    }

}
