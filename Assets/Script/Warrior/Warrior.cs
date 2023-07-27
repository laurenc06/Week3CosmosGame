using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    StateController controller;
    public float viewRange = 40f;
    public float attackRange = 2.5f;
    public float health = 10f;
    public int teamNumber = 1;
    public int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new WarriorControl());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public int getTeam() {
        return teamNumber;
    }

    public float getAttackRange() {
        return attackRange;
    }

    public int getDamage() {
        return damage;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, this.viewRange);
    }
}
