//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script que al entrar en contacto con el trigger muestra un panel con
/// un video tutorial describiendo esa mécanica
/// </summary>
public class Videotutorial : MonoBehaviour
{
    private bool activado = false;

    private Canvas canvas;


    private void Start()
    {
        //pillo el canvas del hijo
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        if (InputManager.Instance.InteractWasPressedThisFrame())
        {
            if(canvas.enabled) 
            { 
                canvas.enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activado) 
        {
            canvas.enabled = true; 
            activado = true;


        }
    }

} // class Videotutorial 
// namespace
