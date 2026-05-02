//---------------------------------------------------------
// Código de la zona letal que perseguirá al player en el boss final
// Lepoldo Gutiérrez Cobo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class BossZone : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //Variable de velociad
    [SerializeField]
    private float speed = 15f;

    //Transform de referencia con la posi del jugador
    [SerializeField]
    private Transform jugador;

    //Trigger del inicio de la persecución
    [SerializeField]
    private Collider2D detector;

    //Variable para el retardo entre activación y persecución
    [SerializeField]
    private float triggerDelay = 3f;


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //rb para el movimiento
    private Rigidbody2D rb;

    //Bool para activar y desactivar la persecución
    private bool chasing = false;

    //Variable para guardar el momento de activación de la persecución
    private float triggerTime;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //En cada frame se hace una cosa u otra dependiendo del estado de chasing
        if (chasing) BossChase();
        else BossStill();

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void BossChase()
    {
        //Si el tiempo actual supera al momento del trigging + el delay establecido
        //la zona de boss empieza a seguir al componente referencia del player


        if (Time.time >= triggerTime + triggerDelay)
        {
            Vector2 direction = (jugador.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }

    }

    //Método que pone chasing a true e inicia el contador con triggerTime
    public void StartChase()
    {
       
        chasing = true;
        triggerTime = Time.time;
        Debug.Log("empieza el boss. Chase = " + chasing);

    }

    //Método que pone chasing a false
    public void EndChase()
    {
        chasing = false;
        Debug.Log("Deja de perseguir. Chase = " + chasing);

    }

    //Método que detendrá al boss
    public void BossStill()
    {
        rb.linearVelocity = Vector2.zero;
    }



    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class BossZone 
// namespace
