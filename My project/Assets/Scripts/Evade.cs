using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Evade : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Transform lastTransform;
    
    public float searchRadius;
    public float changeDirRadius;
    

    public float maxFleeFrequency;
    private float fleeFrequency;

    Quaternion rotation;

    void Start()
    {
        //Vector3 targetDir = target.transform.position - transform.position;
        
        //float lookAhead = targetDir.magnitude / agent.speed;
        //Flee(target.transform.position + target.transform.forward * lookAhead);
        //lastTransform = agent.transform;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(transform.position, agent.destination, Color.magenta);
        if (fleeFrequency > maxFleeFrequency)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            
            float lookAhead = targetDir.magnitude / agent.speed;
            //Flee(target.transform.position + target.transform.forward * lookAhead);
            Flee(targetDir);

            fleeFrequency -= maxFleeFrequency;


        }
        fleeFrequency += Time.deltaTime;

        //if(NavMesh.SamplePosition(targetDir, out NavMeshHit hit, searchRadius, NavMesh.AllAreas))
        //{
        //    float lookAhead = targetDir.magnitude / agent.speed;
        //    Flee(target.transform.position + target.transform.forward * lookAhead);
        //}
        //else
        //{
        //   if(NavMesh.SamplePosition(agent.transform.position, out  hit, changeDirRadius, NavMesh.AllAreas))
        //   {
        //        Flee(hit.position);
        //   }
        //}




        


    }

    void Flee(Vector3 pos)
    {       
        //Vector3 fleeVector = pos - this.transform.position;
        agent.destination = this.transform.position - pos;
    }

    void ChangeDestination()
    {
        Vector3 worldTarget = transform.TransformPoint(target.transform.position);
        worldTarget.y = 0f;
        int debug = NavMesh.GetAreaFromName("Path");


        if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, searchRadius, NavMesh.AllAreas))
        {
            
            agent.destination = hit.position;

        }
        else
        {
            worldTarget = transform.TransformPoint(-target.transform.position);
            worldTarget.y = 0f;
            if (NavMesh.SamplePosition(worldTarget, out hit, searchRadius, NavMesh.AllAreas))
            {
                
                agent.destination = hit.position;

            }

        }

    }
}

