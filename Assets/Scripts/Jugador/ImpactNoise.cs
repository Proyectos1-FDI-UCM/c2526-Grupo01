//---------------------------------------------------------
// Script que controla el daño por velocidad potencial del jugador, mecanica que consiste en un "daño potencial" 
// que se muestra en la barra de ruido el cual se aplica solo si el jugador se choca con algo a alta velocidad
// Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class ImpactNoise : MonoBehaviour
{
    [Header("Configuración de impacto")]

    //velocidad minima de impacto para mostrar velocidad potencial
    [SerializeField] private float velMinImpact = 12f;
    //multiplicador de daño de impacto, para balancear el daño recibido
    [SerializeField] private float multDamage = 3f;
    //layer que ignora la colision de daño (enemigos)
    [SerializeField] private LayerMask layerEnemigos;

    [Header("Referencias")]
    //referencias
    [SerializeField] private Noise noise;
    [SerializeField] private BoxCollider2D hitboxIzq;
    [SerializeField] private BoxCollider2D hitboxDrch;
    private Rigidbody2D rb;

    //variables privadas
    private float velAnterior;
    private bool acabaDeImpactar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //cada frame almacena cual fue la ultima velocidad del jugador
        velAnterior = Mathf.Abs(rb.linearVelocity.x);
        acabaDeImpactar = false;
        
        //si esa velocidad fue mayor a la minima de impacto, calcula el daño potencial, lo actualiza con el metodo y lo muestra
        if (velAnterior >= velMinImpact)
        {
            float potencialDamage = CalcDamage(velAnterior);
            noise.UpdatePotentialBar(potencialDamage);
            noise.ShowHUD();
        }
        //si deja de serlo oculta la barra y actualiza el impacto
        else
        {
            noise.HidePotentialBar();
            acabaDeImpactar = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //convertimos la layer del objeto en formato bitmask para poder compararla (formato complicado que no entiendo del todo)
        int layerObjeto = 1 << other.gameObject.layer;
        //comparamos, en caso de que los dos bitmask sean iguales (igual al de enemigo) no cuenta trigger 
        if ((layerObjeto & layerEnemigos.value) != 0)
        {
            return;
        }

        // en caso que se supere la velocidad minima de impacto
        if (velAnterior >= velMinImpact)
        {
            //calculamos el incremento de daño 
            float realDamage = CalcDamage(velAnterior);
            noise.HidePotentialBar();
            noise.takeNoise(Mathf.RoundToInt(realDamage));
            acabaDeImpactar = true;
        }
    }

    //metodo para calcular el daño real que se aplica al HUD, con el multiplicador del inspector integrado
    private float CalcDamage(float velocidadX)
    {
        float Damage = velocidadX - velMinImpact;
        return Damage * multDamage;
    }
}
