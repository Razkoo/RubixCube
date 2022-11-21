using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorMove : MonoBehaviour
{

    public Vector2 turn;
    [Range(0.1f, 100f)] public float sensetivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
