using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Vector2 lastDirection;
    private float changeDirectionTime = 5f;
    private float changeDirectionTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseRandomDirection();
    }

    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0 || IsFacingWall())
        {
            ChooseRandomDirection();
            changeDirectionTimer = changeDirectionTime;
        }
        MoveMonster();
    }

    void MoveMonster()
    {
        rb.velocity = movementDirection * speed;
    }

    void ChooseRandomDirection()
    {
        Vector2 newDirection = lastDirection;
        int attempts = 0;
        while (newDirection == lastDirection && attempts < 10)
        {
            int choice = Random.Range(0, 4);
            switch (choice)
            {
                case 0:
                    newDirection = Vector2.up;
                    break;
                case 1:
                    newDirection = Vector2.down;
                    break;
                case 2:
                    newDirection = Vector2.left;
                    break;
                case 3:
                    newDirection = Vector2.right;
                    break;
            }
            attempts++;
            
        }
        if (attempts >= 10)
        {
            movementDirection = lastDirection = -movementDirection;
        }
        else
        {
            lastDirection = movementDirection = newDirection;
        }
        


    }

    bool IsFacingWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size, 0f, movementDirection, 0.3f, LayerMask.GetMask("Box"));
        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}

