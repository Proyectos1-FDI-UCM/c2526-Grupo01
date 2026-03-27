//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class KeyInventory : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public void AddKey(string key)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            Debug.Log("Llave recogida" + key);
        }
    }
    public bool HasKey(string key)
    {
        return keys.Contains(key);
    }
} // class KeyInventory 
// namespace
