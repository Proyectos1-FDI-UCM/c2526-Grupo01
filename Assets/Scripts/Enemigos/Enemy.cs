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

    private float lastHitTime = -Mathf.Infinity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Noise noise = collision.gameObject.GetComponent<Noise>();
            //.GetComponent<Noise>();

        if (noise == null || Time.time < lastHitTime + cooldown)
        {
           // Debug.Log("No hay nada que haga ruido");


        }
        else noise.takeNoise(noiseEffect);

    }


}
