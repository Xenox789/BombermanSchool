using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void SaveGame()
    {
        FindObjectOfType<LevelGenerator>().SaveLevel();
    }

    public void LoadGame()
    {
        // LevelGenerator lg = FindObjectOfType<LevelGenerator>();
        // DontDestroyOnLoad(lg);
        // lg.LoadLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
