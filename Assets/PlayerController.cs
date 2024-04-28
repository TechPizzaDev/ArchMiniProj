using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool walking = false;
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            walking = true;
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            walking = true;
            spriteRenderer.flipX = false;
        }
        else
        {
            walking = false;
        }

        
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if (walking)
        {
            anim.SetBool("walking", true);
        }
        else if (!walking)
        {
            anim.SetBool("walking", false);
        }
    }


}