using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerRetrieve : State
{
    WorkerScript worker;
    NavMeshAgent agent;
    GameObject target;
    public GameObject teamBase;
    public GameObject[] teamBases;

    public override void OnEnter()
    {
        Debug.Log("WorkerRetrieve: OnEnter()");
        worker = sc.gameObject.GetComponent<WorkerScript>();
        agent = worker.GetComponent<NavMeshAgent>();
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == worker.GetComponent<WorkerScript>().teamNumber){
                teamBase = teamBases[count];
            }
        }
        target = teamBase;
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
