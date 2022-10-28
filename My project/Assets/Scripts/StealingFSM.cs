using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StealingFSM : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(0.05f);   // 1 / 20
    delegate IEnumerator State();
    private State state;

    public NavMeshAgent agent;
    public float radius;
    public float offset;

    public GameObject guard;
    public GameObject targetToSteal;
    public float maxDistanceToGuard;

    public float minDistanceToTarget;
    bool isCloseToTarget;
   
    IEnumerator Start()
    {
        
        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    void Update()
    {

    }
    IEnumerator Wander()
    {
        //Wander
            Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
            localTarget += new Vector3(0, 0, offset);
            Vector3 worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0.0f;
            agent.speed = 3.5f;


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

         //Check if guard is guarding the target
        if(Vector3.Distance(guard.transform.position, transform.position) > maxDistanceToGuard)
        {
            state = ApproachToSteal;
        }

        yield return wait;

    }

    IEnumerator ApproachToSteal()
    {
        //Check if guard is guarding the target
        if (Vector3.Distance(guard.transform.position, targetToSteal.transform.position) > maxDistanceToGuard)
        {
            agent.destination = targetToSteal.transform.position;
            agent.speed = 6;
        }
        else state = Wander;

        //Check if agent is close to target
        if (Vector3.Distance(transform.position, targetToSteal.transform.position) < minDistanceToTarget)
        {
            isCloseToTarget = true;
            state = Steal;
        }
        else isCloseToTarget = false;
        yield return wait;
    }
    IEnumerator Steal()
    {
        //Stay still near target
        agent.destination = transform.position;

        if (targetToSteal.GetComponent<Chest>().isOpen == false) targetToSteal.GetComponent<Chest>().hasToOpen = true;

        else state = Flee;
        yield return wait;
    }
    IEnumerator Flee()
    {
        Debug.DrawLine(transform.position, agent.destination, Color.magenta);
       
            Vector3 targetDir = guard.transform.position - transform.position;

            float lookAhead = targetDir.magnitude / agent.speed;
        
        agent.destination = this.transform.position - targetDir;



        yield return wait;
    }
}


