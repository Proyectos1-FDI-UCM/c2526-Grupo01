//---------------------------------------------------------
// Script para el globo (ascensor)
// Samuel McDermott de Juan
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Globo : MonoBehaviour
{
    [SerializeField]
    private float alturaMaxima;
    [SerializeField]
    private float alturaInicial;

    [SerializeField]
    private float velocidadGlobo = 5f;

    [SerializeField]
    private float tiempoEsperaArriba = 2f;
    [SerializeField]
    private float tiempoEsperaAbajo = 2f;

    private bool jugadorEncima = false;

    private float tiempoEspera = 0f;

    // 0 = suelo, 1 = subiendo, 2 = esperando arriba, 3 = bajando, 4 = esperando abajo
    private int estado = 0;

    /// <summary>
    /// Método para ver si el jugador entra en contacto con el trigger del globo
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement jugador = other.GetComponent<PlayerMovement>();
        if (jugador != null)
        {
            jugadorEncima = true;
            // Para que el jugador no clipee con el globo
            jugador.transform.SetParent(transform);
        }

    }
    /// <summary>
    /// Método para ver si el jugador deja de estar en contacto con el trigger del globo
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerMovement jugador = other.GetComponent<PlayerMovement>();
        if (jugador != null)
        {
            jugadorEncima = false;
            // Para que el jugador no clipee con el globo
            jugador.transform.SetParent(transform);
        }
    }
    /// <summary>
    /// Método para que el globo se mueva suavemente
    /// </summary>    
    private void MoverConstante(float alturaDestino)
    {
        Vector3 posicion = new Vector3(transform.position.x, alturaDestino, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posicion, velocidadGlobo * Time.deltaTime);
    }
    void Update()
    {
        switch (estado)
        {
            // Suelo
            case 0:
                if (jugadorEncima)
                {
                    // Empieza a subir
                    estado = 1;
                }
                break;
            // Subiendo
            case 1:
                MoverConstante(alturaMaxima);
                // Calcular la distancia para ver si ha llegado a la altura máxima, dejando un margen (0.1)
                if (Mathf.Abs(transform.position.y - alturaMaxima) <= 0.1f)
                {
                    tiempoEspera = tiempoEsperaArriba;
                    // Llega arriba
                    estado = 2;
                }
                break;
            // Esperando arriba
            case 2:
                // Restar el tiempo de espera
                tiempoEspera -= Time.deltaTime;
                if (tiempoEspera <= 0)
                {
                    // Empieza a bajar
                    estado = 3;
                }
                break;
            // Bajando
            case 3:
                MoverConstante(alturaInicial);
                // Calcular la distancia para ver si ha llegado a la altura inicial (suelo), dejando un margen (0.1)
                if (Mathf.Abs(transform.position.y - alturaInicial) <= 0.1f)
                {
                    if (jugadorEncima)
                    {
                        tiempoEspera = tiempoEsperaAbajo;
                        estado = 4;
                    }
                    else
                    {
                        // Llega al suelo
                        estado = 0;
                    }
                }
                break;
            // Esperando abajo
            case 4:
                tiempoEspera -= Time.deltaTime;
                if (tiempoEspera <= 0)
                {
                    // Empieza a subir
                    estado = 1;
                }
                break;
        }
    }
} // class Globo 

// namespace
