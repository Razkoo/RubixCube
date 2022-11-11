using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    private Vector2 turn;
    private float xm = 0, ym = 0, zm = 0;
    bool r, l, u, d, o, p;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        r = Input.GetKey(KeyCode.RightArrow);
        l = Input.GetKey(KeyCode.LeftArrow);
        u = Input.GetKey(KeyCode.UpArrow);
        d = Input.GetKey(KeyCode.DownArrow);
        o = Input.GetKey(KeyCode.O);
        p = Input.GetKey(KeyCode.P);

        turn.x += Input.GetAxis("Mouse X"); 
        turn.y += Input.GetAxis("Mouse Y");

        if (r) xm = 0.3f;
        else if (l) xm = -0.3f;
        if (u) zm = 0.4f;
        else if (d) zm = -0.4f;
        if (o) ym = 0.4f;
        else if (p) ym = -0.4f;

        transform.eulerAngles = new Vector3(-turn.y, turn.x, 0.0f); // Mouse angle
        transform.position = new Vector3(transform.position.x + xm, transform.position.y + ym, transform.position.z +  zm); // Player angle
        xm = 0; ym = 0; zm = 0;

    }
}
