using UnityEngine;
using UnityEngine.SceneManagement;
public class Quit : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
    {
        SceneManager.LoadScene(_sceneName);
    }
    }
}
