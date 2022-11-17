using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float turnSpeed = 90;
    [Range(0.1f, 1000f)] public float horizontalSpeed;
    [Range(0, 5000)] public float verticalSpeed;
    public KeyCode up, down;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
        rb.velocity = (transform.forward * Input.GetAxis("Vertical")) * horizontalSpeed * Time.fixedDeltaTime;

        if (Input.GetKey(up)) rb.AddForce(transform.up * verticalSpeed * Time.fixedDeltaTime);
        else if (Input.GetKey(down)) rb.AddForce(transform.up * -verticalSpeed * Time.fixedDeltaTime);
    }
}
