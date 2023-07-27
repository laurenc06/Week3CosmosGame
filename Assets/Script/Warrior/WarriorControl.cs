using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorControl : State
{
    Warrior warrior;

    public override void OnEnter()
    {
        doNotRemove = true;
        warrior = sc.gameObject.GetComponent<Warrior>();
    }

    public override void OnUpdate()
    {
        //What does the guard do?

        sc.AddNewState(new WarriorStill());
    }
    
    public override void OnExit()
    {
        //This state never exits
    }
}
