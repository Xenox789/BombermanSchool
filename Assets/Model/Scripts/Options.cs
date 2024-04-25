using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel1()
    {
        GameManager.Instance.loadFileName = "level1.bml";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel2()
    {
        GameManager.Instance.loadFileName = "level2.bml";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel3()
    {
        GameManager.Instance.loadFileName = "level3.bml";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
