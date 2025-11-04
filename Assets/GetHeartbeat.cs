using UnityEngine;

public class GetHeartbeat : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private string _beatAnimation = "StopBeat";

    private void Update()
    {
        _anim.SetTrigger(_beatAnimation);
    }
}
