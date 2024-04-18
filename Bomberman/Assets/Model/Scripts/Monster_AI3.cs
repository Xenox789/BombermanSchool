using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monszert : MonoBehaviour
{

    public float speed = 1.0f;
    private Rigidbody2D rb;
    public Vector2 movementDirection;
    public List<Vector2> avDirections;
    private LayerMask obstacleLayer;

    public MovementSpriteRenderer spriteRendererUp;
    public MovementSpriteRenderer spriteRendererDown;
    public MovementSpriteRenderer spriteRendererLeft;
    public MovementSpriteRenderer spriteRendererRight;
    private MovementSpriteRenderer activeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Box", "OuterWall");
        activeSpriteRenderer = spriteRendererDown;
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        AvailableDirections();
        if (avDirections.Count == 0)
        {
            movementDirection = Vector2.zero;
        }
        else
        {
            ChangeDirections();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        AvailableDirections();
        if (avDirections.Count >= 3 && Random.Range(0, 100) < 5)
        {
            ChangeDirections();
        }
        if (avDirections.Count == 0)
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        
        }
        else if (avDirections.Count >= 1 && movementDirection == Vector2.zero)
        {
            ChangeDirections();
        }
        MoveMonster();
    }


    private void ChangeDirections()
    {
        
        int choice = Random.Range(0, avDirections.Count);
        Vector2 direction = avDirections[choice];
        if(direction == Vector2.up)
        {
            SetDirection(direction, spriteRendererUp);
        } else if (direction == Vector2.down)
        {
            SetDirection(direction, spriteRendererDown);
        }
        else if (direction == Vector2.left)
        {
            SetDirection(direction, spriteRendererLeft);
        }
        else if (direction == Vector2.right)
        {
            SetDirection(direction, spriteRendererRight);
        }
    }

    private void SetDirection(Vector2 newDirection, MovementSpriteRenderer spriteRenderer)
    {
        movementDirection = newDirection;
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = movementDirection == Vector2.zero;
    }
    private void MoveMonster()
    {
        Vector2 targetPosition = new Vector2(Mathf.Round(transform.position.x + movementDirection.x), Mathf.Round(transform.position.y + movementDirection.y));
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void AvailableDirections()
    {
        avDirections.Clear();
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> result = new List<Vector2>();
        foreach (var direction in directions)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, direction, CalcNextBlock(direction), LayerMask.GetMask("Box", "OuterWall"));
            if (hit.collider == null)
            {
                result.Add(direction);
            }
           
        }

        avDirections = result;
    }

    float CalcNextBlock(Vector2 direction)
    {
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;
        return Vector2.Distance(transform.position, new Vector2(x, y));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            Destroy(gameObject);

        }
        else if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            AvailableDirections();
            if(avDirections.Count >= 1)
            {
                ChangeDirections();
            }
            
        }
    }
}
