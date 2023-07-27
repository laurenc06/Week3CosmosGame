using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierChase : State
{
    Soldier soldier;

    public bool baseInRange;
    public GameObject teamBase;
    public GameObject target;
    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        soldier = sc.gameObject.GetComponent<Soldier>();
        agent = soldier.GetComponent<UnityEngine.AI.NavMeshAgent>();
        teamBase = GameObject.Find("Cat Base");

        target = sc.FindClosestEnemy(soldier.GetComponent<Soldier>().viewRange, soldier.GetComponent<Soldier>().teamNumber); 
        agent.destination = target.transform.position;
    }

    public override void OnUpdate()
    {
        //What does the guard do?
        baseInRange = BaseInRange();

        if (baseInRange) {
            agent.destination = target.transform.position;
        }
        else {
            sc.AddNewState(new SoldierStill()); //AHHHH
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    public bool BaseInRange() {
        Vector3 difference = teamBase.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (soldier.GetComponent<Soldier>().viewRange * soldier.GetComponent<Soldier>().viewRange);
    }

    public override void OnCollisionEnter(Collision collision) //trigger enter?
    {
        if (collision.gameObject.tag == "Worker") { //I NEED SOMETHING TO DIFFERENTIATE WHAT TEAM THEY ARE 
            //decrease the health of enemy
        }
    }

    //method with range set to like 2 for warrior

    //method with range set to like 10 for archer
}
