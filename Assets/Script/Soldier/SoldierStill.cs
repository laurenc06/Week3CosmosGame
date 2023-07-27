using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStill : State
{
    Soldier soldier;

    public bool enemyInRange;
    public GameObject enemy;

    UnityEngine.AI.NavMeshAgent agent;

    public override void OnEnter()
    {
        soldier = sc.gameObject.GetComponent<Soldier>();
        agent = soldier.GetComponent<UnityEngine.AI.NavMeshAgent>();

        GameObject teamBase = GameObject.Find("Cat Base");
        agent.destination = teamBase.transform.position;
    }

    public override void OnUpdate()
    {
        //What does the guard do?
        enemyInRange = EnemyInRange();

        if (enemyInRange) {
            sc.AddNewState(new SoldierChase());
        }
    }
    
    public override void OnExit()
    {
        //This state never exits
    }

    public bool EnemyInRange() {
        enemy = sc.FindClosestEnemy(soldier.GetComponent<Soldier>().viewRange, soldier.GetComponent<Soldier>().teamNumber); //NEED TO FIGURE OUT WHAT TEAM SO WE KNOW IF ENEMY OR FRIEND

        Vector3 difference = enemy.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (soldier.GetComponent<Soldier>().viewRange * soldier.GetComponent<Soldier>().viewRange);
    }
}
