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

    private void Update()
    {
        if (_inventory._Items[_slotID])
            _image.sprite = _inventory._Items[_slotID].GetComponent<SpriteRenderer>().sprite;
        else _image.sprite = null;
    }
}
