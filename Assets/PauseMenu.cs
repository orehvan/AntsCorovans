using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    [SerializeField] private GameObject pauseMenuUI;
    
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (gamePaused)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
