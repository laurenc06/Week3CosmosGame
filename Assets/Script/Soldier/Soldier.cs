using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    StateController controller;
    public float viewRange = 10;
    public int teamNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new SoldierControl());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
