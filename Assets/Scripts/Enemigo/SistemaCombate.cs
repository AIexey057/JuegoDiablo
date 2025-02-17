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
        agent.isStopped = false; // Asegurar que no esté detenido al iniciar la persecución
    }

    private void Update()
    {
        if (main.MainTarget1 != null)
        {
            if (agent.hasPath || agent.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                EnfocarObjetivo();
                agent.SetDestination(main.MainTarget1.position);

                if (!agent.pathPending && agent.remainingDistance <= distanciadCombate)
                {
                    anim.SetBool("Attacking", true);
                    agent.isStopped = true; // Detenerse antes de atacar
                }
                else
                {
                    anim.SetBool("Attacking", false);
                    agent.isStopped = false; // Reanudar movimiento si no está atacando
                }
            }
        }
        else
        {
            main.ActivarPatrulla();
            this.enabled = false; // Desactivar combate si el objetivo desaparece
        }
    }

    private void EnfocarObjetivo()
    {
        Vector3 direccionATarget = (main.MainTarget1.position - transform.position).normalized;
        direccionATarget.y = 0f;
        Quaternion rotacioATarget = Quaternion.LookRotation(direccionATarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacioATarget, Time.deltaTime * 5f);
    }

    private void Atacar()
    {
        if (main.MainTarget1 != null)
        {
            main.MainTarget1.GetComponent<Player>().HacerDanho(danhoAtaque);
        }
    }

    private void FinAtaque()
    {
        agent.isStopped = false; // Reanudar movimiento después del ataque
    }
}
