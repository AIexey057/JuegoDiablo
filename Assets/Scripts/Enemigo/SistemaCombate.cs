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
    [SerializeField] private Animator anim;
    [SerializeField] private float danhoAtaque;
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
            EnfocarObjetivo();
            agent.SetDestination(main.MainTarget1.position);
            if(!agent.pathPending && agent.remainingDistance <= distanciadCombate)
            {
                anim.SetBool("Attacking" ,true);
            }
        }
        else
        {
            main.ActivarPatrulla();
        }

    }
    private void EnfocarObjetivo()
    {
        Vector3 direccionATarget = (main.MainTarget1.position - transform.position).normalized;
        direccionATarget.y = 0f;
        Quaternion rotacioATarget = Quaternion.LookRotation(direccionATarget);
        transform.rotation = rotacioATarget;
    }
    private void Atacar()
    {
        main.MainTarget1.GetComponent<Player>().HacerDanho(danhoAtaque);
    }
    private void FinAtaque()
    {
        
    }
}
