using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [field: SerializeField]
    public List<Item> _Items { get; private set; }
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
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<PlayerMovement>();
        _selectedItem = 0;
    }    
    private void Update()
    {
        Debug.Log(_selectedItem);

        if (InputSystem.actions.FindAction(_drop).WasPressedThisFrame())
            RemoveFromInventory();
        if (_selectedItem != _inventorySize &&
            InputSystem.actions.FindAction(_next).WasPressedThisFrame())
            _selectedItem++;
        if (_selectedItem != 0 &&
            InputSystem.actions.FindAction(_previous).WasPressedThisFrame())
            _selectedItem--;
    }

    public void AddToInventory(Item item)
    {
        Debug.Log("trying to add");

        if (_Items.Count < _inventorySize)
        {
            Debug.Log("it went");
            _Items.Add(item);
            WasGrabbed = true;
        }
        else WasGrabbed = false;
    }

    public void RemoveFromInventory()
    {
        Instantiate(_Items[_selectedItem].gameObject
            ,_player.transform.position,quaternion.identity);
        _Items.Remove(_Items[_selectedItem]);
    }
}
