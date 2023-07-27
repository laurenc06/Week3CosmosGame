using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerRetrieve : State
{
    WorkerScript worker;
    NavMeshAgent agent;
    public GameObject teamBase;

    public override void OnEnter()
    {
        Debug.Log("WorkerRetrieve: OnEnter()");
        worker = sc.gameObject.GetComponent<WorkerScript>();
        agent = worker.GetComponent<NavMeshAgent>();
        if (worker.GetComponent<WorkerScript>().getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (worker.GetComponent<WorkerScript>().getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
        FindBase();
    }

    public override void OnUpdate()
    {

    }

    public void FindBase()
    {
        agent.destination = teamBase.transform.position;
    }

    public void TransferResources()
    {
        //Resource in range, give resources
        agent.ResetPath();

        TeamController teamController = teamBase.GetComponent<TeamController>();
        //teamController.gold += worker.goldCollected;
        teamController.wood += worker.woodCollected;

       // worker.goldCollected = 0;
        worker.woodCollected = 0;

        Debug.Log("remove WorkerRetrieve()");
        sc.RemoveTop(); //state done, get out
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            TransferResources();
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }
}
