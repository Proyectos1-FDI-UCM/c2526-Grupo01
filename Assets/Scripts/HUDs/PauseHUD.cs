//---------------------------------------------------------
// Menú de pausa
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// Añadir aquí el resto de directivas using


/// <summary>
/// Si pulsas ESC / Start se abre/cierra el menu de pausa, pausando el juego y permitiendo volver al menú principal o salir del juego 
/// </summary>
public class PauseHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuPausa; //esto es el canvas del menu d pausa


    private bool activo;

    private void Start()
    {
        //el menu empieza cerrado
        MenuPausa.SetActive(false);
        activo = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (MenuPausa != null)
        {
            
            if (InputManager.Instance.PauseWasPressedThisFrame())
            {
                UpdateHUD();
            }

        }
        else
        {
            //meto un warning x si se os olvida linkearlo
            Debug.LogWarning("No se ha asociado un Menú en el Serialize");
        }

    }

    //si esta activo se oculta y viceversa
    private void UpdateHUD() 
    {

        if (activo) 
        {
            MenuPausa.SetActive(false);
            activo = false;
            Time.timeScale = 1f;
        }
        else if (!activo)
        {
            MenuPausa.SetActive(true);
            activo = true;
            Time.timeScale = 0f;
        }
    }


} 
