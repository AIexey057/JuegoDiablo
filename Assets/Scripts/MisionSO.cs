using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Mision")]
public class MisionSO : ScriptableObject
{
    public string ordenInicial;
    public string ordenFinal;

    public bool repetir;//Si la misión tiene varios pasos.
    public int repeticionesTotales;

    public int estadoActual;

    public int indiceMision;//Identificador único.
}
