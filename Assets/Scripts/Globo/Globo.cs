//---------------------------------------------------------
// Script para el globo (ascensor)
// Samuel McDermott de Juan
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

/// <summary>
/// Controla el comportamiento de un globo que actúa como ascensor.
/// Sube cuando el jugador entra en el trigger, espera arriba,
/// y baja tras un tiempo cuando el jugador se va.
/// </summary>
public class Globo : MonoBehaviour
{
    [SerializeField]
    private float alturaMax = 5f;
    [SerializeField]
    private float velocidadGlobo = 0.5f;

    private Rigidbody2D rb;

    private Vector3 inicial;
    private Vector3 final;

    private float tiempoEspera = 0f;
    private float progreso = 0f;

    private bool activado;
    private bool bajando;

    /// <summary>
    /// Inizializa las posiciones del Rigidbody2D, las posiciones del globo, y si esta activado y bajando.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        inicial = transform.position;
        final = inicial + new Vector3(0, alturaMax, 0);

        activado = false;
        bajando = false;
    }

    /// <summary>
    /// Usando Duck Typing, detecta la colisión con el jugador. Si el jugador entra en el trigger el globo sube.
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement jugador = collision.GetComponent<PlayerMovement>();
        if (jugador != null)
        {
            activado = true;
        }
    }

    /// <summary>
    /// Método para que se mueva el globo
    /// </summary>
    void FixedUpdate()
    {
        if (activado)
        {
            if (!bajando)
            {
                tiempoEspera += Time.deltaTime;
                if (tiempoEspera >= 1f)
                {
                    // Esto hace que el globo se mueva hacia arriba, evitando clippeos
                    progreso += Time.fixedDeltaTime * velocidadGlobo;
                    Vector3 nuevaPosicion = Vector3.Lerp(inicial, final, progreso);
                    rb.MovePosition(nuevaPosicion);
                    // Cuando llega arriba, resetea el progreso, el tiempo de espera y empieza a bajar
                    if (progreso >= 1f)
                    {
                        progreso = 0f;
                        tiempoEspera = 0f;
                        bajando = true;
                    }
                }
            }
            else
            {
                tiempoEspera += Time.fixedDeltaTime;
                // Pongo el tiempoEspera a 3 para que el jugador tenga tiempo para pensar en lo que hacer, es más intuitivo (se puede cambair)
                if (tiempoEspera >= 3f)
                {
                    // Lo mismo de antes pero para bajar, evitando clippeos
                    progreso += Time.fixedDeltaTime * velocidadGlobo;
                    Vector3 nuevaPosicion = Vector3.Lerp(final, inicial, progreso);
                    rb.MovePosition(nuevaPosicion);
                    // Cuando llega abajo, resetea todo
                    if (progreso >= 1f)
                    {
                        rb.MovePosition(inicial);
                        activado = false;
                        bajando = false;
                        progreso = 0f;
                        tiempoEspera = 0f;
                    }
                }
            }
        }
    } // class Globo 
}
// namespace