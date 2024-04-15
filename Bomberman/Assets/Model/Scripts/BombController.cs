using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Bomb bombPrefab;
    
    public KeyCode inputKey;
 
    public int bombAmount = 1;
    public int bombsRemaining = 1;
    
   



    
    public int explosionRadius; 

    public void IncreaseRadius()
    {
        explosionRadius++;
    }
    public void AddBomb()
    {
        bombsRemaining++;
    }

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
        

    }


    private IEnumerator PlaceBomb()
    {

        bombsRemaining--;

        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        
        Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bomb.SetExplosionradius(explosionRadius);
        yield return new WaitUntil(() => bomb.IsAvailable);
        bombsRemaining++;
       
        
    }
    
   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }

    }
    

    
}
