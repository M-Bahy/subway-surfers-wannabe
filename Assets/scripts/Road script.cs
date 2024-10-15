using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roadscript : MonoBehaviour
{
    public float moveSpeed = -2.5f; // Set your desired constant speed

    // Start is called before the first frame update
    void Start()
    {
        // No need to get Rigidbody if not using it
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object at a constant speed
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }
}