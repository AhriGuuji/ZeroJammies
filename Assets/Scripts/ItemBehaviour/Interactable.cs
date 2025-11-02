using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : Item
{
    [SerializeField]
    protected float _interactRange;
    [SerializeField]
    protected string _inputInteract = "Interact";
    [SerializeField]
    protected LayerMask _playerLayer;
    [SerializeField]
    protected int _interactionsNeeded;
    protected InputAction _interact;
    protected int _interactionsPressed;

    protected virtual void Awake()
    {
        _interactionsPressed = 0;
        _interact = InputSystem.actions.FindAction(_inputInteract);
    }

    protected virtual void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange, _playerLayer);
        if (!collider) return;

        if (collider.GetComponent<PlayerMovement>())
        {
            if (_interact.WasPressedThisFrame())
            {
                _interactionsPressed++;
            }
        }

        if (_interactionsPressed == _interactionsNeeded)
        {
            GameEvent();
            _interactionsPressed++;
        }
    }

    protected virtual void GameEvent()
    {
        return;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
    
}
