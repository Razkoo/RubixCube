using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorMove : MonoBehaviour
{
    
    float rotationX = 0f;
    [Range(0.1f, 100f)] public float sensetivity;
    
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensetivity;
        transform.eulerAngles = new Vector3(rotationX, 0, 0);
    }
}
