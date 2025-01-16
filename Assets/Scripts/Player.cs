using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion;
    [SerializeField] private float duracion;
    
    private Camera cam;
    
    private Transform ultimoClick;
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
        if(Time.timeScale == 1)
        {
            Movimiento();
        }
       
        if (ultimoClick && ultimoClick.TryGetComponent(out NPC npc))
        {
            agent.stoppingDistance = distanciaInteraccion;
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                //transform.DOLookAt(npc.transform.position,duracion, AxisConstraint.Y).OnComplete( ()=> LanzarInteraccion(npc));
                npc.Interactuar(this.transform);
               
            }
        }
        else if (ultimoClick)
        {
            agent.stoppingDistance = 0f;
        }
    }
    private void LanzarInteraccion(Iinteractuable iinteractuador)
    {
        iinteractuador.Interactuar(transform);
        ultimoClick = null;
    }
    private void Movimiento()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (Input.GetMouseButtonDown(0))
            {              
                agent.SetDestination(hit.point);
                ultimoClick = hit.transform;

            }
        }
    }

    internal void HacerDanho(float danhoAtaque)
    {
        Debug.Log("Me hacen pupa: " + danhoAtaque);
    }
}
