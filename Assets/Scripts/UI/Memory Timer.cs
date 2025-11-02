using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTimer : MonoBehaviour
{

    [SerializeField]
     private string _sceneName;
    [SerializeField] 
    float initialTime;
    [SerializeField] 
    float remainingTime;
   /* protected override void Awake()
    {
        base.Awake();
        initialTime = remainingTime;
    }*/
     void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
    public void resetTimer()
    {
        remainingTime = initialTime;
    }
}
