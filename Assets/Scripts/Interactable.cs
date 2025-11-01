using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : Item
{
    [SerializeField]
    protected float _interactRange;
    [SerializeField]
    protected string _inputInteract;
    protected InputAction _interact;

    protected virtual void Awake()
    {
        _interact = InputSystem.actions.FindAction(_inputInteract);
    }

    protected virtual void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange);


        if (collider.GetComponent<PlayerMovement>())
        {
            Debug.Log("Alo");
            if (_interact.WasPressedThisFrame())
            {
                Debug.Log("Clicked E or Space");
            }
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}
