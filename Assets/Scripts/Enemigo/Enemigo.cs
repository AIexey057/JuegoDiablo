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

    public SistemaPatrulla Patrulla { get => patrulla; set => patrulla = value; }
    public SistemaCombate Combate { get => combate; set => combate = value; }
    public Transform MainTarget1 { get => MainTarget; }

    public void ActivarCombate(Transform target)
    {
        MainTarget = target;
       combate.enabled = true;
    }
}
