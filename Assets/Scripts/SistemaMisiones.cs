using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private GameObject toggleMision;

    private void OnEnable()
    {
        eventManager.OnNuevaMision += EventManager_OnNuevaMision;
    }

    private void EventManager_OnNuevaMision()
    {
       toggleMision.SetActive(true);    
    }
}
