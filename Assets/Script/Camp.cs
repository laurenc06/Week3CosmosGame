using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    public GameObject teamBase;
    public GameObject[] teamBases;
    public int teamNumber;
    [SerializeField] public float health = 15f;
    public float maxHealth = 15f;

    [SerializeField] Health healthBar;

    void Awake() {
        healthBar = GetComponentInChildren<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        teamBases = GameObject.FindGameObjectsWithTag("Base");
        for(int count = 0; count < teamBases.Length; count++){
            if(teamBases[count].GetComponent<TeamController>().teamNumber == teamNumber){
                teamBase = teamBases[count];
            }
        }
        teamBase.GetComponent<TeamController>().IncreaseUnitCap(5);
        healthBar.UpdateHealthBar(health, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}