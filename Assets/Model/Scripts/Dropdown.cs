using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        // Get file paths from the directory
        string[] filePaths = Directory.GetFiles(Application.streamingAssetsPath + "/Levels/");

        // Parse file names from file paths
        List<string> fileNames = new List<string>();
        foreach (string filePath in filePaths)
        {
            fileNames.Add(Path.GetFileName(filePath));
        }

        // Populate dropdown options
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
