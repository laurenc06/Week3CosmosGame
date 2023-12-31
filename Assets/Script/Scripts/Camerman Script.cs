using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraman : MonoBehaviour
{
    public float speed = 5f; //speed of cam
    
    void start() {

    }
    
    void Update () {
        if (Input.GetKey(KeyCode.A)) { // Left
            transform.position += -transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D)) { // Right
            transform.position += transform.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W)) { // Forward
            //Vector3 myVector = new Vector3(0,0,1);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S)) { // Backward
             //Vector3 myVector = new Vector3(0,0,-1);
             transform.position += -1*transform.forward * Time.deltaTime * speed;
        }
        // if (Input.GetKey(KeyCode.Q) && transform.position.y > 15) { // Zoom in, limit at y=15
        //     transform.position += transform.forward * Time.deltaTime * speed;
        // }
        // if (Input.GetKey(KeyCode.E) && transform.position.y < 35) { // Zoom out, limit at y=35
        //     transform.position += -transform.forward * Time.deltaTime * speed;
        // }
        if (Input.GetKey(KeyCode.T)) { // Rotate right
            //Vector3 myVector = new Vector3(0,2,0);
            //transform.eulerAngles += (myVector * speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.R)) { // Rotate left
            // Vector3 myVector = new Vector3(0,-2,0);
            // transform.eulerAngles += (myVector * speed * Time.deltaTime);
            transform.Rotate(new Vector3(0, -10 * Time.deltaTime, 0));
        }
    }
}
