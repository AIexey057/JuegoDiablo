using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class SistemaDeDialogos : MonoBehaviour
{
    [SerializeField] private GameObject marcos;
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private TMP_Text textoDialogo;
    [SerializeField] private Transform npcCamera;
    private bool escribiendo;
    private int indiceFraseActual = 1;
    public bool enDialogo;  

   private DialogoSO dialogoActual;

    public static SistemaDeDialogos sistema;

    private void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
            DOTween.SetTweensCapacity(500, 50);  
            DontDestroyOnLoad(gameObject.transform.root.gameObject);  
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
    public void IniciarDialogo(DialogoSO dialogo, Transform cameraPoint)
    {
        Debug.Log("Intentando iniciar diálogo. Estado de enDialogo: " + enDialogo);
        if (enDialogo) return;  

        enDialogo = true; 
        if (escribiendo)
        {
            StopAllCoroutines();
        }

        Time.timeScale = 0f;

        npcCamera.SetPositionAndRotation(cameraPoint.position, cameraPoint.transform.rotation);
        dialogoActual = dialogo;
        marcos.SetActive(true);
        indiceFraseActual = 0;  
        textoDialogo.text = "";  
        StartCoroutine(EscribirFrase());
    }

    private IEnumerator EscribirFrase()
    {
        escribiendo = true;

        
        textoDialogo.text = "";

        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();

        foreach (char letra in fraseEnLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoLetras); 
        }

        escribiendo = false; 
    }


    public void SiguienterFrase()
    {
        Debug.Log($"Botón presionado, avanzando a la siguiente frase. Índice actual antes de incrementar: {indiceFraseActual}");

        if (escribiendo)
        {
            CompletarFrase();
        }
        else
        {
            indiceFraseActual++;
            Debug.Log($"Nuevo índice después de incrementar: {indiceFraseActual} / Total frases: {dialogoActual.frases.Length}");

            if (indiceFraseActual < dialogoActual.frases.Length)
            {
                StartCoroutine(EscribirFrase());
            }
            else
            {
                Debug.Log("No hay más frases, terminando diálogo.");
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
        Debug.Log("Ejecutando TerminarDialogo()");

        Time.timeScale = 1f;
        marcos.SetActive(false);
        StopAllCoroutines();
        escribiendo = false;
        enDialogo = false;

        Debug.Log("Diálogo terminado correctamente, enDialogo ahora es " + enDialogo);

        if (dialogoActual.tieneMision)
        {
            eventManager.NuevaMision(dialogoActual.mision);
        }

        dialogoActual = null;
        indiceFraseActual = 0;  
    }


}
