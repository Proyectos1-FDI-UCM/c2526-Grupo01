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
public class Key : MonoBehaviour
{
    public string KeyColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        KeyInventory keyinventory = other.GetComponent<KeyInventory>();

        if (keyinventory != null )
        {
            keyinventory.AddKey(KeyColor);
            Destroy(gameObject);
        }
    }
} // class Key 
// namespace
