//---------------------------------------------------------
// Script que gestiona el ruido potencial
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class ImpactNoise : MonoBehaviour
{
    [Header("POTENCIAL INICIAL (Si sprinta sin daño previo)")]
    //porcentaje de daño potencial INICIAL(0–100)
    [SerializeField] 
    private float porcentajePotencialInicial = 50f;


    [Header("Porcentaje de daño añadido si llevaba ya ruido [Entre 0 - 1]")]
    [SerializeField]
    private float porcentajeConRuido = 0.4f;

    //layer que ignora la colision de daño (enemigos)


    [Header("Referencia")]
    [SerializeField] 
    private Noise noise;

    private float potencialGuardado = 0f;
    private bool acabaDeImpactar;

    private bool esEnemigo = false;

    [SerializeField] 
    private float tiempoEntreImpactos = 0.3f;
    private float ultimoImpacto = -999f;


    private bool estabaSprintando = false;

    private void Update()
    {
        bool sprint = InputManager.Instance.SprintIsPressed();


        //si está sprintando mostramos daño potencial
        if (sprint && !estabaSprintando)
        {
            if (noise.noiseLevel == 0) 
            {
                potencialGuardado = porcentajePotencialInicial;
            }
            else if (noise.noiseLevel > 0) 
            {
                potencialGuardado = noise.noiseLevel + (noise.noiseLevel * porcentajeConRuido);
            }

            potencialGuardado = Mathf.Clamp(potencialGuardado, 0, 100);

            noise.UpdatePotentialBar(potencialGuardado);
            noise.ShowHUD();
        }


        //si deja d sprintar se oculta la barra
        if (!sprint)
        {
            noise.HidePotentialBar();
            potencialGuardado = 0f;
        }

        estabaSprintando = sprint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemigo = collision.gameObject.GetComponent<Enemy>();

        if(enemigo != null) 
        {
            esEnemigo = true;
        }
        else 
        {
            PotencialNoise();
        }

       
    }

    private void PotencialNoise() 
    {
        if (Time.time >= ultimoImpacto + tiempoEntreImpactos)
        {
            //aplicar daño solo si había potencial
            if (potencialGuardado > 0)
            {
                noise.HidePotentialBar();
                noise.takeNoise(Mathf.RoundToInt(potencialGuardado));

                ultimoImpacto = Time.time;
            }

            esEnemigo = false;
        }
        else
        {
            //no hace nada (xq el cooldown activo)
        }
    }
}

