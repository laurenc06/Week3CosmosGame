using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : State
{
    Archer archer;
    private Archer archerGO;
    public GameObject target;
    public GameObject teamBase;

    public float lastAttack = 0f;
    public float attackRate = 2.0f;

    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        archer = sc.gameObject.GetComponent<Archer>();
        archerGO = archer.gameObject.GetComponent<Archer>();
        agent = archer.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (archerGO.getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (archerGO.getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
        target = sc.FindClosestEnemy(archerGO.viewRange, archerGO.getTeam()); 
    }

    public override void OnUpdate()
    {
        target = sc.FindClosestEnemy(archerGO.viewRange, archerGO.teamNumber); 
        if (target == null) {
            agent.destination = teamBase.transform.position;
            sc.AddNewState(new ArcherStill()); 
        }
        //What does the guard do?
        if (targetInAttackRange()) {
            lastAttack += Time.deltaTime;
            if (lastAttack >= attackRate) {
                attack();
            }
        }
        else {
            sc.AddNewState(new ArcherChase()); 
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    public bool targetInAttackRange() {
        if (target == null) {
            return false;
        }
        Vector3 difference = target.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (archerGO.getAttackRange() * archerGO.getAttackRange());
    }

    public void attack() {
        if (target.CompareTag("Warrior")) target.GetComponent<Warrior>().takeDamage(archerGO.getDamage()); 
        if (target.CompareTag("Archer")) target.GetComponent<Archer>().takeDamage(archerGO.getDamage()); 
        if (target.CompareTag("Worker")) target.GetComponent<WorkerScript>().takeDamage(archerGO.getDamage());

        //Reset lastAttack
        lastAttack = 0;
    
    } 

}
