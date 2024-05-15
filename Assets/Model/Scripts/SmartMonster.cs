using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMonster : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;
    public Vector2 movementDirection;
    public List<Vector2> avDirections = new List<Vector2>();
    private LayerMask obstacleLayer;

    public MovementSpriteRenderer spriteRendererUp;
    public MovementSpriteRenderer spriteRendererDown;
    public MovementSpriteRenderer spriteRendererLeft;
    public MovementSpriteRenderer spriteRendererRight;
    public MovementSpriteRenderer activeSpriteRenderer;

    public Vector2 player1Coords;
    public Vector2 player2Coords;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Box", "OuterWall", "Bomb");
        
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

    void Update()
    {
        
        AvailableDirections();
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


    private void UpdatePlayerCoords()
    {
        player1Coords = GameManager.Instance.players[0].transform.position;
        player2Coords = GameManager.Instance.players[1].transform.position;
        
    }

    public void CalcBestDirection()
    {
        UpdatePlayerCoords();
        AvailableDirections();
        float p1min_dis = 100;
        float p2min_dis = 100;
        Vector2 p1min_dir = Vector2.zero;
        Vector2 p2min_dir = Vector2.zero;

        foreach (Vector2 dir in avDirections)
        {
            float p1dis = Vector2.Distance((Vector2)transform.position + dir, player1Coords);
            float p2dis = Vector2.Distance((Vector2)transform.position + dir, player2Coords);
            if (p1min_dis > p1dis)
            {
                p1min_dis = p1dis;
                p1min_dir = dir;
            }
            if (p2min_dis > p2dis)
            {
                p2min_dir = dir;
                p2min_dis = p2dis;
            }  
        }

        if(p1min_dis < p2min_dis)
        {
            avDirections.Clear();
            avDirections.Add(p1min_dir);
        }
        else if(p1min_dis > p2min_dis)
        {
            avDirections.Clear();
            avDirections.Add(p2min_dir);
        }
        else if(p1min_dis == p2min_dis)
        {
            avDirections.Clear();
            avDirections.Add(p1min_dir);
            avDirections.Add(p2min_dir);
        }
    }

    public void ChangeDirections()
    {
        CalcBestDirection();
        int choice = Random.Range(0, avDirections.Count);
        Vector2 direction = avDirections[choice];
        if (direction == Vector2.up)
        {
            SetDirection(direction, spriteRendererUp);
        }
        else if (direction == Vector2.down)
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
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, direction, CalcNextBlock(direction), obstacleLayer);
            if (hit.collider == null)
            {
                result.Add(direction);
            }

        }

        avDirections = result;
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
            if (avDirections.Count >= 1)
            {
                ChangeDirections();
            }

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

    private float CalcNextBlock(Vector2 direction)
    {
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;
        return Vector2.Distance(transform.position, new Vector2(x, y));
    }

}
