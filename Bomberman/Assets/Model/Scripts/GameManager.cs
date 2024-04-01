using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalRounds = 3; 
    private int[] playerScores;
    public GameObject[] players;
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
        playerScores = new int[2]; 
        playerScores[0] = 0;
        playerScores[1] = 0;
    }


    public void CheckWin()
    {
        
        int alivePlayer = 0;
        foreach (GameObject player in players)
        {
            if(player.activeSelf) alivePlayer++;
        }
        if (alivePlayer <= 1)
        {
            Invoke(nameof(NewRound), 1.5f);
        }

    }
    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayerWins()
    {

        if(players[1].activeSelf && !players[0].activeSelf)
            playerScores[0]++;
        else if(players[0].activeSelf && !players[1].activeSelf)
            playerScores[1]++;

        CheckGameEnd();
    }

    void CheckGameEnd()
    {
        foreach (int score in playerScores)
        {
            if (score == totalRounds)
            {
                
                Debug.Log("Game Over. Player " + (score == playerScores[0] ? "1" : "2") + " wins!");
                RestartGame();
                return;
            }
        }

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RestartGame()
    {
        
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject); 
    }

    
}
