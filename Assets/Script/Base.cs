using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabWorker;
    public bool create;
    public Vector3 spawnPosition;
    public float spawnRate = 5f;
    public float lastSpawn = 0f;
    public GameObject teamBase;

    void Start()
    {
        create = false;
        spawnPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10);
        teamBase = GameObject.Find("Cat Base");
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;
        if(create && lastSpawn >= spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            CreateWorker(spawnPosition);
            create = false;
            lastSpawn = 0;
            teamBase.GetComponent<TeamController>().IncreaseUnitNum(1);
        } else if (create && lastSpawn < spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("wait");
        } else if (create && teamBase.GetComponent<TeamController>().GetUnitCap() <= teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("no space");
            create = false;
        }
    }

    public void CreateWorker(Vector3 spawnLocation){
        GameObject Worker = Object.Instantiate(prefabWorker, spawnLocation, Quaternion.identity);
    }
}
