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
public class Doors : MonoBehaviour
{
    public string requiredKey;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        KeyInventory keyinventory = collision.gameObject.GetComponent<KeyInventory>();

        if (keyinventory != null )
        {
            if (keyinventory.HasKey(requiredKey))
            {
                Debug.Log("Puerta abierta");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Necesitas la llave" + requiredKey);
            }
        }
    }
} // class Doors 
// namespace
