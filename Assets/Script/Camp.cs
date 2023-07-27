using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public GameObject teamBase;
    public GameObject[] teamBases;
    public int teamNumber;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 25;
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == teamNumber){
                teamBase = teamBases[count];
            }
        }
        teamBase.GetComponent<TeamController>().IncreaseUnitCap(5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}