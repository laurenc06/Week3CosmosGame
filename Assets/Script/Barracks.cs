using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabWorker;
    public GameObject prefabGuard;

    void Start()
    {
        prefabWorker = GameObject.Find("Worker");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createWorker(){
        GameObject Worker = Object.Instantiate(prefabWorker, transform.position, Quaternion.identity);
    }
}

