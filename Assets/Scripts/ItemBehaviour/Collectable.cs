using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField]
    protected Show _respectiveShow;
    protected Inventory _inventory;
    protected override void Awake()
    {
        base.Awake();

        _inventory = FindAnyObjectByType<Inventory>();
    }

    protected override void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange, _playerLayer);
        if (!collider) return;

        if (collider.GetComponent<PlayerMovement>())
        {
            if (_interact.WasPressedThisFrame())
            {
                Debug.Log("grab");
                _inventory.AddToInventory(this);
                StartCoroutine(_respectiveShow.ShowMe());
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collectable>().enabled = false;
            }
        }
    }
}
