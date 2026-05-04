//---------------------------------------------------------
// Script de pushable para elementos interactuables y empujables
// Hector Prous arroyo, creditos a tutoriales de youtube respecto a la aplicacion de los gizmos y otras preguntas generales
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;

// Añadir aquí el resto de directivas using

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>

public class Pushable : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject interactIcon;

    [Header("Velocidad")]
    [SerializeField] private float velBox = 100f; //parametro que determina en % la velocidad que tendra el jugador al agarrar la caja 

    private Rigidbody2D rb;
    private PlayerMovement player;
    private bool isBeingPushed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //logica del boton interact, si InteractWasPressedThisFrame activa el bool y si se vuelve a clickar lo desactiva
        if (player != null)
        {
            if (InputManager.Instance.InteractWasPressedThisFrame())
            {
                if (!isBeingPushed)
                {
                    isBeingPushed = true;
                }
                else
                {
                    isBeingPushed = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //para que funcione en las pausas (eso creo, no lo he probado mucho y se lo mire a samu)
        if (Physics2D.simulationMode == SimulationMode2D.Script)
        {
        }
        else
        {   //logica de movimiento de la caja, si isbeingpushed es true le asigna los mismos valores de velocidad en x que tiene el player
            if (isBeingPushed && player != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.linearVelocity = new Vector2(player.GetCurrentVelX() * (velBox / 100f), rb.linearVelocity.y);
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //si lo que entra al trigger posee playermovement lo asigna a una variable, y si esa variable no es nula se la asigna a la variable de clase playermovement y activa el icono de grab
        PlayerMovement detectedPlayer = other.GetComponent<PlayerMovement>();

        if (detectedPlayer != null)
        {
            player = detectedPlayer;
            interactIcon.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //la logica es similar a la de ontriggerenter, pero a causa de un bug sin resolver que envolvia un falso trigger, al final se detecta si el jugador esta 
        //empujando o no para luego sacar la distancia entre la caja y el player y solo si es algo alta permite abandonar la accion de empujar
        //(antes al intentar agarrar y tirar hacia atras la caja desde muyyy cerca te sacaba inmediatamente por lo que creo que es un problema que detectaba el colider de la caja como trigger)
        PlayerMovement detectedPlayer = other.GetComponent<PlayerMovement>();

        if (detectedPlayer != null)
        {
            float bugFalsoTrigger = Vector2.Distance(transform.position, detectedPlayer.transform.position);

            if (!isBeingPushed || bugFalsoTrigger > 4f)
            {
                isBeingPushed = false;
                player = null;
                interactIcon.SetActive(false);
            }
        }
    }
}