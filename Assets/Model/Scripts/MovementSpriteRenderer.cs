using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpriteRenderer : MonoBehaviour
{

    
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;
    public float animationTime = 0.25f;
    private int animationFrame;

    public bool isInviolable = false;
    public bool isExpires = false;
    public bool isGhost = false;
    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;

        if(loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if(idle)
        {
            spriteRenderer.sprite = idleSprite;
        } 
        else if(animationFrame >=0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }

        if(isInviolable)
        {
            spriteRenderer.color = Color.blue;
        } else if (isExpires)
        {
            spriteRenderer.color = Color.red;
        } else if (isGhost)
        {
            spriteRenderer.color = Color.green;
        }  
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
