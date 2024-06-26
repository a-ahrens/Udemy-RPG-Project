using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Collision
    [Header("Collission Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckRadius;

    #endregion

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    protected bool isKnocked;
    [SerializeField] protected float knockbackDuration;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }

    #endregion

    public int facingDirection { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockBack");
        Debug.Log(gameObject.name + " Was Damaged.");
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDirection, knockbackDirection.y);
    
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    #region Velocity Controls

    public void SetZeroVelocity()
    {
        if (isKnocked)
        {
            return;
        }
            
        rb.velocity = new Vector2(0, 0);
        
    }
        
    public void SetVelocity(float xVelocity, float YVelocity)
    {
        if(isKnocked)
        {
            return;
        }
        
        rb.velocity = new Vector2(xVelocity, YVelocity);
        FlipController(xVelocity);
    }

    #endregion

    #region Collision Detection

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion

    #region Flip Methods
    public virtual void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        else if (x < 0 && facingRight)
        {
            Flip();
        }
    }

    #endregion
}
