using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class ExtrasTest
{
    
        [Test]
        public void TestPickUpFakeBox()
        {
            var fakeBoxControllerGameObject = new GameObject();
            var fakeBoxController = fakeBoxControllerGameObject.AddComponent<FakeBoxCrontroller>();

           
            fakeBoxController.PickUpFakeBox();
            Assert.AreEqual(4, fakeBoxController.remainingFakeBox);
        }

        [Test]
        public void TestAddFakeBox()
        {
            var fakeBoxControllerGameObject = new GameObject();
            var fakeBoxController = fakeBoxControllerGameObject.AddComponent<FakeBoxCrontroller>();

           
            fakeBoxController.AddFakeBox();
            Assert.AreEqual(2, fakeBoxController.remainingFakeBox);
        }

        [UnityTest]
        public IEnumerator TestPlaceFakeBox()
        {
            var fakeBoxControllerGameObject = new GameObject();
            var fakeBoxController = fakeBoxControllerGameObject.AddComponent<FakeBoxCrontroller>();

           
            var fakeBoxPrefab = new GameObject();
            fakeBoxController.fakeBoxPrefab = fakeBoxPrefab;

           
            fakeBoxController.PlaceFakeBox();

            
            yield return null;

           
            var createdFakeBoxes = GameObject.FindObjectsOfType<GameObject>();
            
            var createdFakeBoxPosition = createdFakeBoxes[0].transform.position;
            Assert.AreEqual(Mathf.RoundToInt(fakeBoxController.transform.position.x), Mathf.RoundToInt(createdFakeBoxPosition.x));
            Assert.AreEqual(Mathf.RoundToInt(fakeBoxController.transform.position.y), Mathf.RoundToInt(createdFakeBoxPosition.y));
        }


}
