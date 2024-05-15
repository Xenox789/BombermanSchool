using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        string directory = Application.streamingAssetsPath + "/Levels/";

        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
            if (Path.GetExtension(file) == ".bml")
                dropdown.options.Add(new TMP_Dropdown.OptionData(Path.GetFileName(file)));
        }

        dropdown.RefreshShownValue();
    }

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

    public void SetLevelToLoad()
    {
        if (dropdown.value > 0)
        {
            GameManager.Instance.loadFileName = dropdown.options[dropdown.value].text;
        }
    }
}
