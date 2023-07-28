using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldArea : MonoBehaviour
{
    [SerializeField] private float goldLeft = 120;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float takeGold(float workerRate) 
    {
        if (goldLeft > workerRate * Time.deltaTime) {
            goldLeft -= workerRate * Time.deltaTime;
            return workerRate;
        }
        else {
            Destroy(this.gameObject);
        }
        return 0;
    }
}
