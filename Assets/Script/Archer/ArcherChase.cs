using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherChase : State
{
    Archer archer;
    private Archer archerGO;

    public bool baseInRange;
    public GameObject teamBase;
    public GameObject target;
    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        archer = sc.gameObject.GetComponent<Archer>();
        archerGO = archer.GetComponent<Archer>();
        agent = archer.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (archerGO.getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (archerGO.getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
        target = sc.FindClosestEnemy(archerGO.viewRange, archerGO.getTeam()); 
        if (target != null) {
            agent.destination = target.transform.position;
        }
    }

    public override void OnUpdate()
    {
        target = sc.FindClosestEnemy(archerGO.viewRange, archerGO.getTeam()); 

        if (enemyInAttackRange()) {
            sc.AddNewState(new ArcherAttack());
        }
        if (target != null) {
            agent.destination = target.transform.position;
        }
        else {
            sc.AddNewState(new ArcherStill());
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    // public bool BaseInRange() {
    //     Vector3 difference = teamBase.transform.position - agent.transform.position;
    //     return difference.sqrMagnitude < (archerGO.viewRange * archerGO.viewRange);
    // }

    public bool enemyInAttackRange() {
        if (target == null) {
            return false;
        }
        Vector3 difference = target.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (archerGO.getAttackRange() * archerGO.getAttackRange());
    }
}
