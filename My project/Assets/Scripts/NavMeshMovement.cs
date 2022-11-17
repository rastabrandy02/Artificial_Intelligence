using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;
    public GameObject target;

    public float radius;
    public float offset;
    
    void Start()
    {
        //Seek();
        Wander();
        //SeekTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 2.5f)
        {
            Wander();

        }

    }

    void Seek(Vector3 worldTarget)
    {
        agent.destination = worldTarget;

    }
    void SeekTarget()
    {
        agent.destination = target.transform.position;

    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0.0f;
        int debug = NavMesh.GetAreaFromName("Path");

        
        if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            //Debug.Log("Found target");
            Seek(hit.position);
            
        }
        else
        {
            worldTarget = transform.TransformPoint(-localTarget);
            worldTarget.y = 0f;
            if (NavMesh.SamplePosition(worldTarget, out hit, radius, NavMesh.AllAreas))
            {
                //Debug.Log("Found target");
                Seek(hit.position);
               
            }
            
        }

    }
}
