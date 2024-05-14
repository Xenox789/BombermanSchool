using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int playerNumber = 1;


    public Vector2 movementDirection;
    private Rigidbody2D rb;
    private bool IsPowerUpEnable = true;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public bool isAlive = true;
    public MovementSpriteRenderer spriteRendererUp;
    public MovementSpriteRenderer spriteRendererDown;
    public MovementSpriteRenderer spriteRendererLeft;
    public MovementSpriteRenderer spriteRendererRight;
    private MovementSpriteRenderer activeSpriteRenderer;
    public bool isInviolable;

   
    public bool isFlying = false;
    public bool isOnWallOrBox;
    public void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        isInviolable = false;
        isOnWallOrBox = false;
    }

    public void SetInviolable()
    {
        isInviolable = false;
        spriteRendererUp.isExpires = false;
        spriteRendererDown.isExpires = false;
        spriteRendererLeft.isExpires = false;
        spriteRendererRight.isExpires = false;
        activeSpriteRenderer.isExpires = false;
    }


    public void TimerExpires()
    {
        spriteRendererUp.isInviolable = false;
        spriteRendererDown.isInviolable = false;
        spriteRendererLeft.isInviolable = false;
        spriteRendererRight.isInviolable = false;
        activeSpriteRenderer.isInviolable = false;
        spriteRendererUp.isGhost = false;
        spriteRendererDown.isGhost = false;
        spriteRendererLeft.isGhost = false;
        spriteRendererRight.isGhost = false;
        activeSpriteRenderer.isGhost = false;
        spriteRendererUp.isExpires = true;
        spriteRendererDown.isExpires = true;
        spriteRendererLeft.isExpires = true;
        spriteRendererRight.isExpires = true;
        activeSpriteRenderer.isExpires = true;

    }

    public void Inviolable()
    {
        if (!isInviolable)
        {
            isInviolable = true;
            spriteRendererUp.isInviolable = true;
            spriteRendererDown.isInviolable = true;
            spriteRendererLeft.isInviolable = true;
            spriteRendererRight.isInviolable = true;
            activeSpriteRenderer.isInviolable = true;
            Invoke(nameof(TimerExpires), 4f);
            Invoke(nameof(SetInviolable),5f);
        }
    }

    public void Update()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }



    }
    
    void FixedUpdate()
    {

        if(isFlying)
            MovePlayer(movementDirection);
        else{ 
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
            
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isInviolable)
        {
            if (collision.gameObject.CompareTag("Explosion"))
            {
                Death();

            }
            else if (collision.gameObject.CompareTag("Monster"))
            {
                Death();

            }

        }
    
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Explosion"))
        {
            Death(); 
        }
        else if (other.CompareTag("Monster"))
        {
            Death();
        }
        if (other.CompareTag("Wall") || other.CompareTag("Box")|| other.CompareTag("fakeBox"))
        {
            isOnWallOrBox = true; 
        }
    }
    

   
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Box")|| other.CompareTag("fakeBox"))
        {
            isOnWallOrBox = false; 
        }
    }
    public void MovePlayer(Vector2 direction)
    {
        Vector2 newPosition = rb.position + moveSpeed * Time.fixedDeltaTime * direction;
        
        
        if (!IsPositionWithinBounds(newPosition))
        {
            newPosition = ClampPositionToBounds(newPosition);
        }
        
        rb.MovePosition(newPosition);
    }

    public bool IsPositionWithinBounds(Vector2 position)
    {
        return position.x >= -0.1f && position.x <= 14.1f && position.y >= -0.1f && position.y <= 10.1f;
    }

    
    public Vector2 ClampPositionToBounds(Vector2 position)
    {
        float x = Mathf.Clamp(position.x, -0.1f, 14.1f);
        float y = Mathf.Clamp(position.y, -0.1f, 10.1f);
        return new Vector2(x, y);
    }

    public void Death()
    {
        isAlive = false;
        enabled = false;
        gameObject.SetActive(false);

        FindObjectOfType<GameManager>().CheckWin();


    }
    public void IncreaseSpeed()
    {
        if (IsPowerUpEnable)
        {
            moveSpeed *= 1.5f;
            IsPowerUpEnable = false;
        }
    }

    public void ActivateDecreaseSpeed()
    {
        float _movespeed = moveSpeed;
        moveSpeed = moveSpeed/2f;
        StartCoroutine(DeactivateDecreaseSpeed(_movespeed,10f));
    }

    private IEnumerator DeactivateDecreaseSpeed(float _movespeed, float delay)
    {
        
        yield return new WaitForSeconds(delay);
        moveSpeed = _movespeed;
        
    }

    public void ActivateFlyingPowerUp()
    {
        
        isFlying = true;
        GetComponent<Collider2D>().isTrigger = true;
        spriteRendererUp.isGhost = true;
        spriteRendererDown.isGhost = true;
        spriteRendererLeft.isGhost = true;
        spriteRendererRight.isGhost = true;
        activeSpriteRenderer.isGhost = true;
        Invoke(nameof(TimerExpires), 3.5f);
        StartCoroutine(DeactivateFlyingPowerUpAfterDelay(5f));
    }

    
    private IEnumerator DeactivateFlyingPowerUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        isFlying = false;
        spriteRendererUp.isExpires = false;
        spriteRendererDown.isExpires = false;
        spriteRendererLeft.isExpires = false;
        spriteRendererRight.isExpires = false;
        activeSpriteRenderer.isExpires = false;
        GetComponent<Collider2D>().isTrigger = false;
        if(isOnWallOrBox)
        {
            Death();
        }
    }

    /*public static void Create2(Vector3 position)
    {
        Instantiate(player2Prefab, position, Quaternion.identity);
    }*/
}
