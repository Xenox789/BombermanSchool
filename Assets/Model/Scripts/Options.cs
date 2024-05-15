using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown levelDropdown;
    [SerializeField] private TMP_Dropdown roundsDropdown;

    private void Awake()
    {
        string directory = Application.streamingAssetsPath + "/Levels/";

        string[] files = Directory.GetFiles(directory);

        levelDropdown.ClearOptions();

        foreach (string file in files)
        {
            if (Path.GetExtension(file) == ".bml")
                levelDropdown.options.Add(new TMP_Dropdown.OptionData(Path.GetFileNameWithoutExtension(file)));
        }

        levelDropdown.RefreshShownValue();
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
        if (levelDropdown.value > 0)
            GameManager.Instance.loadFileName = levelDropdown.options[levelDropdown.value].text + ".bml";
    }

    public void SetRounds()
    {
        GameManager.Instance.totalRounds = int.Parse(roundsDropdown.options[roundsDropdown.value].text);
    }
}
