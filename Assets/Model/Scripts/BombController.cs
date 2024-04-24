using System.Collections;
using TMPro;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Bomb bombPrefab;

    public KeyCode inputKey;

    public int bombAmount = 1;
    public int bombsRemaining = 1;
    public bool _IsAnyBombRemaining { get; private set; }

    public bool IsDetonatable = false;
    public bool IsPlaceBombUnable { get; private set; }
    public bool IsPlaceBombInstantly { get; private set; }

    public bool IsBombsRadiusChangeAble {get; private set;}
    public int explosionRadius;

    public void IncreaseRadius()
    {
        new WaitUntil(() => IsBombsRadiusChangeAble);
        explosionRadius++;
    }

    public void ActivateDecreaseRadius()
    {
        IsBombsRadiusChangeAble = false;
        int _explosionRadius = explosionRadius;
        explosionRadius = 1;
        StartCoroutine(DeactivateDecreaseRadius(_explosionRadius, 10f));

    }

    private IEnumerator DeactivateDecreaseRadius(int length, float delay)
    {
        yield return new WaitForSeconds(delay);
        explosionRadius = length;
        IsBombsRadiusChangeAble = true;
    }

    public void ActivateUnablePlaceBomb()
    {
        IsPlaceBombUnable = true;
        StartCoroutine(DeactivateUnablePlaceBomb(10f));
    }

    private IEnumerator DeactivateUnablePlaceBomb(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsPlaceBombUnable = false;
    }

    public void ActivatePlaceBombInstantly()
    {
        IsPlaceBombInstantly = true;
        StartCoroutine(DeactivatePlaceBombInstantly(10f));
    }

    private IEnumerator DeactivatePlaceBombInstantly(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsPlaceBombInstantly = false;
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
        IsPlaceBombUnable = false;
        IsPlaceBombInstantly = false;
        IsBombsRadiusChangeAble = true;
    }
    private void Update()
    {
        if (!IsPlaceBombUnable)
        {
            if (bombsRemaining > 0 && IsPlaceBombInstantly)
            {
                StartCoroutine(PlaceBomb());
            }
            else
            {
                if (!IsDetonatable)
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
                    else if (bombsRemaining == 0 && Input.GetKeyDown(inputKey))
                    {
                        if (Input.GetKeyDown(inputKey))
                            _IsAnyBombRemaining = true;
                    }


                }
            }
        }




    }

    private IEnumerator PlaceDetonatorBomb()
    {
        bombsRemaining--;

        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall") || collider.CompareTag("Box") || collider.CompareTag("fakeBox"))
            {
                bombsRemaining++;
                yield break;
            }
        }

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
        if (bombsRemaining == 0) _IsAnyBombRemaining = true;
        _IsAnyBombRemaining = false;
    }


    private IEnumerator PlaceBomb()
    {

        bombsRemaining--;


        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Wall") || collider.CompareTag("Box") || collider.CompareTag("fakeBox"))
            {
                bombsRemaining++;
                yield break;
            }
        }

        Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bomb.SetExplosionradius(explosionRadius);
        yield return new WaitUntil(() => bomb.IsAvailable);
        bombsRemaining++;


    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }

    }



}
