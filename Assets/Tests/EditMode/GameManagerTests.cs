using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManagerTests
{


    [Test]
    public void GameManager_CheckWin_Calls_NewRound()
    {
        GameObject gameManagerObject = new GameObject();
        GameManager gameManager = gameManagerObject.AddComponent<GameManager>();

        gameManager.totalRounds = 3; 
        gameManager.CheckAlive = false; 

        gameManager.CheckWin();

        Assert.IsTrue(gameManager.CheckAlive);

        Object.DestroyImmediate(gameManagerObject);
    }



}
