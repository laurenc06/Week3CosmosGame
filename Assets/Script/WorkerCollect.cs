using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerCollect : State
{
    WorkerScript worker;
    NavMeshAgent agent;
    GameObject target;

    public override void OnEnter()
    {
        Debug.Log("WorkerGather:OnEnter()");

        worker = sc.gameObject.GetComponent<WorkerScript>();
        agent = worker.GetComponent<NavMeshAgent>();
        //Debug.Log("OnEnter: " + worker);

        //Find nearest resources
        if(worker.GetComponent<WorkerScript>().collectGold){
            FindNearestGold();
        } else if (worker.GetComponent<WorkerScript>().collectWood){
            FindNearestResource();
        }
    }

    public override void OnUpdate()
    {
        if (worker == null)
        {
            //Just a check to prevent errors (shouldn't reach this)
            Debug.Log("unreachable code was reached");
            return;
        }

        if (target == null)
        {
            //No resource, find next resource
            if(worker.GetComponent<WorkerScript>().collectGold){
                FindNearestGold();
            } else if (worker.GetComponent<WorkerScript>().collectWood){
                FindNearestResource();
            }
        }
        else if (worker.BackpackFull())
        {
            //Cannot carry anymore, transition to return state
            Debug.Log("WorkerGather() change to WorkerReturnResources()");
            worker.GetComponent<WorkerScript>().collectWood = !worker.GetComponent<WorkerScript>().collectWood;
            worker.GetComponent<WorkerScript>().collectGold = !worker.GetComponent<WorkerScript>().collectGold;
            sc.ChangeState(new WorkerRetrieve());
        }
        else if (Vector3.Distance(worker.transform.position, target.transform.position) <= worker.harvestRange)
        {
            //Resource in range, harvest
            agent.ResetPath();
            if (target.CompareTag("ResourceArea"))
            {
                float collected = target.GetComponent<ResourceArea>().takeResource(worker.harvestRate);
                worker.woodCollected += collected * Time.deltaTime;
            }
            else if (target.CompareTag("Gold"))
            {
                float collected = target.GetComponent<GoldArea>().takeGold(worker.harvestRate);
                worker.goldCollected += worker.harvestRate * Time.deltaTime;
            }
            // add this in when we have a second resourcearea
        }
    }

    public override void OnExit()
    {
        //Nothing to do here, just move along
        Debug.Log("WorkerGather:OnExit()");
    }

    public void FindNearestResource()
    {
        target = sc.FindClosestTarget("ResourceArea", worker.viewRange);

        if (target == null)
        {
            //No resource found, remove state
            sc.RemoveTop();
        }
        else
        {
            agent.destination = target.transform.position;
        }
    }

    public void FindNearestGold(){
        target = sc.FindClosestTarget("Gold", worker.viewRange);

        if (target == null)
        {
            //No resource found, remove state
            worker.GetComponent<WorkerScript>().collectWood = !worker.GetComponent<WorkerScript>().collectWood;
            worker.GetComponent<WorkerScript>().collectGold = !worker.GetComponent<WorkerScript>().collectGold;
            
        }
        else
        {
            agent.destination = target.transform.position;
        }
    }
}
