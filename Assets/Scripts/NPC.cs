using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    Outline outline;
    [SerializeField] Texture2D CursorInteraccion;
    [SerializeField] Texture2D CursorPorDefecto;
    [SerializeField] private float tiempoDuracion;
    
    // Start is called before the first frame update
    private void Awake()
    {
        outline = GetComponent<Outline>();
        
    }

    // Update is called once per frame
   public void Interactuar(Transform interactuador)
    {
        Debug.Log("Hola");
        transform.DOLookAt(interactuador.position, tiempoDuracion, AxisConstraint.Y);
        
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
