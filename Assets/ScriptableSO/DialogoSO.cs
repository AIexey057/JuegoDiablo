using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogo")]

public class DialogoSO : ScriptableObject
{
    [TextArea]
    public string[] frases;
    
    public float tiempoLetras;

    public bool tieneMision;

    public MisionSO mision;
}
