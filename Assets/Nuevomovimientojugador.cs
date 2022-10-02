using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuevomovimientojugador : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [Header("movimiento")]
    private float movimientoHorizontal= 0f;

    [SerializeField]private float velocidadDeMovimiento;

    [SerializeField]private float suavizadoDeMovimiento;
    private Vector3 velocidad=Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField]private float fuerzaSalto;
    [SerializeField]private LayerMask queEsSuelo;
    [SerializeField]private Transform controladorSuelo;
    [SerializeField]private Vector3 dimensionesCaja;
    [SerializeField]private bool enSuelo;
    private bool salto=false;

    [Header("animacion")]
    private Animator animator;
    

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    private void Update(){
        movimientoHorizontal= Input.GetAxisRaw("Horizontal")*velocidadDeMovimiento;
        
        animator.SetFloat("Horizontal",Mathf.Abs(movimientoHorizontal));

        if(Input.GetButtonDown("Jump")){
            salto=true;
        }
        
    }
    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position,dimensionesCaja,0f,queEsSuelo);
        animator.SetBool("enSuelo",enSuelo);
        //* mover
        mover(movimientoHorizontal* Time.fixedDeltaTime, salto);
        salto=false;
    }

    private void mover(float mover,bool saltar){
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity =Vector3.SmoothDamp(rb2D.velocity,velocidadObjetivo,ref velocidad,suavizadoDeMovimiento);

        if(mover >0 && !mirandoDerecha)
        {
            girar();
        }

        else if (mover < 0 && mirandoDerecha)
        {
            //girar
            girar();
        } 

        if(enSuelo && saltar){
            enSuelo=false;
            rb2D.AddForce(new Vector2(0f,fuerzaSalto));
        }
        
    }

    private void girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale=escala;
    }

    


    
}


