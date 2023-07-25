using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; //speed of cam
    
    void start() {

    }
    
    void Update () {
        if (Input.GetKey(KeyCode.Q) && transform.position.y > 15) { // Zoom in, limit at y=15
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.E) && transform.position.y < 35) { // Zoom out, limit at y=35
            transform.position += -transform.forward * Time.deltaTime * speed;
        }
    }
}
