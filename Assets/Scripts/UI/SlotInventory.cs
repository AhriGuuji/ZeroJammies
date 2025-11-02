using System;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : MonoBehaviour
{
    [SerializeField]
    private int _slotID;
    public int SlotID { get => _slotID; }
    private Image _image;
    private Inventory _inventory;
    private void Awake()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        _image = GetComponent<Image>();
    }
//a kátia teve aki
    private void Update()
    {
        try
        {
            _image.enabled = true;
            _image.sprite = _inventory._Items[_slotID].GetComponent<SpriteRenderer>().sprite;
        }
        catch (ArgumentOutOfRangeException)
        {
            _image.enabled = false;
        }
    }
}
