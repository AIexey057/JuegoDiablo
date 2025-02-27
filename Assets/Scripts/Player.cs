using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float distanciaInteraccion = 2f;
    [SerializeField] private float distanciaAtaque = 2f;
    [SerializeField] private float duracion;
    public GameObject prefabBola; 
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1f;
    private bool puedeDisparar = true;

    private Camera cam;
    
    private Transform ultimoClick;
    private NavMeshAgent agent;
    private Animator anim;
    private PlayerAnimation playerAnimation;

    public PlayerAnimation PlayerAnimation { get => playerAnimation; set => playerAnimation = value; }

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
       
        if (ultimoClick && ultimoClick.TryGetComponent(out Iinteractuable interactuable))
        {
            agent.stoppingDistance = distanciaInteraccion;
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                interactuable.Interactuar(transform);
               
            }
        }
        else if (ultimoClick && ultimoClick.TryGetComponent(out Enemigo enemigo))
        {

        }
        else if (ultimoClick)
        {
            agent.stoppingDistance = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && puedeDisparar)
        {
            Disparar();
        }
    }
    void Disparar()
    {
        if (prefabBola != null && puntoDisparo != null)
        {
            GameObject bola = Instantiate(prefabBola, puntoDisparo.position, Quaternion.identity);

            Vector3 direccionDisparo = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
            bola.GetComponent<Proyectil>().ConfigurarDireccion(direccionDisparo);

            puedeDisparar = false; 
            Invoke("ReiniciarDisparo", tiempoEntreDisparos); 
        }
    }
    void ReiniciarDisparo()
    {
        puedeDisparar = true; 
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
