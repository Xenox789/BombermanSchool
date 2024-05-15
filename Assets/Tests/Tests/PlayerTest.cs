using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor.SceneManagement;


[TestFixture]
public class PlayerTest
{
    private GameObject playerGameObject;
    private Player playerScript;

    [SetUp]
    public void SetUp()
    {
        playerGameObject = new GameObject();
        playerScript = playerGameObject.AddComponent<Player>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGameObject);
    }

    [Test]
    public void PlayerSetDirection()
    {                       
        playerScript.movementDirection = Vector2.up;
        Assert.AreEqual(Vector2.up, playerScript.movementDirection);
        playerScript.movementDirection = Vector2.down;
        Assert.AreEqual(Vector2.down, playerScript.movementDirection);
        playerScript.movementDirection = Vector2.left;
        Assert.AreEqual(Vector2.left, playerScript.movementDirection);
        playerScript.movementDirection = Vector2.right;
        Assert.AreEqual(Vector2.right, playerScript.movementDirection);

    }

    [Test]
    public void SetInviolableTest()
    {
        playerScript.SetInviolable();
        Assert.IsFalse(playerScript.isInviolable);
    }

    [Test]
    public void TestPlayerSpeedIncrease()
    {
        
        float initialSpeed = playerScript.moveSpeed;
        playerScript.IncreaseSpeed();
        Assert.Greater(playerScript.moveSpeed, initialSpeed);
        playerScript.ActivateDecreaseSpeed();
        Assert.Less(playerScript.moveSpeed,initialSpeed);

    }

    [Test]
    public void Death()
    {
        playerScript.Death();
        Assert.IsFalse(playerGameObject.activeSelf);
    }




}
