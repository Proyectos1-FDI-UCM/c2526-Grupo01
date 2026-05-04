//---------------------------------------------------------
// Llave que, al colisionar con el jugador, se añade a su inventario.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class Key : MonoBehaviour
{
    // color o tipo de la llave (lo ponemos desde unity)
    [SerializeField] private string keyColor;

    // esto se ejecuta cuando algo entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // intentamos coger el inventario del objeto que ha tocado la llave
        KeyInventory keyInventory = other.GetComponent<KeyInventory>();

        // si tiene inventario (normalmente el jugador)
        if (keyInventory != null)
        {
            // le añadimos la llave
            keyInventory.AddKey(keyColor);

            // destruimos la llave del mapa
            Destroy(gameObject);
        }
    }
} // class Key 
// namespace
