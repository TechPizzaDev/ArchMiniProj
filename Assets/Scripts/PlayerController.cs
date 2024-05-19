using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool walking = false;
    //Rigidbody2D body;

    public Transform destinationPoint;

    NavMeshAgent navMeshAgent;

    LineRenderer lineRenderer;
    Vector3[] walkPointBuffer = new Vector3[16];

    public float runSpeed = 20.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //body = GetComponent<Rigidbody2D>();

        lineRenderer = destinationPoint.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Don't set dest if user clicked something on UI.
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SetDestination();
            }
        }

        if (Vector3.Distance(navMeshAgent.nextPosition, transform.position) < 0.5f)
        {
            walking = false;
        }

        if (transform.position.x < navMeshAgent.destination.x)
        {
            //Debug.Log("going right");
            walking = true;
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > navMeshAgent.destination.x)
        {
            //Debug.Log("going left");
            walking = true;
            spriteRenderer.flipX = true;
        }

        anim.SetBool("walking", walking);

        if (walking)
        {
            int count = navMeshAgent.path.GetCornersNonAlloc(walkPointBuffer);
            lineRenderer.positionCount = count;
            lineRenderer.SetPositions(walkPointBuffer.AsSpan(0, count).ToArray());
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void SetDestination()
    {
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent reference is null. Make sure it's properly initialized.");
            return;
        }

        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Camera.main.nearClipPlane; // Use the near clip plane of the camera

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        destinationPoint.position = new Vector3(worldPos.x, worldPos.y, 0f);

        navMeshAgent.destination = destinationPoint.position;

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh surface.");
            return;
        }
    }


}