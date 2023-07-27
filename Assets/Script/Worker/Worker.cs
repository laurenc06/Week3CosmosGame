using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    StateController controller;

    public float viewRange = 50;
    public float harvestRange = 2;
    public float maxWeight = 25;
    public float woodCollected = 0;
    public float goldCollected = 0;
    public float harvestRate = 2;   
    public int teamNumber;        //per second

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new WorkerThink());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool BackpackFull()
    {
        return (woodCollected + goldCollected) >= maxWeight;
    }
}
