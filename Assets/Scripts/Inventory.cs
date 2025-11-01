using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> _items;

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
        _items = new List<Item>();
        _selectedItem = 0;
    }    
    private void Update()
    {
        if (InputSystem.actions.FindAction(_drop).WasPressedThisFrame())
            RemoveFromInventory(_selectedItem);
        if (_selectedItem != _items.Count &&
            InputSystem.actions.FindAction(_next).WasPressedThisFrame())
            _selectedItem++;
        if (_selectedItem != 0 &&
            InputSystem.actions.FindAction(_previous).WasPressedThisFrame())
            _selectedItem--;

        
    }

    private void AddToInventory(Item item)
    {
        if (_items.Count - _items.Count != 0)
        {
            _items.Append(item);
            WasGrabbed = true;
        }
        else WasGrabbed = false;
    }

    private void RemoveFromInventory(int itemID)
    {
        Instantiate(_items[itemID],transform);
        _items.Remove(_items[itemID]);
    }
}
