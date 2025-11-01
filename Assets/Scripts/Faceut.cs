using UnityEngine;

public class Faceut : Interactable
{
    [SerializeField]
    private Sprite _sprite;
    protected override void Event()
    {
        Debug.Log("Xerz");
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }
}
