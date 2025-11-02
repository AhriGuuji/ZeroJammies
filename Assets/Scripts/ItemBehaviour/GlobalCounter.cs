using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalCounter : Interactable
{
    [field: SerializeField]
    public int TaskCounter { get; private set; }

    protected void CounterUp()
    {
        TaskCounter++;
        Debug.Log("Global item count: " + TaskCounter);
    }

    protected void ResetCounter()
    {
        TaskCounter = 0;
    }
}
