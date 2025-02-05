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
    private int indiceFraseActual;
    public bool enDialogo;  // Variable que indica si estamos en un diálogo

   private DialogoSO dialogoActual;

    public static SistemaDeDialogos sistema;

    private void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
            DOTween.SetTweensCapacity(500, 50);  // Ajuste de la capacidad de los tweens
            DontDestroyOnLoad(gameObject.transform.root.gameObject);  // Asegura que no se destruye entre escenas
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Método para iniciar el diálogo
    public void IniciarDialogo(DialogoSO dialogo, Transform cameraPoint)
    {
        if (enDialogo) return;  // Si ya hay un diálogo en curso, no se inicia otro

        enDialogo = true;  // Marcamos que estamos en un diálogo
        if (escribiendo)
        {
            StopAllCoroutines();
        }

        Time.timeScale = 0f;

        npcCamera.SetPositionAndRotation(cameraPoint.position, cameraPoint.transform.rotation);
        dialogoActual = dialogo;
        marcos.SetActive(true);
        indiceFraseActual = 0;  // Comienza desde la primera frase
        textoDialogo.text = "";  // Limpiamos el texto antes de empezar
        StartCoroutine(EscribirFrase());
    }

    private IEnumerator EscribirFrase()
    {
        escribiendo = true;

        // Limpiamos el texto antes de empezar a escribir la nueva frase
        textoDialogo.text = "";

        char[] fraseEnLetras = dialogoActual.frases[indiceFraseActual].ToCharArray();

        foreach (char letra in fraseEnLetras)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(dialogoActual.tiempoLetras); // Controla la velocidad
        }

        escribiendo = false; // Marca que se terminó de escribir la frase
    }

    // Método para continuar al siguiente diálogo o terminarlo
    public void SiguienterFrase()
    {
        if (escribiendo)
        {
            CompletarFrase(); // Completa la frase actual inmediatamente si se está escribiendo
        }
        else
        {
            indiceFraseActual++;  // Avanza a la siguiente frase

            if (indiceFraseActual < dialogoActual.frases.Length)
            {
                StartCoroutine(EscribirFrase());
            }
            else
            {
                TerminarDialogo();  // Si no hay más frases, termina el diálogo
            }
        }
    }

    // Completa la frase si está siendo escrita
    private void CompletarFrase()
    {
        StopAllCoroutines(); // Detenemos cualquier corutina en ejecución
        textoDialogo.text = dialogoActual.frases[indiceFraseActual];  // Muestra la frase completa
        escribiendo = false;
    }

    // Método para terminar el diálogo
    private void TerminarDialogo()
    {
        Time.timeScale = 1f;
        marcos.SetActive(false);
        StopAllCoroutines();
        indiceFraseActual = 0;  // Reiniciamos el índice de frases
        escribiendo = false;
        enDialogo = false;  // Marcamos que el diálogo ha terminado

        if (dialogoActual.tieneMision)
        {
            eventManager.NuevaMision(dialogoActual.mision);
        }

        dialogoActual = null;  // Limpiamos el diálogo actual
    }
}
