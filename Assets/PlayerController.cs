using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool walking = false;
    //Rigidbody2D body;

    public Transform destinationPoint;

    NavMeshAgent navMeshAgent;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetDestination();
        if (Vector3.Distance(navMeshAgent.nextPosition, transform.position) < 0.5f)
        {
            walking = false;
        }
        if (transform.position.x < navMeshAgent.destination.x)
        {
            Debug.Log("going right");
            walking = true;
            spriteRenderer.flipX = false;
        }
        if (transform.position.x > navMeshAgent.destination.x)
        {
            Debug.Log("going left");
            walking = true;
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {

        if (walking)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }


    }
    //body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);


    void SetDestination()
    {

        navMeshAgent.destination = destinationPoint.position;

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                mousePos.z = Camera.main.nearClipPlane; // Use the near clip plane of the camera

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);


                destinationPoint.position = new Vector3(worldPos.x, worldPos.y, 0f);
                Debug.Log(worldPos.x);
                Debug.Log(worldPos.y);
                Debug.Log(worldPos.z);

            }
        }
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent reference is null. Make sure it's properly initialized.");
            return;
        }

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh surface.");
            return;
        }
    }


}