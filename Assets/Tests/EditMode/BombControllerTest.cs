using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
[TestFixture]
public class BombControllerTest
{
    private GameObject playerGameObject;
    private BombController BombControllerScript;
    [SetUp]
    public void SetUp()
    {
        playerGameObject = new GameObject();
        BombControllerScript = playerGameObject.AddComponent<BombController>();
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGameObject);
    }
    [Test]
    public void AddBombTest()
    {
        BombControllerScript.bombsRemaining = 0;
        BombControllerScript.AddBomb();
        Assert.AreEqual(1,BombControllerScript.bombsRemaining);
       
    }
    [Test]
    public void SetDetonatableTest()
    {
        BombControllerScript.IsDetonatable = false;
        BombControllerScript.SetDetonatable();
        Assert.IsTrue(BombControllerScript.IsDetonatable);
    }
}
