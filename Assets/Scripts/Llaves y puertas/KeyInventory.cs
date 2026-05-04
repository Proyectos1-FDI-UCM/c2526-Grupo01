//---------------------------------------------------------
// Gestiona el inventario de llaves del jugador.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    // lista donde guardamos las llaves que tiene el jugador
    private List<string> keys = new List<string>();

    // este metodo sirve para añadir una llave al inventario
    internal void AddKey(string key)
    {
        // si no tenemos ya esa llave
        if (!keys.Contains(key))
        {
            // la añadimos a la lista
            keys.Add(key);

            // mensaje para comprobar que funciona
            Debug.Log("Llave recogida " + key);
        }
    }

    // este metodo comprueba si tenemos una llave concreta
    internal bool HasKey(string key)
    {
        // devuelve true si la lista contiene esa llave
        return keys.Contains(key);
    }
} // class KeyInventory 
// namespace
