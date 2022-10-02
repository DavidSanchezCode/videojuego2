using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientojugador : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [Header("movimiento")]
    private float movimientoHorizontal= 0f;

    [SerializeField]private float velocidadDeMovimiento;

    [SerializeField]private float suavizadoDeMovimiento;
    private Vector3 velocidad=Vector3.zero;
    private bool mirandoDerecha = true;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        movimientoHorizontal= Input.GetAxisRaw("Horizontal")*velocidadDeMovimiento;

        
    }
    private void FixedUpdate()
    {
        //* mover
        mover(movimientoHorizontal* Time.fixedDeltaTime);
    }

    private void mover(float mover){
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
        
    }

    private void girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale=escala;
    }


    
}
