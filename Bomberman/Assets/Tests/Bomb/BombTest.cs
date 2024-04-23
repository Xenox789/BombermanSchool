using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

public class BombTest
{
    [UnityTest]
    public IEnumerator BombDetonatesCorrectly()
    {
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();

        
        bomb.SetExplosionradius(1);
        bomb.explosionPrefab = new GameObject(); 

        
        bomb.SetDetonatable(true);

        
        bomb.SetStartExplode(true);

        
        yield return new WaitForSeconds(1.5f); 

        
        Assert.IsNull(bombObject); 
    }

    [Test]
    public void BombExplodesOnCollisionWithExplosion()
    {
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();

        bomb.SetExplosionradius(1);
        bomb.explosionPrefab = new GameObject(); 
        // Create a dummy explosion object
        GameObject explosionObject = new GameObject();
        explosionObject.tag = "Explosion";

        
        //bomb.OnCollisionEnter2D(explosionObject.AddComponent<Collision2D>());

        
        Assert.IsNull(bombObject);
}

    [Test]
    public void BombSetsAvailableAfterExplode()
    {
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();

       
        bomb.SetExplosionradius(1);
        bomb.explosionPrefab = new GameObject(); 

       
        bomb.SetDetonatable(true);
        bomb.SetStartExplode(true);

       
        Assert.IsTrue(bomb.IsAvailable); 
    }
}
