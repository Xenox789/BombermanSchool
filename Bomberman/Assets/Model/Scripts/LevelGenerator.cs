using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject boxPrefab;
    public GameObject grassPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public int width = 15;
    public int height = 10;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                Instantiate(grassPrefab, new Vector2(x, y), Quaternion.identity, transform);
                if (x == 1 && y == height / 2)
                {
                    Vector2 leftSpawnPosition = new Vector2(x, y);
                    Instantiate(player2Prefab, leftSpawnPosition, Quaternion.identity);
                }
                else if (x == width - 2 && y == height / 2)
                {
                    Vector2 rightSpawnPosition = new Vector2(x, y);
                    Instantiate(player1Prefab, rightSpawnPosition, Quaternion.identity);
                }
                // Falak a szélén
                else if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    Instantiate(wallPrefab, new Vector2(x, y), Quaternion.identity);
                }
                // Falak minden második mez?n belül
                else if (x % 2 == 0 && y % 2 == 0)
                {
                    Instantiate(wallPrefab, new Vector2(x, y), Quaternion.identity);
                }
                // Dobozok helyezése a falak közötti részeken
                else
                {
                    // Doboz vagy üres mez? véletlenszer? elhelyezése
                    if (Random.Range(0, 2) == 0) // 50% esély
                    {
                        Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }
        }
    }
}
