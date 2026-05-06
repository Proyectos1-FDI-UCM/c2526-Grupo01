//---------------------------------------------------------
// Código para que el trigger del Boss lance daño al Noise.cs de Jugador
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Rendering;
// Añadir aquí el resto de directivas using


/// <summary>
/// Código para añadirle ruido al jugador, ya que el boss no tiene collider.
/// </summary>
public class BossDamage : MonoBehaviour
{
 
  
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    //Valor elevado de ruido para garantizar la muerte instantánea
    private int damage = 999;
    //Componente de ruido del player
    private Noise player;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    //Busca en el player el componente Noise y le aplica el valor deseado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Noise>())
        {
            Debug.Log("He chocado con player");
            player = collision.GetComponent<Noise>();
            player.takeNoise(damage);
        }
       

    }
    
    #endregion

  
    
  
} 
// namespace
