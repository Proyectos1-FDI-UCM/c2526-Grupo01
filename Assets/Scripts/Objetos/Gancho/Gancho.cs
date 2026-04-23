//---------------------------------------------------------
// Script de la máquina de gancho, que se activa cuando el jugador colisiona con ella. Este lo engancha para elevarlo a una altura determinada.
// Samuel McDermott de Juan
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Máquina de gancho que coge al jugador para elevarlo a una determinada distancia. 
/// 3 Métodos: Start, OnTriggerEnter2D y FixedUpdate
/// </summary>
public class Gancho : MonoBehaviour
{
    [SerializeField]
    private float alturaMaxima = 5f;
    [SerializeField]
    private float velocidadGancho = 1f;

    private Rigidbody2D rb;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    private float progreso = 0f;
    private float tiempoEspera = 0f;

    private bool tieneJugador;
    private bool bajando;

    private Transform jugadorTransform;

    private double posXgancho = 0.2;
    private double posYgancho = -1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Posicion inicial, da igual donde se coloque el gancho
        posicionInicial = transform.position;

        // Calcula la posicion final con respecto a la posicion inicial
        posicionFinal = posicionInicial + new Vector3(0, alturaMaxima, 0);

        tieneJugador = false;
        bajando = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta jugador
        PlayerMovement jugador = collision.GetComponent<PlayerMovement>();
        if (jugador != null && !tieneJugador && !bajando)
        {
            // Detecta la colisión por debajo para que el jugador no se enganche desde cualquier otro lado del gancho
            if (collision.transform.position.y < transform.position.y)
            {
                jugadorTransform = jugador.transform;
                jugadorTransform.parent = transform;
                tieneJugador = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Para que cuando el juego este en pausa
        if (Physics2D.simulationMode == SimulationMode2D.Script)
        {
        }
        else
        {
            if (tieneJugador)
            {
                // Lo que produce que el gancho se mueva
                progreso += Time.fixedDeltaTime * velocidadGancho;
                Vector3 nuevaPos = Vector3.Lerp(posicionInicial, posicionFinal, progreso);
                rb.MovePosition(nuevaPos);

                if (jugadorTransform != null)
                {
                    Rigidbody2D rbJugador = jugadorTransform.GetComponent<Rigidbody2D>();

                    // Si se encuentra el Rigidbody2D del jugador, este se engancha al gancho y se mueve con él
                    if (rbJugador != null)
                    {
                        // Ajusta la posición del jugador para que este este en el centro y justo debajo del gancho,
                        // da la casualidad que con 0.2 y -1 queda perfecto (se puede ajustar)
                        Vector3 posicionDestinoJugador = nuevaPos + new Vector3((float)posXgancho, (float)posYgancho, 0);

                        // Mueve al jugador con el gancho al mismo tiempo, para evitar el clippeo
                        rbJugador.MovePosition(posicionDestinoJugador);

                        // Detiene la acumulación de velocidad (para que al desenganchar al jugador este no caiga inmediatamente)
                        // y evita el clippeo
                        rbJugador.linearVelocity = Vector3.zero;
                    }
                }

                // Pongo 1,4 para que el gancho tarde un poco más en soltar al jugador, para que así el jugador tenga más tiempo
                // para pensar en lo que hacer y sepa a donde debe ir, si no es lo deseado se puede cambiar perfectamente
                if (progreso >= 1.4f)
                {
                    // Cuando el jugador se desengancha, vaciamos el jugadorTransform para que el gancho este libre,
                    // y el jugador pueda moverse por su cuenta
                    if (jugadorTransform != null)
                    {
                        jugadorTransform.parent = null;
                        jugadorTransform = null;
                    }
                    tieneJugador = false;
                    bajando = true;
                    progreso = 0f;
                }
            }

            // Se usa un else if porque sino el gancho estaría bajando todo el rato
            else if (bajando)
            {
                tiempoEspera += Time.fixedDeltaTime;
                if (tiempoEspera >= 2.5f)
                {
                    // Movimiento de bajada
                    progreso += Time.fixedDeltaTime * velocidadGancho;
                    rb.MovePosition(Vector3.Lerp(posicionFinal, posicionInicial, progreso));

                    if (progreso >= 1f)
                    {
                        rb.MovePosition(posicionInicial);
                        bajando = false;
                        tiempoEspera = 0f;
                        progreso = 0f;
                    }
                }
            }
        }

    }
} // class Gancho
 // namespace