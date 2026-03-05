
//---------------------------------------------------------
// Funcionamiento de la mecánica de ruido
// Leopoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>
///Lógica básica de recepción de nivel de ruido y regeneración del mismo. 
///Tras recibir daño de ruido y permanecer 5 segundos sin subidas,
/// se inicia un proceso de regeneración del nivel de ruido hasta el 0. 
/// </summary>
public class Noise : MonoBehaviour
{

    /// <summary>
    /// DECLARACIÓN DE VARIABLES
    /// </summary>
    [SerializeField]
    private float noiseLevel = 0f;   //Nivel de ruido


    //Variable que guarda el último momento de impacto en el tiempo
    private float lastHitTime;

    //Retraso de tiempo entre último impacto y proceso re regeneración
    [SerializeField]
    private float healDelay = 5f;

    //Temporizador de regeneración
    private float healTimer;





    [SerializeField]
    private Image noiseBar;

    [SerializeField]
    private Image noiseBarBG;

    [SerializeField]
    private float fadeSpeed = 1.5f;

    private bool fadingIn= false;
    private bool fadingOut = false;

    private void Start()
    {
        noiseLevel = 0f;
    }

    private void Update()
    {
        //Se llama al Regenerate() en cada frame
        Regenerate();
    }


    /// <summary>
    /// MÉTODO DE ADICIÓN DE RUIDO: Éste método toma como medida del ruido a sumar 
    /// la cantidad de ruido que supone cada enemigo individualmente
    /// </summary>
    public void takeNoise(int noiseDamage)
    {
        if (noiseLevel <= 0);
        noiseLevel += noiseDamage;
        noiseLevel = Mathf.Clamp(noiseLevel, 0, 100);
        UpdateBar();
        //Se guarda en una variable el momento exacto del hit
        lastHitTime = Time.time;
        healTimer = 0f;
        Debug.Log("Ruido hecho. Tienes este ruido: " + noiseLevel);

    }

    /// <summary>
    /// Método que salta cuando hay niveles de ruido después del momento de hit,
    /// </summary>
    private void Regenerate()
    {

        if (Time.time >= lastHitTime + healDelay && noiseLevel > 0)
        {
            Debug.Log("Regenerando");

            //Se inicia el crono
            healTimer += Time.deltaTime;
            if (healTimer >= 1f)
            {
                //Se reduce el nivel de ruido por 20 cada segundo
                noiseLevel -= 20;
                UpdateBar();
                healTimer = 0f;

                //Cuando se llega a 0, se apaga el temporizador
                if (noiseLevel <= 0)
                {
                    Debug.Log("Se ha regenerado todo el ruido. Volvemo");
                    //Reiniciamos el timer a 0 para resetear el cooldown de 5 seg para el próximo hit
                    healTimer = 0f;
                }
            }
        }
        //Evitamos valores negativos de ruido
        if (noiseLevel < 0)
        {
            noiseLevel = 0;
            
        }
    }

    private void UpdateBar()
    {
        noiseBar.fillAmount = noiseLevel / 100;
    }
    
    //public void PotentialNoise(){}
}

