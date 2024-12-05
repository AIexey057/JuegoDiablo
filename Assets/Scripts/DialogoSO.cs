using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogo")]

public class DialogoSO : ScriptableObject
{
    [TextArea]
    public string[] frases;
    
    public int tiempoLetras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
