using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    StateController controller;

    public GameObject prefabBarracks;
    public GameObject prefabCamp;
    public bool buildBarracks;
    public bool buildCamp;
    public float viewRange = 50;
    public float harvestRange = 7;
    public float buildRange;
    public float maxWeight = 25;
    public float woodCollected = 0;
    public float goldCollected = 0;
    public float health;
    public int teamNumber;
    public bool collectGold;
    public bool collectWood;
    //public float goldCollected = 0;
    public float harvestRate = 2;           //per second

    // Start is called before the first frame update
    void Start()
    {
        collectGold = false;
        collectWood = true;
        health = 5;
        controller = GetComponent<StateController>();
        controller.ChangeState(new WorkerControl());
        buildRange = 5;
        buildBarracks = false;
        buildCamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(buildBarracks){
            controller.ChangeState(new WorkerBuildBarracks());
            buildBarracks = false;
        } else if (buildCamp){
            controller.ChangeState(new WorkerBuildCamp());
            buildCamp = false;
        }
    }

    public bool BackpackFull()
    {
        return (woodCollected + goldCollected) >= maxWeight;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, this.viewRange);
    }
}
