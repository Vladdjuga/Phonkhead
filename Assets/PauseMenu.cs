using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool is_paused = false;
    public GameObject pauseMenuUI;
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (is_paused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        is_paused = false;
    }
    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        is_paused = true;
    }
    public void quit()
    {
        Application.Quit();
    }
}
