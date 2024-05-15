using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string saveFileName;

    // MainMenu
    public void StartGame()
    {
        // SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
    }

    // PauseMenu
    public void QuitToMain()
    {
        Destroy(FindObjectOfType<Score>().gameObject);
        Destroy(FindObjectOfType<GameManager>().gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SaveLevel()
    {
        FindObjectOfType<LevelGenerator>().SaveLevel(GameManager.Instance.saveFileName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GetSaveFileName(string fileName)
    {
        saveFileName = fileName + ".bml";

        GameManager.Instance.saveFileName = saveFileName;
    }
}
