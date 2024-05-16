using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class FakeBoxControllerTest
{
    private GameObject FakeBoxControllerObject;
    private FakeBoxCrontroller FakeBoxCrontrollerScript;

    [SetUp]
    public void SetUp()
    {
        FakeBoxControllerObject = new GameObject();
        FakeBoxCrontrollerScript = FakeBoxControllerObject.AddComponent<FakeBoxCrontroller>();
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(FakeBoxControllerObject);
    }

    [Test]
    public void PickUpFakeBoxTest()
    {
        FakeBoxCrontrollerScript.remainingFakeBox = 0;
        FakeBoxCrontrollerScript.PickUpFakeBox();
        Assert.AreEqual(3,FakeBoxCrontrollerScript.remainingFakeBox);

    }

    [Test]
    public void AddFakeBoxTest()
    {
        FakeBoxCrontrollerScript.remainingFakeBox = 0;
        FakeBoxCrontrollerScript.AddFakeBox();
        Assert.AreEqual(1,FakeBoxCrontrollerScript.remainingFakeBox);


    }

    [UnityTest]
        public IEnumerator TestPlaceFakeBox()
        {
            
           
            var fakeBoxPrefab = new GameObject();
            FakeBoxCrontrollerScript.fakeBoxPrefab = fakeBoxPrefab;

           
            FakeBoxCrontrollerScript.PlaceFakeBox();

            
            yield return null;

           
            var createdFakeBoxes = GameObject.FindObjectsOfType<GameObject>();
            
            var createdFakeBoxPosition = createdFakeBoxes[0].transform.position;
            Assert.AreEqual(Mathf.RoundToInt(FakeBoxCrontrollerScript.transform.position.x), Mathf.RoundToInt(createdFakeBoxPosition.x));
            Assert.AreEqual(Mathf.RoundToInt(FakeBoxCrontrollerScript.transform.position.y), Mathf.RoundToInt(createdFakeBoxPosition.y));
        }
}
