using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamThink : State
{
    // Start is called before the first frame update
    TeamController team;
    public float lastAction = 0;
    public float actionRate = 2;

    public override void OnEnter()
    {
        doNotRemove = true;
        team = sc.gameObject.GetComponent<TeamController>();
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        lastAction += Time.deltaTime;
        Debug.Log(team.GetComponent<TeamController>().teamNumber);
        if(lastAction > actionRate){
            //TeamThink;
        }

    }


}
