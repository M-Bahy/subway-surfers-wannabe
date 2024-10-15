using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    int state ;
    float moveCooldown = 0.2f; 
    float lastMoveTime = 0f; 
    float transitionSpeed = 2.0f;
    float moveSpeed = 10.0f;
    float fakeZcomponent = 0.0f;


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
    
        
    
    private void FixedUpdate() {
        rb.AddForce(new Vector3(0, 0, moveSpeed));
        if (Time.time - lastMoveTime < moveCooldown) {
            return;
        }
    
        float motionX = Input.GetAxis("Horizontal");
        bool right = motionX > 0;
        bool left = motionX < 0;
    
        if (right) {
            switch (state) {
                case 1:
                    transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y , transform.position.z+ fakeZcomponent);
                    state = 2;
                    lastMoveTime = Time.time; 
                    break;
                case 2:
                    transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z+ fakeZcomponent);
                    state = 3;
                    lastMoveTime = Time.time; 
                    break;
                case 3:
                    Debug.Log("Invalid move");
                    break;
            }
        }
    
        if (left) {
            switch (state) {
                case 1:
                    Debug.Log("Invalid move");
                    break;
                case 2:
                    transform.position = new Vector3(transform.position.x - transitionSpeed, transform.position.y, transform.position.z+ fakeZcomponent);
                    state = 1;
                    lastMoveTime = Time.time; 
                    break;
                case 3:
                    transform.position = new Vector3(transform.position.x - transitionSpeed, transform.position.y, transform.position.z+ fakeZcomponent);
                    state = 2;
                    lastMoveTime = Time.time; 
                    break;
            }
        }
    }
}
