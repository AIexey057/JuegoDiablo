using UnityEngine.AI;
using UnityEngine;

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
        agent.isStopped = false; 
    }

    private void Update()
    {
        if (main.MainTarget1 != null)
        {
            // Confirmamos que el objetivo no es null
            Debug.Log("Objetivo del enemigo: " + main.MainTarget1.name);

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
            Debug.Log("El enemigo ha perdido el objetivo o el objetivo es null.");
            main.ActivarPatrulla();
            this.enabled = false; // Desactivar combate si el objetivo desaparece
        }
    }


    private void EnfocarObjetivo()
    {
        if (main.MainTarget1 == null) return;

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
        agent.isStopped = false; 
    }
}
