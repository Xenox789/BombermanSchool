using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalRounds = 3; // Hány körig tart a játék
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
        // Játékos pontszámok inicializálása
        playerScores = new int[2]; // Ha a játékot kib?vítjük 3 játékosra, itt kell módosítani
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
                // Játék vége, eredmények megjelenítése
                Debug.Log("Game Over. Player " + (score == playerScores[0] ? "1" : "2") + " wins!");
                RestartGame();
                return;
            }
        }

        // Új kör indítása
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RestartGame()
    {
        // Játék újraindítása az alapértelmezett állapothoz
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject); // GameManager törlése, hogy újra inicializálódjon
    }

    // Egyéb funkciók, mint pontszámok frissítése, játék pause és folytatása
}
