using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion;
    private Camera cam;
    private NPC npcActual;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        if (npcActual)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                npcActual.Interactuar(this.transform);
                npcActual = null;
                agent.isStopped = true;
                agent.stoppingDistance = 0;

            }
        }
    }
    private void Movimiento()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.TryGetComponent(out NPC npc))
                {
                    npcActual = npc;
                    agent.stoppingDistance = distanciaInteraccion;
                }
                agent.SetDestination(hit.point);
            }
        }
    }
}
