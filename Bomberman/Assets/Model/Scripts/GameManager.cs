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
    

    public int totalRounds = 3; 
    public int[] playerScores;
    public GameObject[] players;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    private bool CheckAlive;

    void Start()
    {
        playerScores = new int[2];
        players = new GameObject[2];
        playerScores[0] = 0;
        playerScores[1] = 0;
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
        bool output = EditorUtility.DisplayDialog("Game Over","Game Over. Player " + playerIndex + " wins!","ok");
        if(output) RestartGame();
    }
    void RestartGame()
    {
        
        SceneManager.LoadScene("Menu");
        Destroy(gameObject); 
    }


}
