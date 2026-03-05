//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int noiseEffect = 3;

    [SerializeField]
    private float cooldown = 1.0f;

    [SerializeField]
    private float healDelay = 2.0f;
    [SerializeField]
    private float healRate = 20f;

    private float lastHitTime = -Mathf.Infinity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Noise noise = other.GetComponent<Noise>();

        if (noise == null || Time.time < lastHitTime + cooldown)
        {
            Debug.Log("No hay nada que haga ruido");


        }
        else noise.takeNoise(noiseEffect);

    }

     void Update()
    {
        
    }


   




}
