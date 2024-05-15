using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
