using UnityEngine;

public class Collectable : Interactable
{
    private Inventory _inventory;
    protected override void Awake()
    {
        base.Awake();

        //_inventory = FindAnyObjectByType<Inventory>();
    }

    private void Update()
    {
        if (_interact.WasPressedThisFrame())
        {
            _inventory.AddToInventory(this);
        }
    }

    protected override void OnDrawGizmo()
    {
        base.OnDrawGizmo();
    }
}
