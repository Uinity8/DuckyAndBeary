using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    
    private bool isPaused = false;
    public bool IsPaused  => isPaused;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 씬 전환 기능
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        //Time.timeScale = isPaused ? 0 : 1;
    }

    // 게임 종료 기능
    public void QuitGame()
    {
        Application.Quit();
    }
}