using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlockingManager : MonoBehaviour
{
    public GameObject zombie;
    public int numZombies;
    public GameObject [] allZombies;
    public float neighbourDistance;


    public float minOffset;
    public float maxOffset;
    public float minSpeed;
    public float maxSpeed;
    public float rotationSpeed;

   
    


    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        allZombies = new GameObject[numZombies];
        for (int i = 0; i < numZombies; ++i)
        {
            offset = Random.Range(minOffset, maxOffset);

            Vector3 pos = this.transform.position + Random.insideUnitSphere * offset;
            pos.y = this.transform.position.y + 0.5f;
            Vector3 randomize;
            
            randomize.x = Random.value;
            //randomize.y = this.transform.position.y;
            randomize.y = 0.0f;
            
            randomize.z = Random.value;


            allZombies[i] = Instantiate(zombie, pos, Quaternion.LookRotation(randomize));
           
            allZombies[i].GetComponent<Flocking>().myManager = this;
            allZombies[i].GetComponent<Flocking>().leader = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
