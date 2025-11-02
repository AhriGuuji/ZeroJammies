using UnityEngine;

public class Guest : Character
{
    [Header("FallCheck")]
    [SerializeField] private Transform fallCheck;
    [SerializeField] private float fallCheckRadius = 2.0f;
    [Header("WallCheck")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 2.0f;
    private bool cantPass;
    private bool willFall;
    private Vector2 direction = Vector2.right;
    protected override void Update()
    {
        PreventFalling();
        WallNear();

        if (willFall | cantPass)
            direction = -direction;

        Debug.Log(direction);
        Debug.Log(willFall);
        Debug.Log(cantPass);
                
        base.Update(); 
    }

    private void WallNear()
    {
        Collider2D collider = Physics2D.OverlapCircle(wallCheck.position,
            wallCheckRadius, groundLayer);

        if (collider != null)
            cantPass = true;
        else cantPass = false;
    }
    private void PreventFalling()
    {
        Collider2D collider = Physics2D.OverlapCircle(fallCheck.position,
            fallCheckRadius, groundLayer);

        if (collider == null)
            willFall = true;
        else willFall = false;
    }

    protected override float GetDirection()
    {
        return direction.x;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(fallCheck.position, fallCheckRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}
