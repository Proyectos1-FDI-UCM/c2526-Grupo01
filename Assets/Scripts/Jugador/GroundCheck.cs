//---------------------------------------------------------
// Script para detectar suelo
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
///Script para detectar si el jugador está tocando el suelo o no
///para ello lo que hacemos es detectar con un trigger pequeño a la altura de 
///los pies del jugador
/// </summary>
public class GroundCheck : MonoBehaviour
{

    //hacemos un getter, setter de grounded (propiedad que pregunta si esta tocando suelo)
    //y el set en privado para q solo este script pueda cambiarlo
    public bool grounded { get; private set; }

   
    private void OnTriggerEnter2D(Collider2D other)
    {  
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        grounded = false;
    }
    
}
