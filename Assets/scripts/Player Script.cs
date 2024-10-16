using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject road;
    int state;
    float moveCooldown = 0.2f;
    float lastMoveTime = 0f;
    float transitionSpeed = 2.0f;
    float moveSpeed = 7.5f;
    float fakeZcomponent = 0.0f;
    
    float roadLength = 23.0f;

    private Vector3 lastRoadPosition;
    private bool isFirstRoad = true;
    private bool invalidMoveDetected = false;
    private float invalidMoveCooldown = 0.5f; // Cooldown duration for invalid moves
    private float lastInvalidMoveTime = 0f;

    public float jumpForce = 0.0001f; // Force applied for the jump
    private bool isJumping = false; // Flag to check if the player is jumping
    float jumpZcomponent = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "invisible wall")
        {
            GameObject newRoad = Instantiate(road);
            if (isFirstRoad)
            {
                newRoad.transform.position = new Vector3(0.95f, -3.58f, 18.93f);
                isFirstRoad = false;
            }
            else
            {
                newRoad.transform.position = lastRoadPosition + new Vector3(0, 0, roadLength - 4.53f);
            }
           lastRoadPosition = newRoad.transform.position;
        }
    }

    private void FixedUpdate()
    {
        
        if (!isJumping)
        {
           rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
        }
        if (Time.time - lastMoveTime < moveCooldown)
        {
            return;
        }
        float motionX = Input.GetAxis("Horizontal");
        bool right = motionX > 0;
        bool left = motionX < 0;
        bool jump = Input.GetAxis("Jump") > 0;

        if (right)
        {
            switch (state)
            {
                case 1:
                    transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z + fakeZcomponent);
                    state = 2;
                    lastMoveTime = Time.time;
                    break;
                case 2:
                    transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z + fakeZcomponent);
                    state = 3;
                    lastMoveTime = Time.time;
                    break;
                case 3:
                    if (!invalidMoveDetected || Time.time - lastInvalidMoveTime > invalidMoveCooldown)
                    {
                        Debug.Log("Invalid move");
                        invalidMoveDetected = true;
                        lastInvalidMoveTime = Time.time;
                    }
                    motionX = 0;
                    break;
            }
        }

        if (left)
        {
            switch (state)
            {
                case 1:
                    if (!invalidMoveDetected || Time.time - lastInvalidMoveTime > invalidMoveCooldown)
                    {
                        Debug.Log("Invalid move");
                        invalidMoveDetected = true;
                        lastInvalidMoveTime = Time.time;
                    }
                    motionX = 0;
                    break;
                case 2:
                    transform.position = new Vector3(transform.position.x - transitionSpeed, transform.position.y, transform.position.z + fakeZcomponent);
                    state = 1;
                    lastMoveTime = Time.time;
                    break;
                case 3:
                    transform.position = new Vector3(transform.position.x - transitionSpeed, transform.position.y, transform.position.z + fakeZcomponent);
                    state = 2;
                    lastMoveTime = Time.time;
                    break;
            }
        }

        if (jump && !isJumping)
        {
            Roadscript.isAllowedToMove = false;
            AliothScript.isAllowedToMove = false;
            Jump();
        }

        // Reset the invalid move detection flag after the cooldown period
        if (invalidMoveDetected && Time.time - lastInvalidMoveTime > invalidMoveCooldown)
        {
            invalidMoveDetected = false;
        }
    }

    private void Jump()
    {
        isJumping = true;
       // do it with velocity
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z/2.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToLower().Contains("road"))
        {
            isJumping = false;
            AliothScript.isAllowedToMove = true;
            Roadscript.isAllowedToMove = true;
            
        }
    }
}