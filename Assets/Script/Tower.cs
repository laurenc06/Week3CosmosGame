using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

using TMPro;

public class Tower : MonoBehaviour
{
    //public Tower tower;
    public GameObject target;
    public float attackRange = 3f;
    public float fireRate = 2.0f;
    public float lastFire = 0f;
    public int teamNumber;
    public int damage = 1;
    [SerializeField] public float health = 20f;
    public float maxHealth = 15f;
    public GameObject teamBase;
    public GameObject[] teamBases;

    [SerializeField] Health healthBar;

    void Awake() {
        healthBar = GetComponentInChildren<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == teamNumber){
                teamBase = teamBases[count];
            }
        }
    }


       // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.rotation.y, 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        lastFire += Time.deltaTime;
        target = FindClosestEnemy(); 

        if (target != null)
        {
            //Look at it
            transform.LookAt(target.transform);

            if (lastFire >= fireRate)
            {
                attack();
                lastFire = 0;
            }
        }
    } 

    public void attack() {
        if (target.CompareTag("Warrior")) target.GetComponent<Warrior>().takeDamage(damage); 
        if (target.CompareTag("Archer")) target.GetComponent<Archer>().takeDamage(damage); 
        if (target.CompareTag("Worker")) target.GetComponent<WorkerScript>().takeDamage(damage); 
        
        //Reset lastAttack
        lastFire = 0;

    } 

    public GameObject FindClosestEnemy()
    {
        //brute force MWAHAHA

        GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("Worker");  //Fill array with all gameobjects with the tag
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("Warrior");  //Fill array with all gameobjects with the tag
        GameObject[] gameObjects3 = GameObject.FindGameObjectsWithTag("Archer");  //Fill array with all gameobjects with the tag

        GameObject closest = null;                          //result: starts at null just in case we don't find anything in range

        float distance = this.attackRange * this.attackRange;         //square the distance
        Vector3 position = transform.position;              //our current position

        foreach (GameObject obj in gameObjects1)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance && obj.GetComponent<WorkerScript>().teamNumber != this.teamNumber)                     //comparing the squared distances
            {
                closest = obj;                              //new closest object set
                distance = curDistance;                     //distance to the object saved for next comparison in loop
            }
        }

        foreach (GameObject obj in gameObjects2)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance && obj.GetComponent<Warrior>().teamNumber != this.teamNumber)                     //comparing the squared distances
            {
                closest = obj;                              //new closest object set
                distance = curDistance;                     //distance to the object saved for next comparison in loop
            }
        }

        foreach (GameObject obj in gameObjects3)
        {
            Vector3 difference = obj.transform.position - position; //calculate the difference to the object from our current position
            float curDistance = difference.sqrMagnitude;    //distance requires a square root, which is slow, just using the squared magnitued

            if (curDistance < distance && obj.GetComponent<Archer>().teamNumber != this.teamNumber)                     //comparing the squared distances
            {
                closest = obj;                              //new closest object set
                distance = curDistance;                     //distance to the object saved for next comparison in loop
            }
        }

        //Return the obejct we found, or null if we didn't find anything
        return closest;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, this.attackRange);
    }

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}

