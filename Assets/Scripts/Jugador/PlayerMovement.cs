//---------------------------------------------------------
// Script para controlar el movimiento del jugador
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //velocidad configurable en el editor
    [SerializeField]
    private float speed = 5f;

    //Velocidad actual del player.
    private float currentSpeed = 0f;

    //Grado de aceleración inicial.
    [SerializeField]
    private float walkAcceler = 15f;

    //Grado de frenado final.
    [SerializeField]
    private float walkDeceler = 15f;

    //velocidad del salto
    [SerializeField]
    private float jumpSpeed = 7f; 

    [SerializeField]
    private float gravity = 20f; 



    private float verticalSpeed = 0f;

    //referencia al trigger del los pies del jugador
    [SerializeField]
    private GroundCheck groundCheck;
    //groundCheck.grouended variable para saber si esta el jugador en el suelo o no




    void Update()
    {

        //Debug.Log("El jugador esta en el suelo?: " + groundCheck.grounded);

        //MOVIMIENTO HORIZONTAL

        //hay q confirmar que existe el InputManager
        if (!InputManager.HasInstance())
        {
            Debug.LogWarning("no hay InputManager en escena");
            return;
        }

        //leeemos input del eje x
        float moveX = InputManager.Instance.MovementVector.x;


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0)

        {
            transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }



        if (moveX != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveX * speed, walkAcceler * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, walkDeceler * Time.deltaTime);
        }



        //nos movemos el función del input
        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0f, 0f);



        //SALTO

        //si el jugador ha pulsado el botón d salto y esta en el suelo
        if (InputManager.Instance.JumpWasPressedThisFrame() && groundCheck.grounded == true)
        {
            verticalSpeed = jumpSpeed;
        }

        //afecta la gravedad
        verticalSpeed = verticalSpeed - (gravity * Time.deltaTime);

        //si esta tocando suelo y sigue cayendo, cortamos la caida
         if (groundCheck.grounded == true && verticalSpeed < 0f)
         {
             verticalSpeed = 0f;
         } 

        //aplicamos movimiento vertical manual
        Vector3 pos = transform.position;
        pos.y += verticalSpeed * Time.deltaTime;

        transform.position = pos;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("estoy tocando: " + collision.gameObject.name);
    }

    

}

