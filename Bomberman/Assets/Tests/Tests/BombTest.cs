using UnityEngine;
using NUnit.Framework;

[TestFixture]
public class BombTests
{
    [Test]
    public void Bomb_ExplodesProperlyAfterPlacement()
    {
    
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();
        bomb.explosionPrefab = new GameObject(); 
        bomb.SetExplosionradius(1); 

        
        bomb.Update(); 
        Assert.IsTrue(!bomb.IsAvailable); 
    }

     [Test]
    public void Bomb_ExplodesObjectsAroundProperly()
    {
        // Arrange
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();
        bomb.explosionPrefab = new GameObject();
        bomb.SetExplosionradius(1); 
        Vector3 position = bomb.transform.position; 
        
        
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.tag = "Wall";
        wall.transform.position = new Vector3(position.x, position.y + 1, 0);
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.tag = "Box";
        box.transform.position = new Vector3(position.x + 1, position.y + 1, 0); 

       
        bomb.Update(); 
        Collider[] colliders = Physics.OverlapSphere(bomb.transform.position, bomb.GetExplosionRadius());
        bool anyObjectLeft = true;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall") || collider.CompareTag("Box"))
            {
                anyObjectLeft = false;
                break;
            }
        }
        Assert.IsFalse(anyObjectLeft, "The bomb did not explode objects properly.");
    }

     [Test]
    public void DetonatableBomb_DetonatesProperly()
    {
        // Arrange
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();
        bomb.explosionPrefab = new GameObject(); 
         bomb.SetExplosionradius(1); 
        bomb.SetDetonatable(true); // Beállítjuk a bombát detonálhatóvá

      
        bomb.SetStartExplode(true); 
        bomb.Update(); 

        
        Collider[] colliders = Physics.OverlapSphere(bomb.transform.position, bomb.GetExplosionRadius());
        bool anyObjectLeft = true;
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall") || collider.CompareTag("Box"))
            {
                anyObjectLeft = false;
                break;
            }
        }
        Assert.IsFalse(anyObjectLeft, "The bomb did not detonate properly.");
    }

    [Test]
    public void Bomb_ExplosionRadiusIsCorrect()
    {
        
        GameObject bombObject = new GameObject();
        Bomb bomb = bombObject.AddComponent<Bomb>();
        bomb.explosionPrefab = new GameObject(); 
        int expectedExplosionRadius = 2; 

        
        bomb.SetExplosionradius(expectedExplosionRadius); 
        int actualExplosionRadius = bomb.GetExplosionRadius(); 
        
        Assert.AreEqual(expectedExplosionRadius, actualExplosionRadius, "The explosion radius is not correct.");
    }
     
}
