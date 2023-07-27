using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    StateController controller;
    public float viewRange = 40f;
    public int teamNumber = 1;
    public float attackRange = 8f;
    public float health = 7f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
        controller.ChangeState(new ArcherControl());
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
