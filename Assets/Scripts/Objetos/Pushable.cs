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

    //distancia del raycast que detecta al jugador
    [SerializeField] private float raycastDistance = 3f;
    //distancia del raycast cuando se interactua (para que no se escape facilmente)
    [SerializeField] private float raycastCage = 5f;
    //layer que detecta el raycast como jugador (el resto las ignora)
    [SerializeField] private LayerMask playerLayer;

    [Header("Referencias")]
    //Imagen de boton de interaccion
    [SerializeField] private GameObject interactIcon;

    //referencias 
    private Rigidbody2D rb;
    private PlayerMovement player;

    //booleano que se activa al interactuar con el pushable
    private bool isBeingPushed;
    //creaccion de raycast
    private float trueRaycast;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trueRaycast = raycastDistance;
    }

    //Update solo se encarga de detectar al jugador cada frame 
    private void Update()
    {   
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        //si el jugador interactua con el objeto (pulsa E):
        if (isBeingPushed && player != null)
        {
            //Descongela X para poder moverla
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = new Vector2(player.GetCurrentVelX(), rb.linearVelocity.y);


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
        //deteccion de la posicion del jugador respecto a la caja (izq o drch)
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, trueRaycast, ~LayerMask.GetMask("Suelo"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, trueRaycast, ~LayerMask.GetMask("Suelo"));

        //asignamos el hit al rayo que haya detectado al jugador (esto puede ayudar en un futuro si queremos saber
        //en que lado el jugador cogio la caja, de momento es irrelevante)
        RaycastHit2D hit;
        if (hitRight.collider != null)
        {
            hit = hitRight;
        }
        else
        {
            hit = hitLeft;
        }
        
        //detectamos al jugador perse y obtenemos su componente de playerMovement
        if (hit.collider != null)
        {
            PlayerMovement deteccion = hit.collider.GetComponentInParent<PlayerMovement>();
            
            //cambiamos variable y activamos el icono interactuable
            if (deteccion != null)
            {
                player = deteccion;
                interactIcon.SetActive(true);

                // Pulsar E agarra, volver a pulsar suelta
                if (InputManager.Instance.InteractWasPressedThisFrame())
                {
                    if (!isBeingPushed)
                    {
                        isBeingPushed = true;
                        player.SetGrabbing(true);
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
            //se "olvida" la referencia del jugador
            player = null;
            //desactivamos el icono
            interactIcon.SetActive(false);
        }

     /*   if (hit.collider != null)
        {
            Debug.Log("golpeando a: " + hit.collider.name);
        }
        else
        {
            Debug.Log("no golpea nadaaa");
        }*/

    }

    //metodo responsable de detener la accion de empuje a traves de doble E o de alejarse
    private void PararPush()
    {
        isBeingPushed = false;

        if (player != null)
        {
            player.SetGrabbing(false);
        }
    }

    //vista del rayo en el editor, para facil modificación
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * trueRaycast);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * trueRaycast);
    }
}