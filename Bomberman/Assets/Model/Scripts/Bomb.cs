using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject wallPrefab;
    public GameObject boxPrefab;

    public LayerMask explosionLayerMask;
    public GameObject explosionPrefab;
    public GameObject groundPrefab;
    public KeyCode inputKey;
    public float bombDropDelay = 3f;
    private int explosionradius;
    public bool IsAvailable { get; private set;}
    private bool IsDetonatable = false;
    private bool IsStartExplode = false;
    public GameObject[] extras;
    private void Update()
    {
        if(IsDetonatable) StartCoroutine(DetonatorExplode());
        else StartCoroutine(Ex());

    }


    private IEnumerator Ex()
    {

        IsAvailable = false;
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        
        
        
        yield return new WaitForSeconds(bombDropDelay);

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        Destroy(explosion, 1f);

        ExplodeAll(position, explosionradius);
        IsAvailable = true;
        Destroy(gameObject);
        

    }
    private IEnumerator DetonatorExplode()
    {

        IsAvailable = false;
        yield return new WaitUntil(() => IsStartExplode);
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        Destroy(explosion, 1f);

        ExplodeAll(position, explosionradius);
        IsAvailable = true;
        Destroy(gameObject);
        

    }

    public void SetExplosionradius(int length)
    {
        explosionradius = length;
    }
    
    public void SetDetonatable(bool _IsDetonatable)
    {
        IsDetonatable = _IsDetonatable;
    }
    
    public void SetStartExplode(bool _IsStartExplode)
    {
        IsStartExplode = _IsStartExplode;
    }

    public void ExplodeAll(Vector2 position, int explosionRadius)
    {
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);

    }

    public void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }


        Vector2 explosionPosition = position += direction;

        if (Physics2D.OverlapBox(explosionPosition, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            Collider2D collider = Physics2D.OverlapPoint(explosionPosition);

            if (collider != null && collider.gameObject.CompareTag("Monster"))
            {
                GameObject monsterObject = collider.gameObject;
                Destroy(monsterObject);
                GameObject expl = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                Destroy(expl, 1f);
            }
            if(collider != null && collider.gameObject.CompareTag("fakeBox"))
            {
                GameObject fakeBoxObject = collider.gameObject;
                Destroy(fakeBoxObject);
                GetComponent<FakeBoxCrontroller>().AddFakeBox();
                GameObject expl = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                Destroy(expl, 1f);
            }
            if (collider != null && collider.gameObject.CompareTag(boxPrefab.tag))
            {


                GameObject wallObject = collider.gameObject;
                Destroy(wallObject);
                Instantiate(groundPrefab, explosionPosition, Quaternion.identity);
                GameObject expl = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                Destroy(expl, 1f);
                int random = Random.Range(0, extras.Length);
                Instantiate(extras[random], explosionPosition, Quaternion.identity);



            }
            return;
        }

        GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        Destroy(explosion, 1f);
        Explode(explosionPosition, direction, length - 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Explosion"))
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

            Destroy(explosion, 1f);

            ExplodeAll(position, explosionradius);
            
            Destroy(gameObject);
            IsAvailable = true;
        }

    }


}
