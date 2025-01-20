using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Player main;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //main = playerAnimation = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EjecutarAtaque()
    {
        anim.SetBool("attacking", true);
    }
    public void DetenerAtaque()
    {
        anim.SetBool("attacking", false);
    }
}
