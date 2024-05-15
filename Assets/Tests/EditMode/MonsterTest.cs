using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class MonsterTest
{
    private GameObject MonsterObject;
    private FlyingMonsterScript monsterScript;

    [SetUp]
    public void SetUp()
    {
        MonsterObject = new GameObject();
        monsterScript = MonsterObject.AddComponent<FlyingMonsterScript>();
    }
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(MonsterObject);
    }
    [Test]
    public void CalcNextBlockTest()
    {
        MonsterObject.transform.position = new Vector2(1f,2f);
        float result = monsterScript.CalcNextBlock(Vector2.right);
        float expected = 1f;
        Assert.AreEqual(expected, result);

    }

    
}
