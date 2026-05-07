//---------------------------------------------------------
// Controla la mano que avisa de muerte por ruido
// Adrián de la Calle Valiente
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class DeathHand : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float distanceX = 30f;
    [SerializeField]
    private float distanceY = 30f;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float maxnoise = 100f;

    [SerializeField]
    private Noise ruido;

    private Vector3 restposition;
    private bool moveIt = false;

    private void Start()
    {
        //guardamos en restposition la posición que la mano mantendrá respecto al jugador
        restposition = new Vector3(distanceX, distanceY, 0);
    }
    private void Update()
    {
        if (player != null && ruido != null)
        {
            moveIt = ruido.GetNoise() >= maxnoise;

            if (!moveIt) //Si el nivel de ruido supera el máximo establecido (que debería ser 100) la mano se desplaza hasta la posición del jugador, para simular que le atrapa
            {
                Vector3 TargetPosition = player.position + restposition;
                transform.position = Vector3.Lerp(transform.position, TargetPosition, speed * Time.deltaTime);
            }
            else //En caso contrario, la mano mantiene la distancia fija con el jugador
            {
                Vector3 TargetPosition = player.position + restposition;
                transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }
} 
