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
        restposition = new Vector3(distanceX, distanceY, 0);
    }
    private void Update()
    {
        if (player != null && ruido != null)
        {
            moveIt = ruido.GetNoise() >= maxnoise; 

            if (!moveIt)
            {
                Vector3 TargetPosition = player.position + restposition;
                transform.position = Vector3.Lerp(transform.position, TargetPosition, speed * Time.deltaTime);
            }
            else
            {
                Vector3 TargetPosition = player.position + restposition;
                transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
            }
        }
    }
} 
