//---------------------------------------------------------
// Script que controla el daño por velocidad potencial del jugador, mecanica que consiste en un "daño potencial" 
// que se muestra en la barra de ruido el cual se aplica solo si el jugador se choca con algo a alta velocidad
// Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class ImpactNoise : MonoBehaviour
{
    [Header("Configuración de impacto")]
    [SerializeField] private float velocidadMinimaImpacto = 12f;  // velocidad X mínima para hacer daño
    [SerializeField] private float multiplicadorDanyo = 3f;        // cuánto ruido por unidad de velocidad
    [SerializeField] private LayerMask layerEnemigos;              // layer de enemigos para ignorarlos

    [Header("Referencias")]
    [SerializeField] private Noise noise;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ActualizarBarraPotencial();
    }

    // Actualiza la barra potencial en tiempo real según la velocidad X actual
    private void ActualizarBarraPotencial()
    {
        float velocidadX = Mathf.Abs(rb.linearVelocity.x);

        if (velocidadX >= velocidadMinimaImpacto)
        {
            float danyoPotencial = CalcularDaño(velocidadX);
            noise.UpdatePotentialBar(danyoPotencial);
        }
        else
        {
            noise.HidePotentialBar();
        }
    }

    // Se llama automáticamente cuando el jugador colisiona con algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignoramos enemigos
        if (EsEnemigo(collision.gameObject))
        {
            return;
        }

        float velocidadX = Mathf.Abs(rb.linearVelocity.x);

        if (velocidadX >= velocidadMinimaImpacto)
        {
            int danyo = Mathf.RoundToInt(CalcularDaño(velocidadX));
            noise.takeNoise(danyo);
        }
    }

    // Calcula el daño proporcional a la velocidad X
    private float CalcularDaño(float velocidadX)
    {
        float exceso = velocidadX - velocidadMinimaImpacto;
        return exceso * multiplicadorDanyo;
    }

    // Comprueba si el objeto es un enemigo por su layer
    private bool EsEnemigo(GameObject obj)
    {
        int layerObjeto = 1 << obj.layer;

        if ((layerObjeto & layerEnemigos.value) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
