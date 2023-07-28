using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefabWarrior;
    public GameObject prefabArcher;
    public bool createWarrior;
    public bool createArcher;
    public Vector3 spawnPosition;
    public float spawnRate = 5f;
    public float lastSpawn = 0f;
    public int teamNumber;
    [SerializeField] public float health = 25f;
    public float maxHealth = 15f;
    [SerializeField] Health healthBar;
    public GameObject teamBase;
    public GameObject[] teamBases;

    void Awake() {
        healthBar = GetComponentInChildren<Health>();
    }

    void Start()
    {
        createWarrior = false;
        createArcher = false;
        spawnPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
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
        if(createWarrior && lastSpawn >= spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            CreateWarrior(spawnPosition);
            createWarrior = false;
            lastSpawn = 0;
            teamBase.GetComponent<TeamController>().IncreaseUnitNum(1);
        } else if (createArcher && lastSpawn >= spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            CreateArcher(spawnPosition);
            createArcher = false;
            lastSpawn = 0;
        } else if (createWarrior && lastSpawn < spawnRate && teamBase.GetComponent<TeamController>().GetUnitCap() > teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("wait");
        } else if (createWarrior && teamBase.GetComponent<TeamController>().GetUnitCap() <= teamBase.GetComponent<TeamController>().GetUnitNum()){
            Debug.Log("no space");
            createWarrior = false;
        }
    }

    public void CreateWarrior(Vector3 spawnLocation){
        GameObject Warrior = Object.Instantiate(prefabWarrior, spawnLocation, Quaternion.identity);
        Warrior.GetComponent<Warrior>().teamNumber = teamNumber;
        teamBase.GetComponent<TeamController>().warriorNum++;
    }

    public void CreateArcher(Vector3 spawnLocation){
        GameObject Archer = Object.Instantiate(prefabArcher, spawnLocation, Quaternion.identity);
        Archer.GetComponent<Archer>().teamNumber = teamNumber;
        teamBase.GetComponent<TeamController>().archerNum++;
    }

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}

