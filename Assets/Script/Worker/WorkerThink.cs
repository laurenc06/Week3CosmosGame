using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerThink : State
{
    Worker worker;

    public override void OnEnter()
    {
        doNotRemove = true;
        worker = sc.gameObject.GetComponent<Worker>();
    }

    public override void OnUpdate()
    {
        //Any enemies nearby? if so, return to base
        //Any resoruces collected? if so, return to base
        //Otherwise, go find some resources
        //which type? what resource is most needed? AI decision.
        //If no resource in range (fog of war), go hunt for some

        //For now, just go find stuff
        Debug.Log("adding WorkerGather()");
        sc.AddNewState(new WorkerGather());
    }

    public override void OnExit()
    {
        //This state never exits
    }
}
