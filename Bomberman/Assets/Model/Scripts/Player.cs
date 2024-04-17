using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int playerNumber = 1;

    //public GameObject bombPrefab;

    private Vector2 movementDirection;
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


    [SerializeField] private static GameObject player1Prefab;
    [SerializeField] private static GameObject player2Prefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }


    void Update()
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
        
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            Death();
            
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            Death();
            
        }
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
        if(IsPowerUpEnable)
        {
            moveSpeed *= 1.5f;
            IsPowerUpEnable = false;
        }
    }
    
    public static void Create1(Vector3 position)
    {
        Instantiate(player1Prefab, position, Quaternion.identity);
    }

    public static void Create2(Vector3 position)
    {
        Instantiate(player2Prefab, position, Quaternion.identity);
    }
}
