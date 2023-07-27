using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherControl : State
{
    Archer archer;

    public override void OnEnter()
    {
        doNotRemove = true;
        archer = sc.gameObject.GetComponent<Archer>();
    }

    public override void OnUpdate()
    {
        sc.AddNewState(new ArcherStill());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
