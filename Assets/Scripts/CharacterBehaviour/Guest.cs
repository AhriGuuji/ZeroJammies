using System.Collections;
using UnityEngine;

public class Guest : Character
{
    [Header("FallCheck")]
    [SerializeField] private Transform fallCheck;
    [SerializeField] private float fallCheckRadius = 2.0f;
    [Header("WallCheck")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 2.0f;

    [Header("Stress Levels")]
    [field: SerializeField]
    public float StressLevel { get; private set; }
    [field: SerializeField]
    public bool CanStress { get; private set; }
    [SerializeField]
    private float _maxStressLevel;
    [SerializeField]
    private float _cdToIncStress;
    private bool cantPass;
    private bool willFall;
    private Vector2 direction = Vector2.right;

    protected override void Update()
    {
        PreventFalling();
        WallNear();

        if (willFall | cantPass)
            direction = -direction;

        base.Update();

        if (StressLevel >= _maxStressLevel)
        {
            FindAnyObjectByType<GameManager>().GuestRunAway(this);
            Destroy(this);
        }

        Debug.Log(StressLevel);
    }

    public IEnumerator IncrementStress(float stressGained)
    {
        CanStress = false;

        WaitForSeconds wfs = new(_cdToIncStress);
        StressLevel += stressGained;

        yield return wfs;

        CanStress = true;
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
