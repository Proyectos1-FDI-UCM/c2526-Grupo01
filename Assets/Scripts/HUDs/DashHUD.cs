//---------------------------------------------------------
// Script para mostrar/ocultar el HUD del Dash
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
//uso la libreria DOTween que es la que nos permiten los profes
using DG.Tweening;
using System.Runtime.CompilerServices;


/// <summary>
/// Este script controla el icono del Dash dentro del HUD.
/// Su función es mostrar visualmente cuándo el jugador puede
/// volver a utilizar el dash.
///
/// Cuando el dash se recarga:
/// -El icono aparece gradualmente con una animación de "popeo"
/// -Se desvanece lentamente.
/// -Se reproduce un sonido indicando que el dash está listo.
///
/// Cuando el dash no está disponible, el icono desaparece
/// progresivamente 
/// </summary>
public class DashHUD : MonoBehaviour
{
    [SerializeField]
    private Image dashIcon;

    [SerializeField]
    private float fadeSpeed = 5f;

    [SerializeField]
    private PlayerMovement jugador;

    [SerializeField]
    private AudioSource readySound;
 

    private bool visible = false;
    private Vector3 originalScale;


    //Pongo constantes para que no existan "números mágicos",
    //y pongo constantes en lugar de serializables porque en principio están bien y no se deberían tocar
    private const float POP_INITIAL_SCALE = 0.7f;
    private const float POP_TARGET_SCALE = 1f;
    //lo q dura la animación
    private const float POP_DURATION = 0.2f;

    private const float FADE_IN_DURATION = 0.1f;
    private const float FADE_FROM_ALPHA = 0.3f;

    //alpha a 1 es q esta completamente visible
    private const float VISIBLE_ALPHA = 1f;
    //a 0 es invisible
    private const float HIDDEN_ALPHA = 0f;

    void Start()
    {
        //guardamos la escala real del icono
        originalScale = dashIcon.transform.localScale;
    }

    void Update()
    {
        //preguntamos al jugador si puede dashear o no
        bool canDash = jugador.canDashUI();

        //cambiamos la visibilidad si hace falta
        if (canDash != visible)
        {
            visible = canDash;

            if (visible)
            {
                dashIcon.transform.localScale = originalScale * POP_INITIAL_SCALE;

                //esto es para que haga pop, usando las librerias dot
                dashIcon.transform.DOScale(POP_TARGET_SCALE, POP_DURATION).SetEase(Ease.OutBack);

                //y esto es para que flashee al rellenarse
                dashIcon.color = Color.white;
                dashIcon.DOFade(VISIBLE_ALPHA, FADE_IN_DURATION).From(FADE_FROM_ALPHA);

                readySound.Play();
            }
        }

        Color c = dashIcon.color;

        float targetAlpha;

        if (visible)
        {
            targetAlpha = VISIBLE_ALPHA; 
        }
        else
        {
            targetAlpha = HIDDEN_ALPHA;
        }

        //se interpola suavemente el alpha (la transparencia)
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * fadeSpeed);

        //se aplica el nuevo color al icono
        dashIcon.color = c;
    }

}
