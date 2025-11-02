using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public Transform getDestionation()
    {
        return destination;
    }


}
