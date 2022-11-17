using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;

    public float maxSeekFrequency;
    private float seekFrequency;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      if(seekFrequency > maxSeekFrequency)
      {
            //if (agent.remainingDistance < 1.0f)
            //{
            //    Vector3 targetDir = target.transform.position - transform.position;
            //    float lookAhead = targetDir.magnitude / agent.speed;
            //    Seek(target.transform.position + target.transform.forward * lookAhead);

            //}
            Vector3 targetDir = target.transform.position - transform.position;
            float lookAhead = targetDir.magnitude / agent.speed;
            Seek(target.transform.position + target.transform.forward * lookAhead);



            seekFrequency -= maxSeekFrequency;


        }
        seekFrequency += Time.deltaTime;

    }

    void Seek(Vector3 worldTarget)
    {
        agent.destination = worldTarget;
    }
}
