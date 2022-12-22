using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GuardingFSM : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(0.05f);   // 1 / 20
    delegate IEnumerator State();
    private State state;

    public NavMeshAgent agent;
    public float radius;
    public float offset;

    public GameObject targetToGuard;
    public float maxDistanceToTarget;
    public float minDistanceToTarget;
    float distanceToGuard;

    public GameObject targetToPursue;
   

    // Start is called before the first frame update
    IEnumerator Start()
    {

        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator Wander()
    {

        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0.0f;
        int debug = NavMesh.GetAreaFromName("Path");

        //Check if chest is open

        if (targetToGuard.GetComponent<Chest>().isOpen)
        {
            state = Pursue;
        }

        //Search for random destination

        if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {

            agent.destination = hit.position;

        }
        else
        {
            worldTarget = transform.TransformPoint(-localTarget);
            worldTarget.y = 0f;
            if (NavMesh.SamplePosition(worldTarget, out hit, radius, NavMesh.AllAreas))
            {

                agent.destination = hit.position;

            }

        }


        //If he is far away, he approaches the target
        if(Vector3.Distance(transform.position, targetToGuard.transform.position) > maxDistanceToTarget)
        {
            state = ApproachToTarget;
        }

        
        yield return wait;

    }
    IEnumerator ApproachToTarget()
    {
        agent.destination = targetToGuard.transform.position;
        if(Vector3.Distance(transform.position, targetToGuard.transform.position) < minDistanceToTarget)
        {
            state = Wander;
        }
        yield return wait;
    }

    IEnumerator Pursue()
    {
        agent.destination = targetToPursue.transform.position;
        yield return wait;
    }
}
