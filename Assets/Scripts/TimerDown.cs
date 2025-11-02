using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerDown : MonoBehaviour
{
    [SerializeField]
     private string _sceneName;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(remainingTime <= 0)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
