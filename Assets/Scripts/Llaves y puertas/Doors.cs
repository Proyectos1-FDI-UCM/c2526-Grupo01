//---------------------------------------------------------
// Script para asignar tipo a la puerta, y que esta se abre si se posee la llave correspondiente.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/*
Controla el comportamiento de una puerta.

Comprueba si el jugador tiene la llave necesaria al colisionar con ella.
Si la posee, la puerta se abre (se destruye). Si no, muestra un mensaje
indicando qué llave se necesita.
*/

public class Doors : MonoBehaviour
{
    // llave necesaria para abrir la puerta (se pone en unity)
    [SerializeField] private string requiredKey;

    // esto se ejecuta cuando algo choca con la puerta
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // intentamos coger el inventario del objeto que choca
        KeyInventory keyInventory = collision.gameObject.GetComponent<KeyInventory>();

        // si tiene inventario (jugador)
        if (keyInventory != null)
        {
            // comprobamos si tiene la llave correcta
            if (keyInventory.HasKey(requiredKey))
            {
                // si la tiene, abrimos la puerta (la borramos)
                Debug.Log("Puerta abierta");
                Destroy(gameObject);
            }
            else
            {
                // si no la tiene, avisamos
                Debug.Log("Necesitas la llave " + requiredKey);
            }
        }
    }
} // class Doors 
// namespace
