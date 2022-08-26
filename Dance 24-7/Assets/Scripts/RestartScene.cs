using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] private KeyCode keyRestart;

    // For testing purposes, when corresponding key is pressed, game is either reset or quit
    void Update()
    {
        if (Input.GetKey(keyRestart))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
