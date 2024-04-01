using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    
    private float normalSpeed;
    public int playerNumber = 1;

    
    public GameObject groundPrefab;
    private Vector2 movement;
    private Rigidbody2D rb; 
    private string horizontalAxis;
    private string verticalAxis;


    void Start()
    {
        normalSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        horizontalAxis = "Player" + playerNumber + "Horizontal";
        verticalAxis = "Player" + playerNumber + "Vertical";
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw(horizontalAxis);
        movement.y = Input.GetAxisRaw(verticalAxis);


    }

    void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
            Death();
        }
        
        
    }

    

    private void Death()
    {
        enabled = false;

        GetComponent<Bomb>().enabled = false;
        
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWin();
      
    }
    public void IncreaseSpeed()
    {
        moveSpeed *= 1.5f;
    }
    

}
