//---------------------------------------------------------
// Enemigo que persigue al jugador cuando entra en su zona y vuelve a su posicion inicial cuando el jugador se va tambien empuja al jugador cuando chocan.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class BujaranaEnemy : MonoBehaviour
{

    [Header("Sonidos Bujarania")]

    [SerializeField]
    private AudioClip spiderSound;

    [SerializeField]
    private AudioClip hitSound;


    //para controlar el volumen de cada sonido
    [SerializeField]
    private float volumeSpider;

    [SerializeField]
    private float volumeHit;

    //para ver cual es el clip actual y hacer que no suene todo el rato
    private AudioClip currentClip;

    private AudioSource audioSource;

    [Space]
    [Header("Atributos Bujarania")]

    // referencia al jugador
    [SerializeField] private Transform player;

    // velocidad del enemigo
    [SerializeField] private float speed = 2f;

    // posicion inicial del enemigo (para volver cuando el jugador se va)
    private Vector3 homeposition;

    // indica si el jugador esta dentro de la zona
    private bool playerInZone = false;

    // fuerza del empujon en X
    [SerializeField] private float knockbackForceX = 6f;

    // fuerza del empujon en Y
    [SerializeField] private float knockbackForceY = 4f;

    // tiempo que dura el stun
    [SerializeField] private float stunDuration = 1.5f;

    // estado de stun
    private bool isStunned = false;

    // contador de stun
    private float stunTimer = 0f;

    void Start()
    {
        // guardamos la posicion inicial al empezar
        homeposition = transform.position;

        //sonido de araña al empezar
        audioSource = GetComponent<AudioSource>();
   
    }

    void Update()
    {
        // si esta stuneado, no se mueve
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                isStunned = false;
            }

        }
        else
        {
            //activo el sonido de araña
            if (currentClip != spiderSound)
            {
                activateSpiderSound();
                currentClip = spiderSound;
            }

            // si el jugador esta en la zona y existe
            if (playerInZone && player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards
                    (transform.position, homeposition, speed * Time.deltaTime);
            }

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        // si es el jugador y no esta ya stuneado
        if (player != null && !isStunned)
        {
            //activo el sonido de golpe
            activateHitSound();

            // activar stun
            isStunned = true;
            stunTimer = stunDuration;

            // direccion del empujon
            Vector2 direction = collision.transform.position - transform.position;
            float pushX = Mathf.Sign(direction.x);

            // aplicar knockback
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);
        }
    }

    public void PlayerEnteredZone()
    {
        playerInZone = true;
    }

    public void PlayerLeftZone()
    {
        playerInZone = false;
    }

    private void activateSpiderSound()
    {
        audioSource.Stop();
        audioSource.clip = spiderSound;
        audioSource.volume = volumeSpider;
        audioSource.loop = true;
        audioSource.Play();

    }

    private void activateHitSound()
    {
        audioSource.Stop();
        audioSource.clip = hitSound;
        audioSource.volume = volumeHit;
        //el sonido de golpe solo lo reproduzco una vez porque no tiene sentido que sea un loop
        audioSource.loop = false;
        audioSource.Play();
        currentClip = hitSound;
    }
}
