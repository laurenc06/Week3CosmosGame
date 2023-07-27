using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherStill : State
{
    Archer archer;
    private Archer archerGO;

    public bool enemyInRange;
    public GameObject enemy;
    public GameObject teamBase;

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
        agent.destination = teamBase.transform.position;
    }

    public override void OnUpdate()
    {
        //What does the guard do?
        enemyInRange = EnemyInRange();

        if (enemyInRange) {
            sc.AddNewState(new ArcherChase());
        }
        else {
            agent.destination = teamBase.transform.position;
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    public bool EnemyInRange() {
        enemy = sc.FindClosestEnemy(archerGO.viewRange, archerGO.getTeam()); //NEED TO FIGURE OUT WHAT TEAM SO WE KNOW IF ENEMY OR FRIEND
        if (enemy == null) {
            return false;
        }
        Vector3 difference = enemy.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (archerGO.viewRange * archerGO.viewRange);
    }
}
