using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; //speed of cam
    
    void start() {

    }
    
    void Update () {
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.W)) {
            Vector3 myVector = new Vector3(0,0,1);
            transform.position += myVector * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S)) {
             Vector3 myVector = new Vector3(0,0,-1);
             transform.position += myVector * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.Q)) {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.position += -transform.forward * Time.deltaTime * speed;
        }
    }
}
