using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerRetrieve : State
{
    WorkerScript worker;
    NavMeshAgent agent;
    GameObject target;

    public override void OnEnter()
    {
        Debug.Log("WorkerRetrieve: OnEnter()");
        worker = sc.gameObject.GetComponent<WorkerScript>();
        agent = worker.GetComponent<NavMeshAgent>();
        FindBase();
    }

    public override void OnUpdate()
    {

    }

    public void FindBase()
    {
        target = sc.FindClosestTarget("Base", Mathf.Infinity);
        agent.destination = target.transform.position;
    }

    public void TransferResources()
    {
        //Resource in range, give resources
        agent.ResetPath();

        TeamController teamController = target.GetComponent<TeamController>();
        teamController.gold += worker.goldCollected;
        teamController.wood += worker.woodCollected;

        worker.goldCollected = 0;
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
