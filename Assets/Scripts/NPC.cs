using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour,Iinteractuable
{
    Outline outline;
    [SerializeField] Texture2D CursorInteraccion;
    [SerializeField] Texture2D CursorPorDefecto;
    [SerializeField] private float tiempoDuracion;
    [SerializeField] private DialogoSO dialogo;
    [SerializeField] private Transform cameraPoint;
    
    // Start is called before the first frame update
    private void Awake()
    {
        outline = GetComponent<Outline>();
        
    }

    // Update is called once per frame
   public void Interactuar(Transform interactuador)
    {

        transform.DOLookAt(interactuador.position, tiempoDuracion, AxisConstraint.Y).OnComplete( ()=> SistemaDeDialogos.sistema.IniciarDialogo(dialogo, cameraPoint));//Ctr y click para ir al metodo
        
        
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
