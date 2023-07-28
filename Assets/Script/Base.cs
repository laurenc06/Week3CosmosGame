using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabWorker;
    public bool createWorker;
    public Vector3 spawnPosition;
    public float spawnRate = 5f;
    public float lastSpawn = 0f;
    public int teamNumber;
    public GameObject teamBase;
    public GameObject[] teamBases;
    [SerializeField] public float health = 50f;
    public float maxHealth = 50f;

    [SerializeField] Health healthBar;

    void Awake() {
        healthBar = GetComponentInChildren<Health>();
    }

    void Start()
    {
        createWorker = false;
        spawnPosition = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5);
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == teamNumber){
                teamBase = teamBases[count];
            }
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawn += Time.deltaTime;
        if(createWorker && lastSpawn >= spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            CreateWorker(spawnPosition);
            createWorker = false;
            lastSpawn = 0;
            teamBase.GetComponent<TeamController>().IncreaseUnitNum(1);
        } else if (createWorker && lastSpawn < spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("wait");
        } else if (createWorker && teamBase.GetComponent<TeamController>().GetUnitCap() <= teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("no space");
            createWorker = false;
        }
    }

    public void CreateWorker(Vector3 spawnLocation){
        GameObject Worker = Object.Instantiate(prefabWorker, spawnLocation, Quaternion.identity);
        Debug.Log(GetComponent<TeamController>().teamNumber);
        Worker.GetComponent<WorkerScript>().teamNumber = GetComponent<TeamController>().teamNumber;
        Debug.Log(teamBase.name);
        teamBase.GetComponent<TeamController>().workerNum++;
    }

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
