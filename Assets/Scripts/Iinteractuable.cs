using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Una interfaz es un listado de métodos que cumple todo aquello que,
// en este caso sea interactuable
public interface Iinteractuable 
{
    public void Interactuar(Transform interactuador); 
}
