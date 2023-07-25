using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public GameObject teamBase;
    // Start is called before the first frame update
    void Start()
    {
        teamBase = GameObject.Find("Cat Base");
        teamBase.GetComponent<TeamController>().IncreaseUnitCap(5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}