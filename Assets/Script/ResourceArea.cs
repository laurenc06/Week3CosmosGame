using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceArea : MonoBehaviour
{
    [SerializeField] private float resourceLeft = 80;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float takeResource(float workerRate) 
    {
        if (resourceLeft > workerRate * Time.deltaTime) {
            resourceLeft -= workerRate * Time.deltaTime;
            return workerRate;
        }
        else {
            Destroy(this.gameObject);
        }
        return 0;
    }
}
