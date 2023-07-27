using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChase : State
{
    Warrior warrior;
    private Warrior warriorGO;

    public bool baseInRange;
    public GameObject teamBase;
    public GameObject target;
    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        warrior = sc.gameObject.GetComponent<Warrior>();
        warriorGO = warrior.GetComponent<Warrior>();;
        agent = warrior.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (warriorGO.getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (warriorGO.getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
        target = sc.FindClosestEnemy(warriorGO.viewRange, warriorGO.getTeam()); 
        agent.destination = target.transform.position;
    }

    public override void OnUpdate()
    {
        target = sc.FindClosestEnemy(warriorGO.viewRange, warriorGO.getTeam()); 
        if (enemyInAttackRange()) {
            sc.AddNewState(new WarriorAttack());
        }
         if (target != null) {
            agent.destination = target.transform.position;
        }
        else {
            sc.AddNewState(new WarriorStill()); 
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    // public bool BaseInRange() {
    //     Vector3 difference = teamBase.transform.position - agent.transform.position;
    //     return difference.sqrMagnitude < (warriorGO.viewRange * warriorGO.viewRange);
    // }

    public bool enemyInAttackRange() {
        if (target == null) {
            return false;
        }
        Vector3 difference = target.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (warriorGO.getAttackRange() * warriorGO.getAttackRange());
    }
}
