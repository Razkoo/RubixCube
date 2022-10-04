using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    private Vector2 turn;
    private int MIN_RANGE = -2, MAX_RANGE = 2;
    private float xm = 0, ym = 0, zm = 0;
    bool a, d, w, s, space;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X"); 
        turn.y += Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.RightArrow)) xm = 0.3f;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) xm = -0.3f;
        if (Input.GetKeyDown(KeyCode.UpArrow)) zm = 0.4f;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) zm = -0.4f;
        if (Input.GetKeyDown(KeyCode.O)) ym = 0.4f;
        else if (Input.GetKeyDown(KeyCode.P)) ym = -0.4f;

        transform.eulerAngles = new Vector3(-turn.y, turn.x, 0.0f); // Mouse angle
        transform.position = new Vector3(transform.position.x + xm, transform.position.y + ym, transform.position.z +  zm); // Player angle
        xm = 0; ym = 0; zm = 0;

    }
}
