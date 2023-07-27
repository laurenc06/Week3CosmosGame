using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStill : State
{
    Warrior warrior;
    private Warrior warriorGO;

    public bool enemyInRange;
    public GameObject enemy;
    public GameObject teamBase;

    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        warrior = sc.gameObject.GetComponent<Warrior>();
        warriorGO = warrior.GetComponent<Warrior>();
        agent = warrior.GetComponent<UnityEngine.AI.NavMeshAgent>();

       if (warriorGO.getTeam() == 0) {
            teamBase = GameObject.Find("Cat Base");   
        }
        if (warriorGO.getTeam() == 1) {
            teamBase = GameObject.Find("Dog Base");   
        }
        agent.destination = teamBase.transform.position;
    }

    public override void OnUpdate()
    {
        enemyInRange = EnemyInRange();
        if (enemyInRange) {
            sc.AddNewState(new WarriorChase());
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
        enemy = sc.FindClosestEnemy(warriorGO.viewRange, warriorGO.getTeam()); 
        if (enemy == null) {
            return false;
        }
        Vector3 difference = enemy.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (warriorGO.viewRange * warriorGO.viewRange);
    }
}
