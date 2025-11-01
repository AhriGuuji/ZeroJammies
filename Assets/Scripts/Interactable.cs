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

    protected virtual void OnDrawGizmo()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRange);
    }
}
