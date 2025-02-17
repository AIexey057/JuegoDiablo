using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMisiones : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private ToggleMision[] toggleMision;
    private bool esperandoParaNuevoDialogo = false;


    private void OnEnable()
    {
        eventManager.OnNuevaMision += ActivarToggleMision;
        eventManager.OnActualizarMision += ActualizarToggle;
        eventManager.OnTerminarMision += CerrarToggle;
    }

    private void CerrarToggle(MisionSO mision)
    {
        toggleMision[mision.indiceMision].Toggle.isOn = true;
        toggleMision[mision.indiceMision].TextoMision.text = mision.ordenFinal;
    }

    private void ActualizarToggle(MisionSO mision)
    {
       
    }

    private void ActivarToggleMision(MisionSO mision)
    {
        if (esperandoParaNuevoDialogo) return;
        toggleMision[mision.indiceMision].TextoMision.text = mision.ordenInicial;

        if (mision.repetir)
        {
            toggleMision[mision.indiceMision].TextoMision.text += "(" + mision.estadoActual + "/" + mision.repeticionesTotales + ")";
        }

        toggleMision[mision.indiceMision].gameObject.SetActive(true);
        esperandoParaNuevoDialogo = true;
        StartCoroutine(EsperarAntesDeNuevoDialogo());
    }
    private IEnumerator EsperarAntesDeNuevoDialogo()
    {
        yield return new WaitForSeconds(1f);
        esperandoParaNuevoDialogo = false;
    }
}
