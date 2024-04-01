using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
     public GameObject bombPrefab;
    public GameObject wallPrefab;
    public GameObject boxPrefab;

    public LayerMask explosionLayerMask;
    public GameObject explosionPrefab;
    public GameObject groundPrefab;
    public KeyCode inputKey;
    public float bombDropDelay = 3f;
    public int bombAmount = 1;
    private int bombsRemaining = 1;
    

    
    public int explosionRadius; 



    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
        

    }


    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        
        bombsRemaining--;
        yield return new WaitForSeconds(bombDropDelay);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);


        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
       
        Destroy(explosion, 1f);
        
        
        Explode(position, Vector2.up,explosionRadius);
        Explode(position, Vector2.down,explosionRadius);
        Explode(position, Vector2.right,explosionRadius);
        Explode(position, Vector2.left,explosionRadius);


       
        Destroy(bomb);
        
        bombsRemaining++;
    }

   
   
    
    public void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }


        Vector2 explosionPosition = position += direction;

        if (Physics2D.OverlapBox(explosionPosition, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            Collider2D collider = Physics2D.OverlapPoint(explosionPosition);

            if (collider != null && collider.gameObject.CompareTag(boxPrefab.tag))
            {
                GameObject wallObject = collider.gameObject;
                Destroy(wallObject);
                Instantiate(groundPrefab, explosionPosition, Quaternion.identity);
                GameObject expl = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                Destroy(expl, 1f);
            }
            return;
        }

        GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        Destroy(explosion, 1f);
        Explode(explosionPosition,direction,length-1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }
}
