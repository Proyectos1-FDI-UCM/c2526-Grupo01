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
    [Header("Detección")]
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private float raycastCage = 5f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Referencias")]
    [SerializeField] private GameObject interactIcon;

    private Rigidbody2D rb;
    private PlayerMovement playerInRange;
    private bool isBeingPushed;
    public float trueRaycast;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trueRaycast = raycastDistance;
    }

    private void Update()
    {   
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (isBeingPushed && playerInRange != null)
        {
            //Descongela X para poder moverla
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = new Vector2(playerInRange.GetCurrentVelX(), rb.linearVelocity.y);


            //hacemos raycast mas grande para que el jugador no salga
            trueRaycast = raycastCage;
        }
        else
        {
            //Congela X para que no se pueda mover
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            //devolvemos raycast al tamaño natural 
            trueRaycast = raycastDistance;
        }
    }

    private void DetectPlayer()
    {
       

        //creacion de raycast
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, trueRaycast, playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, trueRaycast, playerLayer);

        //asignamos el hit al rayo que haya detectado al jugador
        RaycastHit2D hit;
        if (hitRight.collider != null)
        {
            hit = hitRight;
        }
        else
        {
            hit = hitLeft;
        }
        
        //detectamos al jugador perse
        if (hit.collider != null)
        {
            PlayerMovement deteccion = hit.collider.GetComponentInParent<PlayerMovement>();
            
            //cambiamos variable y activamos el icono interactuable
            if (deteccion != null)
            {
                playerInRange = deteccion;
                interactIcon.SetActive(true);

                // Pulsar E agarra, volver a pulsar suelta
                if (InputManager.Instance.InteractWasPressedThisFrame())
                {
                    if (!isBeingPushed)
                    {
                        isBeingPushed = true;
                        playerInRange.SetGrabbing(true);
                    }
                    else
                    {
                        PararPush();
                    }
                }
            }
        }
        else
        {
            // El jugador se alejo soltamos automáticamente el pushable
            if (isBeingPushed)
            {
                PararPush();
            }

            playerInRange = null;
            interactIcon.SetActive(false);
        }
    }

    //metodo responsable de detener la accion de empuje a traves de doble E o de alejarse
    private void PararPush()
    {
        isBeingPushed = false;

        if (playerInRange != null)
        {
            playerInRange.SetGrabbing(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //vista del rayo en el editor, para facil modificación
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * trueRaycast);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * trueRaycast);
    }
}