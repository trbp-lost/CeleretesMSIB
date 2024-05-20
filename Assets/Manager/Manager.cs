using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static bool gameIsPaused = false;
    public bool canPause = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MoveToScene(string namaScene)
    {
        Scene sceneIni = SceneManager.GetActiveScene();

        if (sceneIni.name != namaScene) SceneManager.LoadScene(namaScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
