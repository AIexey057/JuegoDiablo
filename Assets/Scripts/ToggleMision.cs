using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMision : MonoBehaviour
{
    [SerializeField] private TMP_Text textoMision;

    private Toggle toggle;

    public TMP_Text TextoMision { get => textoMision; }
    public Toggle Toggle { get => toggle;}
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
}
