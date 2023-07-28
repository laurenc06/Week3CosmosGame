using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherStill : State
{
    Archer archer;
    NavMeshAgent agent;
    GameObject target;
    public GameObject teamBase;
    public GameObject[] teamBases;

    public override void OnEnter()
    {
        Debug.Log("ArcherStill");
        archer = sc.gameObject.GetComponent<Archer>();
        agent = archer.GetComponent<NavMeshAgent>();
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == archer.GetComponent<Archer>().teamNumber){
                teamBase = teamBases[count];
            }
        }
        target = teamBase;
        FindBase();
    }

    public override void OnUpdate()
    {
        if (EnemyInRange()) {
            sc.AddNewState(new ArcherChase());
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
        GameObject enemy = sc.FindClosestEnemy(archer.GetComponent<Archer>().viewRange, archer.GetComponent<Archer>().teamNumber); 
        if (enemy == null) {
            return false;
        }
        Vector3 difference = enemy.transform.position - agent.transform.position;
        return difference.sqrMagnitude < (archer.GetComponent<Archer>().viewRange * archer.GetComponent<Archer>().viewRange);
    }
}