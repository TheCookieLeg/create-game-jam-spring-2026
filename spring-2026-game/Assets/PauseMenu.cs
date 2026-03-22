using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public InputAction pause;
    // Update is called once per frame
    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pause.WasPressedThisFrame())
        {
            Debug.Log("help");
            if (GameIsPaused)
            {
                ContinueGame();
            }
            else
            {
                Pause();
            }
        } 
    }

    public void ContinueGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
