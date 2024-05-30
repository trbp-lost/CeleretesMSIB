using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static bool gameIsPaused = false;
    public GameObject pausePanel;
    public GameObject settingPanel;

    public bool canBePauseScene = true;

    private void Awake()
    {
        if (canBePauseScene)
        {
            pausePanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canBePauseScene)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;

        if (pausePanel != null) pausePanel.SetActive(gameIsPaused);

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        if (pausePanel != null) pausePanel.SetActive(gameIsPaused);
    }

    public void MoveToScene(string namaScene)
    {
        Scene sceneIni = SceneManager.GetActiveScene();

        if (sceneIni.name != namaScene) SceneManager.LoadScene(namaScene);
    }

    public void SettingUI(bool isSetting)
    {

        if (isSetting)
        {
            pausePanel.SetActive(false);
            settingPanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(true);
            settingPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
