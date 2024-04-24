using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Globalization;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalRounds = 3; 
    public int[] playerScores;
    public GameObject[] players;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;


    private bool CheckAlive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerScores = new int[2];
        players = new GameObject[2];
        playerScores[0] = 0;
        playerScores[1] = 0;

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene loaded: " + scene.name);
        // You can perform any necessary actions here
        DontDestroyOnLoad(gameObject);
    }


    public void CheckWin()
    {
        
        if(!CheckAlive)
        {
            CheckAlive = true;
            Invoke(nameof(NewRound), 3f);
        }
    }
    public void NewRound()
    {
        PlayerWins();
        
        FindObjectOfType<LevelGenerator>().GenerateLevel();
        
    }
    public void PlayerWins()
    {

        if(players[1].activeInHierarchy && !players[0].activeInHierarchy)
            playerScores[1]++;
        else if(players[0].activeSelf && !players[1].activeSelf)
            playerScores[0]++;

        player1ScoreText.text = ":" + playerScores[0];
        player2ScoreText.text = ":" + playerScores[1];

        CheckGameEnd();
    }

    void CheckGameEnd()
    {
        
        if(playerScores[0] == 3)
            MessageBox(1);
        else if(playerScores[1] == 3)
            MessageBox(2);
        CheckAlive = false;
                
        
        
        
    }

    public void MessageBox(int playerIndex)
    {
    #if UNITY_EDITOR
        bool output = EditorUtility.DisplayDialog("Game Over","Game Over. Player " + playerIndex + " wins!","ok");
        if(output)
        RestartGame();
    #endif
    }
    void RestartGame()
    {
        
        SceneManager.LoadScene("Menu");
        Destroy(gameObject); 
    }


}
