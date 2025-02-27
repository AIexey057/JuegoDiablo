using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private SistemaCombate combate;
    private SistemaPatrulla patrulla;
    private Transform MainTarget;
    public int vida = 20;

    

    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public Transform MainTarget1 { get => MainTarget; }

    public void ActivarCombate(Transform target)
    {
        MainTarget = target;
        Patrulla.enabled = false;
        combate.enabled = true;
    }

    public void ActivarPatrulla()
    {
        combate.enabled = false;
        patrulla.enabled = true;
    }
    public void TomarDaño(int cantidad)
    {
        vida -= cantidad;
        Debug.Log($"Enemigo recibió {cantidad} de daño. Vida restante: {vida}");

        if (vida <= 0)
        {
            Morir();
        }
    }
    void Morir()
    {
        Debug.Log("¡Enemigo derrotado!");
        Destroy(gameObject);
    }
}
