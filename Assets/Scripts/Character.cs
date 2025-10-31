using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Run Settings")]
    [SerializeField] protected Vector2 _moveSpeed;
    [SerializeField] protected float acceleration = 1f;
    [SerializeField] protected float decceleration = 1f;
    [SerializeField] protected float _velPower = 2;
    [SerializeField] protected float _friction = 0.2f;

    [Header("GroundCheck")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckRadius = 2.0f;
    [SerializeField] protected LayerMask groundLayer;

    protected Rigidbody2D rb;
    protected Vector2 playerDir;
    protected bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GetDirection() < 0 && (transform.right.x > 0))
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else if (GetDirection() > 0 && (transform.right.x < 0))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    protected virtual void FixedUpdate()
    {
        #region Run
        //Direção e movimento desejados ao primir
        float _targetVelocity = GetDirection() * _moveSpeed.x;
        //Calcula a diferença entre velocidade atual e a desejada
        float _speedDif = _targetVelocity - rb.linearVelocity.x;
        //Aceleração dependente da situação
        float _accelRate = (Mathf.Abs(_targetVelocity) > 0.01f) ? acceleration : decceleration;
        //Movimentação final
        float _movement = Mathf.Pow(Mathf.Abs(_speedDif) * _accelRate, _velPower) * Mathf.Sign(_speedDif);

        rb.AddForce(_movement * Vector2.right);
        #endregion

        #region Friction
        if (isGrounded && Mathf.Abs(GetDirection()) < 0.01f)
        {
            //Valor da quantidade de fricção
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocity.x), Mathf.Abs(_friction));
            //Direção da fricção
            amount *= Mathf.Sign(rb.linearVelocity.x);
            //Aplicar a força
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion
    }

    protected virtual void ComputeGroundState()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        isGrounded = (collider != null);
    }

    protected virtual void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    protected abstract float GetDirection();
}
