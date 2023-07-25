using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    //Resources
    public float wood = 0;
    public float gold = 0;
    public int team;
    public int unitCap;
    public int unitNum;
    public GameObject prefabBarracks;
    public GameObject prefabCamp;
    public Vector3 spawnLocation;
    public bool createCamp;

    //Probably should have it's own FSM (state controller)

    // Start is called before the first frame update
    void Start()
    {
        unitCap = 5;
        createCamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(createCamp){
            spawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            CreateCamp(spawnLocation);
            createCamp = false;
        }
    }

    public void IncreaseUnitCap(int increase){
        unitCap += increase;
    }

    public void IncreaseUnitNum(int increase){
        unitNum += increase;
    }

    public int GetUnitCap(){
        return unitCap;
    }

    public int GetUnitNum(){
        return unitNum;
    }

    public void CreateBarracks( ){

    }

    public void CreateCamp(Vector3 spawnLocation){
        GameObject Camp = Object.Instantiate(prefabCamp, spawnLocation, Quaternion.identity);
    }
}
