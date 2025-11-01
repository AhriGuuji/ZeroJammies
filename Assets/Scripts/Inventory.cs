using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [field: SerializeField]
    public Item[] _items { get; private set; }
    [SerializeField]
    private int _inventorySize = 5;

    [field: SerializeField]
    public bool WasGrabbed { get; private set; }
    [Header("Command List")]
    [SerializeField]
    private string _drop = "Drop";
    [SerializeField]
    private string _next = "Next";
    [SerializeField]
    private string _previous = "Previous";
    private int _selectedItem;

    private void Awake()
    {
        _items = new Item[_inventorySize];
        _selectedItem = 0;
    }    
    private void Update()
    {
        if (InputSystem.actions.FindAction(_drop).WasPressedThisFrame())
            RemoveFromInventory(_selectedItem);
        if (_selectedItem != _inventorySize &&
            InputSystem.actions.FindAction(_next).WasPressedThisFrame())
            _selectedItem++;
        if (_selectedItem != 0 &&
            InputSystem.actions.FindAction(_previous).WasPressedThisFrame())
            _selectedItem--;
    }

    public Item[] AddToInventory(Item item)
    {
        List<Item> _itemsList = new(_items);

        if (_itemsList.Count - _inventorySize != 0)
        {
            _items.Append(item);
            WasGrabbed = true;
        }
        else WasGrabbed = false;

        return _items =  _itemsList.ToArray();
    }

    public Item[] RemoveFromInventory(int itemID)
    {
        List<Item> _itemsList = new(_items);

        Instantiate(_items[itemID],transform);
        _itemsList.Remove(_items[itemID]);

        return _items = _itemsList.ToArray();
    }
}
