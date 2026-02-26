//---------------------------------------------------------
// Script para controlar el movimiento del jugador
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f; //velocidad configurable en el editor

    [SerializeField]
    private float jumpSpeed = 7f; //velocidad del salto

    [SerializeField]
    private float gravity = 20f; //como no estamos usando rigidbody ni las fisicas de unity nos creamos la gravedad

    [SerializeField]
    private float groundY = -2.4f; //la altura del suelo


    private float verticalSpeed = 0f;
    private bool grounded = true; //variable para saber si esta el jugador en el suelo o no

    void Start()
    {
        groundY = transform.position.y; //para que pille bien la altura del suelo, tiene q estar bien colocado el player en la escena claro
    }

    void Update()
    {
        //MOVIMIENTO HORIZONTAL

        //hay q confirmar que existe el InputManager
        if (!InputManager.HasInstance()) 
        {
            Debug.LogWarning("no hay InputManager en escena");
            return;
        }

        //leeemos input del eje x
        float moveX = InputManager.Instance.MovementVector.x;

        //nos movemos el función del input
        transform.position += new Vector3(moveX * speed * Time.deltaTime, 0f, 0f);


        //SALTO

        if(grounded && InputManager.Instance.JumpWasPressedThisFrame()) //si el jugador ha pulsado el botón d salto y esta en el suelo
        {
            verticalSpeed = jumpSpeed;
            grounded = false; //ya no está en el suelo
        
        }

        verticalSpeed = verticalSpeed - (gravity * Time.deltaTime);//afecta la gravedad

        Vector3 pos = transform.position;
        pos.y += verticalSpeed * Time.deltaTime;

        //“Suelo” como si fuera
        if (pos.y <= groundY)
        {
            pos.y = groundY;
            verticalSpeed = 0f;
            grounded = true;
        }

        transform.position = pos;
    }


}

