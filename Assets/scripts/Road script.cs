using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roadscript : MonoBehaviour
{
    public float moveSpeed = -2.5f; // Set your desired constant speed
    Rigidbody rb ;
    public static bool isAllowedToMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
         if (other.gameObject.tag == "eater") {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object at a constant speed
        if (isAllowedToMove){
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }
    }
}