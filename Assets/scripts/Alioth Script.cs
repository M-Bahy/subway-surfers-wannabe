using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliothScript : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 0.005f; // Set your desired constant speed
    public static bool isAllowedToMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowedToMove){
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "first road") {
            Debug.Log("You hit the first road");
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "first road") {
            Debug.Log("You hit the first road with a trigger");
            moveSpeed = 7.5f;
        }}

}
