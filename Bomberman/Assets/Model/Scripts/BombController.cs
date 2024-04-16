using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Bomb bombPrefab;
    
    public KeyCode inputKey;
 
    public int bombAmount = 1;
    public int bombsRemaining = 1;
    public bool _IsAnyBombRemaining { get; private set;}
   
    public bool IsDetonatable = false;


    
    public int explosionRadius; 

    public void IncreaseRadius()
    {
        explosionRadius++;
    }
    public void AddBomb()
    {
        bombsRemaining++;
    }

    public void SetDetonatable()
    {
        IsDetonatable = true;
    }

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if(!IsDetonatable)
        {
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) 
            {
                StartCoroutine(PlaceBomb());
            }
        }
        else
        {
            if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) 
            {
                StartCoroutine(PlaceDetonatorBomb());
            }
            else if(bombsRemaining == 0 && Input.GetKeyDown(inputKey))
            {
                if(Input.GetKeyDown(inputKey))
                    _IsAnyBombRemaining = true;
            }
            
            
        }
       
        

    }

    private IEnumerator PlaceDetonatorBomb()
    {
        bombsRemaining--;

        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        
        Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bomb.SetExplosionradius(explosionRadius);
        bomb.SetDetonatable(true);
        
        IsAnyBombRemaining();
        yield return new WaitUntil(() => _IsAnyBombRemaining);
        
        bomb.SetStartExplode(true);
        
    
        yield return new WaitUntil(() => bomb.IsAvailable);
        bombsRemaining++;
    }

    private void IsAnyBombRemaining()
    {
        if(bombsRemaining == 0) _IsAnyBombRemaining = true;
        _IsAnyBombRemaining = false;
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
