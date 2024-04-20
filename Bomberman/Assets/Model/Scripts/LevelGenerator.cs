using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using UnityEditor;

public class LevelGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject outerWallPrefab;
    public GameObject boxPrefab;
    public GameObject grassPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject monster1Prefab;
    public GameObject flyingMonsterPrefab;
    public GameObject smartMonsterPrefab;
    public GameObject notSoSmartMonsterPrefab;

    public int width = 15;
    public int height = 11;

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        GenerateLevel();
        //LoadLevel();

        // SaveLevel();
    }

    public void GenerateLevel()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("MainCamera") || obj.CompareTag("GameController"))
                continue;
            Destroy(obj);
        }
        tileObjects = new List<GameObject>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                GameObject tileObject = Instantiate(grassPrefab, new Vector2(x, y), Quaternion.identity, transform);
                tileObjects.Add(tileObject);
                if (x == 1 && y == height - 2)
                {
                    Vector2 leftSpawnPosition = new Vector2(x, y);
                    tileObject = Instantiate(player2Prefab, leftSpawnPosition, Quaternion.identity);
                    tileObjects.Add(tileObject);
                    GameManager.Instance.players[1] = tileObject;
                }
                else if(x == 1 && y == 1)
                {
                    Instantiate(monster1Prefab, new Vector3(x,y), Quaternion.identity);
                }
                else if (x == width - 2 && y == height-2)
                {
                    Instantiate(smartMonsterPrefab, new Vector3(x, y), Quaternion.identity);
                }
                else if (x == width /2 && y == height - 2)
                {
                    Instantiate(flyingMonsterPrefab, new Vector3(x, y), Quaternion.identity);
                }
                else if (x == width / 2 && y == 1)
                {
                    Instantiate(notSoSmartMonsterPrefab, new Vector3(x, y), Quaternion.identity);
                }
                else if (x == width - 2 && y == 1)
                {
                    Vector2 rightSpawnPosition = new Vector2(x, y);
                    tileObject = Instantiate(player1Prefab, rightSpawnPosition, Quaternion.identity);
                    tileObjects.Add(tileObject);
                    GameManager.Instance.players[0] = tileObject;
                }
                // Falak a sz�l�n
                else if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    tileObject = Instantiate(outerWallPrefab, new Vector2(x, y), Quaternion.identity);
                    tileObjects.Add(tileObject);
                }
                // Falak minden m�sodik mez?n bel�l
                else if (x % 2 == 0 && y % 2 == 0)
                {
                    tileObject = Instantiate(wallPrefab, new Vector2(x, y), Quaternion.identity);
                    tileObjects.Add(tileObject);
                }
                // Dobozok helyez�se a falak k�z�tti r�szeken
                else
                {
                    // Doboz vagy üres mez? véletlenszer? elhelyezése
                    if (Random.Range(0, 100) < 30) // 30% esély
                    {
                        tileObject = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                        tileObjects.Add(tileObject);
                    }
                }
            }
        }
    }

    // List to store the gameobjects (prefabs)
    [SerializeField] private List<GameObject> tileObjects;

    // bml = bomberman level
    [SerializeField] private string saveFileName = "level1.bml";
    [SerializeField] private string loadFileName = "level2.bml";

    public void SaveLevel()
    {
        string saveFilePath = Application.streamingAssetsPath + "/Levels/" + saveFileName;

        Directory.CreateDirectory(Application.streamingAssetsPath + "/Levels/");

        if (File.Exists(saveFilePath))
            return;

        List<string> saveData = new List<string>();

        foreach (var tileObject in tileObjects)
        {
            saveData.Add(tileObject.name + ";" + tileObject.transform.position.ToString());
            Debug.Log("Added " + tileObject.name + " gameObject at " + tileObject.transform.position.ToString() + " position to save data.");
        }

        File.WriteAllLines(saveFilePath, saveData);
    }

    public void LoadLevel()
    {
        string loadFilePath = Application.streamingAssetsPath + "/Levels/" + loadFileName;

        StreamReader sr = new StreamReader(loadFilePath);

        tileObjects = new List<GameObject>();

        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(';');

            GameObject tileObject;

            switch (line[0])
            {
                case "Box(Clone)":
                    tileObject = Instantiate(boxPrefab, StringToVector3(line[1]), Quaternion.identity);
                    break;
                case "Grass(Clone)":
                    tileObject = Instantiate(grassPrefab, StringToVector3(line[1]), Quaternion.identity);
                    break;
                case "Player1(Clone)":
                    tileObject = Instantiate(player1Prefab, StringToVector3(line[1]), Quaternion.identity);
                    // Player.Create1(StringToVector3(line[1]));
                    break;
                case "Player2(Clone)":
                    tileObject = Instantiate(player2Prefab, StringToVector3(line[1]), Quaternion.identity);
                    break;
                case "Wall(Clone)":
                    tileObject = Instantiate(wallPrefab, StringToVector3(line[1]), Quaternion.identity);
                    break;
                default:
                    tileObject = new GameObject();
                    Debug.Log("Invalid line: " + line.ToString());
                    break;
            }

            tileObjects.Add(tileObject);
        }

        sr.Close();
    }

    /// <summary>
    /// String to Vector3 converter method.
    /// </summary>
    /// <param name="position">Input string, format must be like this: (1.00, 0.00, 0.00)</param>
    /// <returns>The converted Vector3.</returns>
    private Vector3 StringToVector3(string position)
    {
        // remove '(' and ')'
        position = position.Substring(1, position.Length - 2);

        string[] coordinates = position.Split(", ");

        Vector3 vector = new Vector3(float.Parse(coordinates[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(coordinates[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(coordinates[2], CultureInfo.InvariantCulture.NumberFormat));
        return vector;
    }
}
