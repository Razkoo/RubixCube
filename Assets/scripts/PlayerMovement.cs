using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 5;
    public float turnSpeed = 90;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        characterController.Move(transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);dcdfgjl';lkjhgfdsa  
    }
}
