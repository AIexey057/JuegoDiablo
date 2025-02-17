using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, Iinteractuable
{
    Outline outline;
    [SerializeField] Texture2D CursorInteraccion;
    [SerializeField] Texture2D CursorPorDefecto;
    [SerializeField] private float tiempoDuracion;
    [SerializeField] private DialogoSO dialogo;
    [SerializeField] private Transform cameraPoint;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    // Método que se llama cuando el jugador interactúa con el NPC
    public void Interactuar(Transform interactuador)
    {
        // Verificar si ya hay un diálogo en curso
        if (!SistemaDeDialogos.sistema.enDialogo) // Asegurarse de que no hay un diálogo en progreso
        {
            transform.DOLookAt(interactuador.position, tiempoDuracion, AxisConstraint.Y).OnComplete(() => SistemaDeDialogos.sistema.IniciarDialogo(dialogo, cameraPoint));

        }
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
        Cursor.SetCursor(CursorInteraccion, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
        Cursor.SetCursor(CursorPorDefecto, Vector2.zero, CursorMode.Auto);
    }
}
