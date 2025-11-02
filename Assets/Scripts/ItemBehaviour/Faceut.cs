using UnityEngine;

public class Faceut : Interactable
{
    [SerializeField]
    private Sprite _sprite;
    private MemoryTimer memoryTimer;
    protected override void Awake()
    {
        base.Awake();

        memoryTimer = FindAnyObjectByType<MemoryTimer>();
    }
    protected override void GameEvent()
    {
        Debug.Log("Xerz");
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = _sprite;
        memoryTimer.resetTimer();

    }
}
