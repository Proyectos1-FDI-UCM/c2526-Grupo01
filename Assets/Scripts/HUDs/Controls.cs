//---------------------------------------------------------
// Muestra y oculta la imagen de los controles del juego
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Este script controla la visualización de la imagen de controles
/// dentro del menú de pausa. Cuando el jugador pulsa el botón de "Controles", se muestra una
/// imagen con las teclas y acciones del juego.
/// Mientras la imagen está activa, el jugador puede cerrarla pulsando
/// el botón de interactuar (E/Y).
/// </summary>
public class Controls : MonoBehaviour
{
    [SerializeField]
    private Image image;

    //indica si la imagen de controles se encuentras visible
    private bool active;

    private void Start()
    {
        //la imagen comienza oculta
        image.enabled = false;
    }
    private void Update()
    {
        //si el jugador ha pulsado el boton de interactuar y la imagen está activa, entonces se oculta
        if (InputManager.Instance.InteractWasPressedThisFrame())
        {
            if(active)
            {
                image.enabled = false;
                active = false;
            }
        }

    }

    //al pulsar el botón muestra la imagen de los controles
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
