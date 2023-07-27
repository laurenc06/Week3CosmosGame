using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttack : State
{
    Warrior warrior;
    private Warrior warriorGO;
    public GameObject teamBase;
    public GameObject target;
    
    public float lastAttack = 0f;
    public float attackRate = 2.0f;

    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        warrior = sc.gameObject.GetComponent<Warrior>();
        warriorGO = warrior.GetComponent<Warrior>();
        agent = warrior.GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = sc.FindClosestEnemy(warriorGO.viewRange, warriorGO.teamNumber); 
        if (warriorGO.getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (warriorGO.getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
    }

    public override void OnUpdate()
    {
        target = sc.FindClosestEnemy(warriorGO.viewRange, warriorGO.teamNumber); 
        if (target == null) {
            agent.destination = teamBase.transform.position;
            sc.AddNewState(new WarriorStill()); 
        }
        if (targetInAttackRange()) {
            lastAttack += Time.deltaTime;
            if (lastAttack >= attackRate) {
                attack();
            }
        }
        else {
            sc.AddNewState(new WarriorChase());
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
        return difference.sqrMagnitude < (warriorGO.getAttackRange() * warriorGO.getAttackRange());
    }

    public void attack() {
        if (target.CompareTag("Warrior")) target.GetComponent<Warrior>().takeDamage(warriorGO.getDamage()); 
        if (target.CompareTag("Archer")) target.GetComponent<Archer>().takeDamage(warriorGO.getDamage()); 
        if (target.CompareTag("Worker")) target.GetComponent<WorkerScript>().takeDamage(warriorGO.getDamage()); 
        
        //Reset lastAttack
        lastAttack = 0;
    
    } 

}
