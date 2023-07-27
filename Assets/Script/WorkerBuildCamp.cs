using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;
 

public class WorkerBuildCamp : State
{
    GameObject target;
    GameObject buildingWaypoints;
    WorkerScript worker;
    NavMeshAgent agent;
    bool moving;
    bool building;
    public GameObject teamBase;
    public GameObject[] teamBases;
    public float buildTimer = 0;
    public float buildTime = 10;
    // Start is called before the first frame update
    public override void OnEnter()
    {
        Debug.Log("WorkerBuildCamps:OnEnter()");
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        worker = sc.gameObject.GetComponent<WorkerScript>();
        agent = worker.GetComponent<NavMeshAgent>();
        target = FindClosestWaypoint(FindBuildingWaypoints());
        Debug.Log(target.name);
        building = false;
        moving = false;
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == worker.GetComponent<WorkerScript>().teamNumber){
                teamBase = teamBases[count];
            }
        }
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        buildTimer += Time.deltaTime;
        if(FindBuildingWaypoints() == null){
            Debug.Log("ahhhhh");
            sc.RemoveTop();
        }
        if (worker == null)
        {
            //Just a check to prevent errors (shouldn't reach this)
            Debug.Log("unreachable code was reached");
            return;
        }
        if(!building){
            if (target == null)
            {
                target = FindClosestWaypoint(FindBuildingWaypoints());
                //No resource, find next resource
            } else if(target.GetComponent<BuildingWaypoint>().occupied){
                Debug.Log("No Space");
                sc.RemoveTop();
            } else if (agent.destination != target.transform.position && !moving) {
                Debug.Log("yup");
                agent.destination = target.transform.position;
                moving = true;
            } else if(Vector3.Distance(worker.transform.position, target.transform.position) <= worker.buildRange){
                Debug.Log("this");
                buildTimer = 0;
                building = true;
            }
        } else if (building){
            if(target.GetComponent<BuildingWaypoint>().occupied){
                sc.RemoveTop();
            }
            else if(buildTimer >= buildTime){
                Debug.Log("here");
                buildCamp();
            }
        }
    }

    public List<GameObject> FindBuildingWaypoints(){
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("BuildingWaypoint");
        List<GameObject> output = new List<GameObject>();

        for(int count = 0; count < waypoints.Length; count++){
            if(!waypoints[count].GetComponent<BuildingWaypoint>().occupied && waypoints[count].GetComponent<BuildingWaypoint>().teamNumber == worker.GetComponent<WorkerScript>().teamNumber){
                output.Add(waypoints[count]);
            }
        }
        return output;
    }

    public GameObject FindClosestWaypoint(List<GameObject> waypoints){
        GameObject closest = null;                          //result: starts at null just in case we don't find anything in range

        float distance = Int32.MaxValue;         //square the distance
        Vector3 position = worker.transform.position;              //our current position

        foreach (GameObject waypoint in waypoints)
        {
            Vector3 difference = waypoint.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance)                     //comparing the squared distances
            {
                closest = waypoint;                              //new closest object set
                distance = curDistance;                     //distance to the object saved for next comparison in loop
            }
        }

        //Return the obejct we found, or null if we didn't find anything
        return closest;
    }

    public void buildCamp(){
        GameObject Camp = GameObject.Instantiate(worker.GetComponent<WorkerScript>().prefabCamp, target.transform.position, Quaternion.identity);
        teamBase.GetComponent<TeamController>().campNum ++;
        target.GetComponent<BuildingWaypoint>().occupied = true;
        Debug.Log("Bye");
        sc.RemoveTop();
    }
}