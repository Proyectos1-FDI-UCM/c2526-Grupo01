//---------------------------------------------------------
// Script para gestionar el HUD del Dash
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
//uso la libreria DOTween que es la que nos permiten los profes
using DG.Tweening;

// Script para mostrar/ocultar el HUD del Dash
public class DashHUD : MonoBehaviour
{
    public Image dashIcon;
    public float fadeSpeed = 5f;

    [SerializeField]
    private PlayerMovement jugador;

    [SerializeField]
    private AudioSource readySound;
 

    private bool visible = false;
    private Vector3 originalScale;

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
                dashIcon.transform.localScale = originalScale * 0.7f;

                //esto es para que haga pop, usando las librerias dot
                dashIcon.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);

                //y esto es para que flashee al rellenarse
                dashIcon.color = Color.white;
                dashIcon.DOFade(1f, 0.1f).From(0.3f);

                readySound.Play();
            }
        }

        //hacemos el fade fade
        Color c = dashIcon.color;

        float targetAlpha;

        if (visible)
        {
            targetAlpha = 1f; //alpha a 1 es q esta completamente visible
        }
        else
        {
            targetAlpha = 0f; //a 0 es invisible
        }

        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * fadeSpeed);

        dashIcon.color = c;
    }

}
