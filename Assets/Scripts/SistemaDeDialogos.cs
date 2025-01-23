using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SistemaDeDialogos : MonoBehaviour
{
   
    [SerializeField] private GameObject marcos;
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private Transform npcCamera;
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

    public void IniciarDialogo(DialogoSO dialogo, Transform cameraPoint)
    {
        Time.timeScale = 0f;

        npcCamera.SetPositionAndRotation(cameraPoint.position, cameraPoint.transform.rotation);
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
        Time.timeScale = 1f;
        
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0;
        escribiendo = false;
        if (dialogoActual.tieneMision)
        {
            eventManager.NuevaMision();
        }

        dialogoActual = null;
    } 
    // Start is called before the first frame update
   
}
