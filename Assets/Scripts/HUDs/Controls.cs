//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Controls : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private bool active;

    private void Start()
    {
        image.enabled = false;
    }
    private void Update()
    {
        if (InputManager.Instance.InteractWasPressedThisFrame())
        {
            if(active)
            {
                image.enabled = false;
                active = false;
            }
        }

    }

    public void ShowControls()
    {
        if (!active)
        {
          image.enabled = true;
          active = true;
        }
    }

} // class Controls 
// namespace
