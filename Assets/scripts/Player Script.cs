using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject road;
    public GameObject[] tiles;
    public GameObject signPrefab;
    int state;
    float moveCooldown = 0.2f;
    float lastMoveTime = 0f;
    float transitionSpeed = 2.0f;
    float moveSpeed = 6;
    float fakeZcomponent = 0.0f;

    private Vector3 lastRoadPosition = new Vector3(0.9565115f, -3.598834f, -2.844748f);
    private bool invalidMoveDetected = false;
    private float invalidMoveCooldown = 0.5f; // Cooldown duration for invalid moves
    private float lastInvalidMoveTime = 0f;

    private float jumpForce = 5.0f; // Force applied for the jump
    private bool isJumping = false; // Flag to check if the player is jumping
    float jumpZcomponent = 3.8f;
    int maxChangesPerRoad = 7; // Variable to control the maximum number of changes per road

    public TMP_Text scoreText; // Score : 0
    public TMP_Text speedText; // Speed : normal
    int speedState = 1; // 1: Normal , 2: High
    public TMP_Text fuelText; // Fuel : 50

    private float score = 0f; // Variable to keep track of the score

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = 2;
        score = 0f; // Initialize the score to 0
        UpdateScoreText(); // Update the score text at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the score based on the passage of time
        score += Time.deltaTime;
        UpdateScoreText(); // Update the score text
    }

    private void UpdateScoreText()
    {
        // Update the score text to display the current score
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "invisible wall")
        {
            GameObject newRoad = Instantiate(road);
            newRoad = generateTiles(newRoad);
            float roadLength = 23.0f;
            float playerZ = transform.position.z;
            newRoad.transform.position = new Vector3(lastRoadPosition.x, lastRoadPosition.y, playerZ + roadLength);
            lastRoadPosition = newRoad.transform.position;
        }
        else if (other.gameObject.tag == "Boost Tile")
        
        {
            Debug.Log("THIS IS A BOOST TILE ");
             if (speedState == 2)
            {
                return;
            }
            moveSpeed = moveSpeed * 2;
            speedState = 2;
            speedText.text = "Speed: High";
            jumpZcomponent = 7;
        }
        else if (other.gameObject.tag == "Burning Tile")
        
        {
            Debug.Log("THIS IS A Burning TILE ");
           
        }
        else if (other.gameObject.tag == "Empty Tile")
        
        {
            Debug.Log("THIS IS AN EMPTY TILE ");
            
        }
        else if (other.gameObject.tag == "Supplies Tile")
        
        {
            Debug.Log("THIS IS A SUPPLIES TILE ");
            
        }
          else if (other.gameObject.tag == "Sticky Tile")
        
        {
            Debug.Log("THIS IS A STICKY TILE ");
            
        }
        else if (other.gameObject.tag == "Obstacle Tile")
        
        {
            Debug.Log("THIS IS AN OBSTACLE TILE ");
            
        }

    }

    private GameObject generateTiles(GameObject newRoad)
    {
        Transform[] children = newRoad.GetComponentsInChildren<Transform>();
        Transform[] Lane1 = null;
        Transform[] Lane2 = null;
        Transform[] Lane3 = null;

        // Find the lanes
        foreach (Transform child in children)
        {
            if (child.name == "Lane 1")
            {
                Lane1 = child.GetComponentsInChildren<Transform>();
            }
            if (child.name == "Lane 2")
            {
                Lane2 = child.GetComponentsInChildren<Transform>();
            }
            if (child.name == "Lane 3")
            {
                Lane3 = child.GetComponentsInChildren<Transform>();
            }
        }

        // Remove the first element from Lane1
        for (int i = 1; i < Lane1.Length; i++)
        {
            Lane1[i - 1] = Lane1[i];
        }
        Array.Resize(ref Lane1, Lane1.Length - 1);

        // Remove the first element from Lane2
        for (int i = 1; i < Lane2.Length; i++)
        {
            Lane2[i - 1] = Lane2[i];
        }
        Array.Resize(ref Lane2, Lane2.Length - 1);

        // Remove the first element from Lane3
        for (int i = 1; i < Lane3.Length; i++)
        {
            Lane3[i - 1] = Lane3[i];
        }
        Array.Resize(ref Lane3, Lane3.Length - 1);

        int changesMade = 0; // Counter to keep track of the number of changes made

        // Iterate through the tiles in each lane
        for (int i = 0; i < 12; i++)
        {
            if (changesMade >= maxChangesPerRoad)
            {
                break; // Stop making changes if the maximum number of changes is reached
            }

            bool lane1Changed = false;
            bool lane2Changed = false;
            bool lane3Changed = false;

            // Randomly decide whether to put a sign or replace the tile with a special tile
            int randAction1 = UnityEngine.Random.Range(0, 3); // 0: unchanged, 1: put sign, 2: replace with special tile
            int randAction2 = UnityEngine.Random.Range(0, 3); // 0: unchanged, 1: put sign, 2: replace with special tile
            int randAction3 = UnityEngine.Random.Range(0, 3); // 0: unchanged, 1: put sign, 2: replace with special tile

            // Ensure at least one tile in the horizontal piece remains unchanged
            if (randAction1 != 0 && randAction2 != 0 && randAction3 != 0)
            {
                int randLane = UnityEngine.Random.Range(0, 3);
                if (randLane == 0)
                {
                    randAction1 = 0;
                }
                else if (randLane == 1)
                {
                    randAction2 = 0;
                }
                else
                {
                    randAction3 = 0;
                }
            }

            if (randAction1 == 1 && !lane1Changed && changesMade < maxChangesPerRoad)
            {
                // Put a sign on the tile in Lane 1
                GameObject sign = Instantiate(signPrefab);
                sign.transform.position = new Vector3(Lane1[i].position.x - 1, Lane1[i].position.y + 0.369f, Lane1[i].position.z + 1);
                sign.transform.parent = Lane1[i];
                lane1Changed = true;
                changesMade++;
                string tileName = Lane1[i].name;
                // Debug.Log($"Sign placed on Lane 1, Tile {tileName}");
            }
            else if (randAction1 == 2 && !lane1Changed && changesMade < maxChangesPerRoad)
            {
                // Replace the tile in Lane 1 with a random special tile
                Transform oldTileTransform = Lane1[i];
                int randIndix = UnityEngine.Random.Range(0, tiles.Length);
                GameObject tilePrefab = tiles[randIndix];
                Vector3 oldTilePosition = oldTileTransform.position;
                Quaternion oldTileRotation = oldTileTransform.rotation;
                Transform oldTileParent = oldTileTransform.parent;
                Destroy(oldTileTransform.gameObject);

                GameObject newTile = Instantiate(tilePrefab, oldTilePosition, oldTileRotation);
                newTile.transform.parent = oldTileParent;
                Lane1[i] = newTile.transform;
                lane1Changed = true;
                changesMade++;
                string tileName = Lane1[i].name;
                // Debug.Log($"Tile replaced in Lane 1, Tile {tileName}");
            }

            if (randAction2 == 1 && !lane2Changed && changesMade < maxChangesPerRoad)
            {
                // Put a sign on the tile in Lane 2
                GameObject sign = Instantiate(signPrefab);
                sign.transform.position = new Vector3(Lane2[i].position.x - 1, Lane2[i].position.y + 0.369f, Lane2[i].position.z + 1);
                sign.transform.parent = Lane2[i];
                lane2Changed = true;
                changesMade++;
                String tileName = Lane2[i].name;
                // Debug.Log($"Sign placed on Lane 2, Tile {tileName}");
            }
            else if (randAction2 == 2 && !lane2Changed && changesMade < maxChangesPerRoad)
            {
                // Replace the tile in Lane 2 with a random special tile
                int randIndex = UnityEngine.Random.Range(0, tiles.Length);
                GameObject tilePrefab = tiles[randIndex];
                Vector3 oldTilePosition = Lane2[i].position;
                Quaternion oldTileRotation = Lane2[i].rotation;
                Transform oldTileParent = Lane2[i].parent;
                Destroy(Lane2[i].gameObject);

                GameObject newTile = Instantiate(tilePrefab, oldTilePosition, oldTileRotation);
                newTile.transform.parent = oldTileParent;
                Lane2[i] = newTile.transform;
                lane2Changed = true;
                changesMade++;
                String tileName = Lane2[i].name;
                // Debug.Log($"Tile replaced in Lane 2, Tile {tileName}");
            }

            if (randAction3 == 1 && !lane3Changed && changesMade < maxChangesPerRoad)
            {
                // Put a sign on the tile in Lane 3
                GameObject sign = Instantiate(signPrefab);
                sign.transform.position = new Vector3(Lane3[i].position.x - 1, Lane3[i].position.y + 0.369f, Lane3[i].position.z + 1);
                sign.transform.parent = Lane3[i];
                lane3Changed = true;
                changesMade++;
                Transform tileName = Lane3[i];
                // Debug.Log($"Sign placed on Lane 3, Tile {tileName}");
            }
            else if (randAction3 == 2 && !lane3Changed && changesMade < maxChangesPerRoad)
            {
                // Replace the tile in Lane 3 with a random special tile
                int randIndex = UnityEngine.Random.Range(0, tiles.Length);
                GameObject tilePrefab = tiles[randIndex];
                Vector3 oldTilePosition = Lane3[i].position;
                Quaternion oldTileRotation = Lane3[i].rotation;
                Transform oldTileParent = Lane3[i].parent;
                Destroy(Lane3[i].gameObject);

                GameObject newTile = Instantiate(tilePrefab, oldTilePosition, oldTileRotation);
                newTile.transform.parent = oldTileParent;
                Lane3[i] = newTile.transform;
                lane3Changed = true;
                changesMade++;
                String tileName = Lane3[i].name;
                // Debug.Log($"Tile replaced in Lane 3, Tile {tileName}");
            }
        }

        return newRoad;
    }

    private void FixedUpdate()
    {
        if (!isJumping)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);

           // Clamp the player's y position to 0.57 if not jumping
            if (transform.position.y > 0.57f)
            {
                transform.position = new Vector3(transform.position.x, 0.57f, transform.position.z);
            }
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
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, jumpZcomponent);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToLower().Contains("road"))
        {
            isJumping = false;
            Roadscript.isAllowedToMove = true;
        }
        if (collision.gameObject.tag == "Obstacle Tile")
        
        {
            Debug.Log("THIS IS AN OBSTACLE TILE (collision) ");
            
        }
    }
}