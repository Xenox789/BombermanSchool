using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI_2 : MonoBehaviour
{
    public float movespeed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void MoveMonster()
    {
        Vector2 targetPosition = new Vector2(Mathf.Round(transform.position.x + moveDirection.x), Mathf.Round(transform.position.y + moveDirection.y));
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movespeed * Time.deltaTime);
    }
}
