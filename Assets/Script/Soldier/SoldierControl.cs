using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierControl : State
{
    Soldier soldier;

    public override void OnEnter()
    {
        doNotRemove = true;
        soldier = sc.gameObject.GetComponent<Soldier>();
    }

    public override void OnUpdate()
    {
        //What does the guard do?

        sc.AddNewState(new SoldierStill());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
