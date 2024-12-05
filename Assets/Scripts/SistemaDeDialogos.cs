using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaDeDialogos : MonoBehaviour
{
   
    [SerializeField] private GameObject marcos;
    [SerializeField] private TMP_Text textoDialogo;

    private bool escribiendo;
    private int indiceFraseActual;


    public static SistemaDeDialogos sistema;
    private void Awake()
    {
           if(sistema == null)
        {
            sistema = this; 
            DontDestroyOnLoad(gameObject);
        } 
           else
        {
            Destroy(this.gameObject);
        }
    }

    public void IniciarDialogo(DialogoSO dialogo)
    {
        marcos.SetActive(true);
    }

    private void EscribirFrase()
    {

    }

    private void SiguienterFrase()
    {

    }
    
    private void TerminarDialogo()
    {

    } 
    // Start is called before the first frame update
   
}
