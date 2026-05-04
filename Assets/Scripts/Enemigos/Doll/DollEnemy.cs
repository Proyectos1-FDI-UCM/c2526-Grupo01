//---------------------------------------------------------
// Comportamiento de la muñeca, también controla sus animaciones de caída
// Adrián de la Calle
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class DollEnemy : MonoBehaviour
{
    [Header("Sonidos estatua")]

    [SerializeField]
    private AudioClip screamSound;

    [SerializeField]
    private AudioClip breakSound;

    [SerializeField]
    private AudioClip explosionSound;

    //para controlar el volumen de cada sonido
    [SerializeField]
    private float volumeScream;

    [SerializeField]
    private float volumeBreak;

    [SerializeField]
    private float volumeExplosion;

    //el audiosource principal lo uso para grito y para sonido de romper.
    private AudioSource audioSource;


    [Space]
    [Header("Atributos estatua")]

    [SerializeField]
    private float knockbackForceX = 6f;
    [SerializeField]
    private float knockbackForceY = 4f;

    [SerializeField]
    private float fallDelay = 2f; //PARA QUE CUADRE CON LA ANIMACIÓN: 2f
    [SerializeField]
    private float screamDelay = 2f; //2f
    [SerializeField]
    private float destroyTime = 0; //0f


    [SerializeField]
    private GameObject largeDollHitbox;

    [SerializeField] //ANIMATOR DE ADRIÁN
    private Animator _animator;

    private bool playerDetected = false;
    private float timer = 0f;
    private Vector3 spawnPoint;

    private int state = 0;
    // 0 = idle
    // 1 = waiting scream
    // 2 = waiting fall
    // 3 = destroy

    public void Start()
    {
        _animator = GetComponent<Animator>();
        spawnPoint = transform.position;

        //incializo el audioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (playerDetected) 
        {

            timer += Time.deltaTime;

            switch (state)
            {
                case 1: // Espera grito
                    if (timer >= screamDelay)
                    {
                        timer = 0f;
                        Scream();

                    }
                    break;

                case 2: // Espera caída

                    if (timer >= fallDelay)
                    {
                        timer = 0f;
                        FallAttack();
                    }
                    break;

                case 3: // Espera destruirse

                    if (timer >= destroyTime)
                    {

                        Destroy(gameObject);
                    }
                    break;
            }
        } 

       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && !playerDetected)
        {
            playerDetected = true;

            _animator.SetTrigger("grito");
            activateScreamSound();
            state = 1;
            timer = 0f;
        }
    }

    public void Scream()
    {
        _animator.SetTrigger("caida");
        activateBreakSound();

        state = 2;
        timer = 0f;
    }

    public void FallAttack()
    {
        activateExplosionSound();
        Instantiate(largeDollHitbox, spawnPoint, Quaternion.identity);


        state = 3;
        timer = 0f;
    }

    private void activateScreamSound()
    {
        audioSource.Stop();
        audioSource.clip = screamSound;
        audioSource.volume = volumeScream;
        //el sonido de golpe solo lo reproduzco una vez porque no tiene sentido que sea un loop
        audioSource.loop = false;
        audioSource.Play();
    }


    private void activateBreakSound()
    {
        audioSource.Stop();
        audioSource.clip = breakSound;
        audioSource.volume = volumeBreak;
        //el sonido de golpe solo lo reproduzco una vez porque no tiene sentido que sea un loop
        audioSource.loop = false;
        audioSource.Play();
    }


    private void activateExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound, volumeExplosion);

    }

}
