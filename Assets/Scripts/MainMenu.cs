using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
