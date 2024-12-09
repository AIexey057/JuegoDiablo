using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SistemaDeDialogos : MonoBehaviour
{
   
    [SerializeField] private GameObject marcos;
    [SerializeField] private TMP_Text textoDialogo;

    private bool escribiendo;
    private int indiceFraseActual;

    private DialogoSO dialogoActual;

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
        Time.timeScale = 0f;
       
        dialogoActual = dialogo;
        marcos.SetActive(true);
        StartCoroutine(EscribirFrase());
    }

    private IEnumerator EscribirFrase()
    {
        escribiendo = true;

        textoDialogo.text = "";
        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();
        foreach(char letra in fraseEnLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoLetras);
        }
        escribiendo = false;
    }

   public void SiguienterFrase()
    {
        if (escribiendo)
        {
            CompletarFrase();
        }
        else
        {
            indiceFraseActual++;
            if(indiceFraseActual < dialogoActual.frases.Length)
            {
                StartCoroutine(EscribirFrase());
            }
            else
            {
                TerminarDialogo();
            }
           
        }
    }
    private void CompletarFrase()
    {
        StopAllCoroutines();
        textoDialogo.text = dialogoActual.frases[indiceFraseActual];
        escribiendo = false;
    }
    
    private void TerminarDialogo()
    {
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0;
        escribiendo = false;
        dialogoActual = null;
        Time.timeScale = 1f;
    } 
    // Start is called before the first frame update
   
}
