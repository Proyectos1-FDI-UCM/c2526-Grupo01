using UnityEngine;
using UnityEngine.U2D;
//CODIGO PARA LAS ANIMACIONES


public class AnimaPlayer : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField] 
    private Transform sprite;
    [SerializeField] 
    private Collision sueloDetector;
    [SerializeField]
    private Noise ruido;
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField] 
    private Rigidbody2D rb;

    private float damageTimer;  
    private const float DAMAGE_DURATION = 0.2f;
    private bool isDamaged;

    private void Start()
    {
        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (isDamaged)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0f)
            {
                _animator.SetBool("isDamaged", false);
                isDamaged = false;
            }
        }

        float move = InputManager.Instance.MovementVector.x;


        bool moving = Mathf.Abs(move) > 0.1f;
        bool enSuelo = sueloDetector.IsOnGround();
        bool dashing = movement.GetIsDashing();


        _animator.SetBool("isMoving", moving);

        _animator.SetBool("isDashing", dashing);

        _animator.SetBool("enAire", !enSuelo);
    }

    public void Flip(int side)
    {

        Vector3 scale = sprite.localScale;
        scale.x = Mathf.Abs(scale.x) * side;
        sprite.localScale = scale;

    }

    public void TakeDamage()
    {
        _animator.SetBool("isDamaged", true);
        isDamaged = true;
        damageTimer = DAMAGE_DURATION;
    }

    public void ResetDamage()
    {
        _animator.SetBool("isDamaged", false);
    }
}
