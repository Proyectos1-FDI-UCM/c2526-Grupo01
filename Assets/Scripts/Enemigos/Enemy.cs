//---------------------------------------------------------
// Función que complementa a Noise.cs y añade un valor de ruido a cada enemigo.
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Class enemy que establece el cooldown y el efecto de ruido de cada enemigo.
/// </summary>
public class Enemy : MonoBehaviour
{

    //Atributos publicos
    [SerializeField]
    private int noiseEffect;

    [SerializeField]
    private float cooldown = 1.0f;


    //Variable privada
    private float lastHitTime = -Mathf.Infinity;

    //Cuando entra en contacto con el player (gameobject que tiene noise) y ha pasado el tiempo
    //de cooldown, hace que se llame a takeNoise en Noise con el valor deseado.
 


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Noise noise = collision.gameObject.GetComponent<Noise>();
        //.GetComponent<Noise>();

        if (noise == null || Time.time < lastHitTime + cooldown)
        {
            // Debug.Log("No hay nada que haga ruido");


        }
        else noise.TakeNoise(noiseEffect);

    }




}
