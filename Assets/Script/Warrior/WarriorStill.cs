using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorStill : State
{
    Warrior warrior;
    NavMeshAgent agent;
    GameObject target;
    public GameObject teamBase;
    public GameObject[] teamBases;

    public override void OnEnter()
    {
        Debug.Log("WarriorStill");
        warrior = sc.gameObject.GetComponent<Warrior>();
        agent = warrior.GetComponent<NavMeshAgent>();
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == warrior.GetComponent<Warrior>().teamNumber){
                teamBase = teamBases[count];
            }
        }
        target = teamBase;
        FindBase();
    }

    public override void OnUpdate()
    {
        if (EnemyInRange()) {
            sc.AddNewState(new WarriorChase());
        }
    }

    public void FindBase()
    {
        agent.destination = teamBase.transform.position;
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            Debug.Log("arrived");
        }
    }

    public bool EnemyInRange() {
        GameObject enemy = sc.FindClosestEnemy(warrior.GetComponent<Warrior>().viewRange, warrior.GetComponent<Warrior>().teamNumber); 
        if (enemy == null) {
            return false;
        }
        Vector3 difference = enemy.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (warrior.GetComponent<Warrior>().viewRange * warrior.GetComponent<Warrior>().viewRange);
    }
}
