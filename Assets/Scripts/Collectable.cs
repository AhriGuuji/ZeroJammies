using UnityEngine;

public class Collectable : Interactable
{
    private Inventory _inventory;
    protected override void Awake()
    {
        base.Awake();

        _inventory = FindAnyObjectByType<Inventory>();
    }

    protected override void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange);


        if (collider.GetComponent<PlayerMovement>())
        {
            Debug.Log("Alo");
            if (_interact.WasPressedThisFrame())
            {
                _inventory.AddToInventory(GetComponent<Item>());
                gameObject.SetActive(false);
                Debug.Log("Popo");
            }
        }
    }
}
