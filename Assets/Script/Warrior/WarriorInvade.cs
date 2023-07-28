using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorInvade : State
{
    Warrior warrior;
    NavMeshAgent agent;
    GameObject target;
    public GameObject teamBase;
    public GameObject[] teamBases;

    public override void OnEnter()
    {
        Debug.Log("WarriorInvade");
        warrior = sc.gameObject.GetComponent<Warrior>();
        agent = warrior.GetComponent<NavMeshAgent>();
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber != warrior.GetComponent<Warrior>().teamNumber){
                teamBase = teamBases[count];
            }
        }
        target = teamBase;
        Debug.Log(teamBase.name);
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
        Vector3 warriorDestination;
        if(warrior.GetComponent<Warrior>().teamNumber == 0){
            warriorDestination = new Vector3(teamBase.transform.position.x + 17.5f, teamBase.transform.position.y, teamBase.transform.position.z + 17.5f);
        } else {
            warriorDestination = new Vector3(teamBase.transform.position.x - 17.5f, teamBase.transform.position.y, teamBase.transform.position.z - 17.5f);
        }
        agent.destination = warriorDestination;
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
