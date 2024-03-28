using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalRounds = 3; // H�ny k�rig tart a j�t�k
    private int[] playerScores;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeGame()
    {
        // J�t�kos pontsz�mok inicializ�l�sa
        playerScores = new int[2]; // Ha a j�t�kot kib?v�tj�k 3 j�t�kosra, itt kell m�dos�tani
    }

    public void PlayerWins(int playerIndex)
    {
        playerScores[playerIndex]++;
        CheckGameEnd();
    }

    void CheckGameEnd()
    {
        foreach (int score in playerScores)
        {
            if (score >= totalRounds)
            {
                // J�t�k v�ge, eredm�nyek megjelen�t�se
                Debug.Log("Game Over. Player " + (score == playerScores[0] ? "1" : "2") + " wins!");
                RestartGame();
                return;
            }
        }

        // �j k�r ind�t�sa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RestartGame()
    {
        // J�t�k �jraind�t�sa az alap�rtelmezett �llapothoz
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject); // GameManager t�rl�se, hogy �jra inicializ�l�djon
    }

    // Egy�b funkci�k, mint pontsz�mok friss�t�se, j�t�k pause �s folytat�sa
}
