using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class GameOverTest
{
    
        [Test]
        public void TestCheckGameEnd_Player1Wins()
        {
            var gameObject = new GameObject();
            var gameManagerScript = gameObject.AddComponent<GameManager>();

            gameManagerScript.playerScores = new int[2] { 3, 1 };

            gameManagerScript.CheckGameEnd();

            Assert.AreEqual(3, gameManagerScript.playerScores[0]);
            Assert.AreEqual(1, gameManagerScript.playerScores[1]);
            Assert.IsFalse(gameManagerScript.CheckAlive);
        }

        [Test]
        public void TestCheckGameEnd_Player2Wins()
        {
            var gameObject = new GameObject();
            var gameManagerScript = gameObject.AddComponent<GameManager>();

            gameManagerScript.playerScores = new int[2] { 1, 3 };

            gameManagerScript.CheckGameEnd();

            Assert.AreEqual(1, gameManagerScript.playerScores[0]);
            Assert.AreEqual(3, gameManagerScript.playerScores[1]);
            Assert.IsFalse(gameManagerScript.CheckAlive);
        }


        


}
