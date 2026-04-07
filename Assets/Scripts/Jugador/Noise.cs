
//---------------------------------------------------------
// Funcionamiento de la mecánica de ruido
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.UI;


///Lógica básica de recepción de nivel de ruido y regeneración del mismo. 
///Tras recibir daño de ruido y permanecer 5 segundos sin subidas,
/// se inicia un proceso de regeneración del nivel de ruido hasta el 0. 
public class Noise : MonoBehaviour
{


    /// DECLARACIÓN DE VARIABLES
   // [SerializeField]
    public float noiseLevel = 0f;   //Nivel de ruido


    //Variable que guarda el último momento de impacto en el tiempo
    private float lastHitTime;

    //Retraso de tiempo entre último impacto y proceso re regeneración
    [SerializeField]
    private float healDelay = 5f;

    //Velocidad con la que se regenera el player
    [SerializeField]
    private float healRate = 14;




    //Variables para el HUD de la barra de ruido.
    [SerializeField]
    private Image noiseBar;

    //ruido potencial
    [SerializeField]
    private Image noiseBarPotential;

    //Overlay de daño recibido
    [SerializeField]
    private Image damageOverlay;

    //Porcentaje donde se comienza a aplicar la transparencia del Overlay daño
    [SerializeField]
    private float toleranciaAlDaño = 50f;


    [SerializeField]
    private CanvasGroup noiseHUD;

    private float noiseHUDApha;
    private float targetNoiseHUDAlpha;



    [SerializeField]
    private float fadeRate;

    private void Start()
    {
        UpdateBar();
    }

    private void Update()
    {
        //Se llama al Regenerate() en cada frame
        Regenerate();

        //Llamamos al cambio de alpha en cada frame.
        //Si se cambia el target de alpha, se inicia un fade.
        HUDPresence();


        //Código provisional de muerte [CAMBIAR]
        if (noiseLevel >= 100)
        {
            DeadbyNoise();
        }
    }



    /// MÉTODO DE ADICIÓN DE RUIDO: Éste método toma como medida del ruido a sumar 
    /// la cantidad de ruido que supone cada enemigo individualmente
    public void takeNoise(int noiseDamage)
    {
        //Establecemos visibilidad a la barra de ruido
        targetNoiseHUDAlpha = 1;


        noiseLevel += noiseDamage;
        noiseLevel = Mathf.Clamp(noiseLevel, 0, 100);

        //Se llama al método para actualizar en la barra el nivel de ruido provocado
        UpdateBar();

        //Se guarda el último frame en el que se ha recibido daño para Regenerate()
        lastHitTime = Time.time;
    }



    
    /// Método que salta cuando hay niveles de ruido después del momento de hit,    
    private void Regenerate()
    {
        //Salta cuando el tiempo actual supera al momento del último impacto más el delay decidido
        //y siempre que el nivel de ruido sea > 0
        if (Time.time >= lastHitTime + healDelay && noiseLevel > 0)
        {

            //Reducción progresiva del nivel de ruido hasta 0 usando como incremento el healRate definido
            noiseLevel = Mathf.MoveTowards(noiseLevel, 0, healRate * Time.deltaTime);
            UpdateBar();
          
            
            //Cuando se vacíe la barra de ruido, iniciamos el fade out
            if (noiseLevel <= 0)
            {
                targetNoiseHUDAlpha = 0;
            }
        }

    }
   
    /// Método para actualizar la barra de ruido tras cada acción de cambio. 
    /// Se divide entre 100 para ajustar el tamaño de la imagen.   
    private void UpdateBar()
    {
        noiseBar.fillAmount = noiseLevel / 100;
        UpdateDamageOverlay();
    }

    
    /// Método provisional de muerte por ruido
    private void DeadbyNoise()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        System.GC.Collect();
    }


    /// <summary>
    /// Método que controla la visibilidad de la barra de ruido.
    /// </summary>
    private void HUDPresence()
    {
        if (targetNoiseHUDAlpha == 0)
        {
            fadeRate = 3.5f;
            noiseHUD.alpha = Mathf.MoveTowards(noiseHUD.alpha, targetNoiseHUDAlpha, fadeRate * Time.deltaTime);
        }
        else
        {
            fadeRate = 2;
            noiseHUD.alpha = Mathf.MoveTowards(noiseHUD.alpha, targetNoiseHUDAlpha, fadeRate * Time.deltaTime);

        }


    }


    //sistemaa de ruido potencial (ImpactNoise)
  
    //barra potencial (usa la misma barra por ahora)
    public void UpdatePotentialBar(float potencial)
    {
        float valor = Mathf.Clamp(potencial, 0, 100);
        noiseBarPotential.fillAmount = valor / 100f;

        //restauramos el canal alpha al aparecer
        Color c = noiseBarPotential.color;
        c.a = 1f;
        noiseBarPotential.color = c;
    }

    public void ShowHUD()
    {
        targetNoiseHUDAlpha = 1;
    }

    //ocultar barra potencial (vuelve al valor real)
    public void HidePotentialBar()
    {
        noiseBarPotential.fillAmount = 0f;

        //si no hay ruido real ocultamos el HUD
        if (noiseLevel <= 0)
        {
            targetNoiseHUDAlpha = 0;
            Color c = noiseBarPotential.color;
            c.a = Mathf.MoveTowards(c.a, 0f, 0.2f * Time.deltaTime);
            noiseBarPotential.color = c;
        }
    }

    //Overlay progresivo de daño del jugador
    private void UpdateDamageOverlay()
    {
        if (damageOverlay != null)
        {
            Color color = damageOverlay.color;
            // si el nivel de ruido es menor a la toleranciaAlDaño, el Overlay se queda transparente
            if (noiseLevel <= toleranciaAlDaño)
            {
                color.a = 0f; 
            }
            // si supera la toleranciaAlDaño aumenta la opacidad acorde al porcentaje de barra de ruido restante (a la tolerancia)
            else
            {
                
                color.a = (noiseLevel - toleranciaAlDaño) / (100f - toleranciaAlDaño);
            }
            //aplica cambios
            damageOverlay.color = color;
        }
    }
}

