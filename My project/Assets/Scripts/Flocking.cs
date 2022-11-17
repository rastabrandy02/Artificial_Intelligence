using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flocking : MonoBehaviour
{
    public FlockingManager myManager;
    public GameObject leader;
    public float minDelay;
    public float maxDelay;


    private float speed;
    private float delay;
    private float timePassed;

    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        delay = Random.Range(minDelay, maxDelay);
        timePassed = 0;
        
    }


    private void Update()
    {
        if(timePassed > delay)
        {
            FlockingRules();
            timePassed -= delay;
        }
        timePassed += Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(direction),
                                     myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        
    }

    // Update is called once per frame
    void FlockingRules()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 leaderDirection = leader.transform.position - this.transform.position;

        direction = Vector3.zero;


        int num = 0;
        foreach (GameObject go in myManager.allZombies)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flocking>().direction;
                    separation -= (transform.position - go.transform.position) /
                                  (distance * distance * 2);

                    num++;
                }
            }
            if (num > 0)
            {
                cohesion = (cohesion / num - transform.position).normalized * speed;
                align /= num;
                speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
            }
            Vector3 randomDirection = Vector3.zero;
            randomDirection.Set (Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));

            //direction = (cohesion + align + 20 *separation + randomDirection + leaderDirection / 10).normalized * speed;
            direction = (cohesion + align +  2*separation + randomDirection + leaderDirection).normalized * speed;
            direction.y = 0.0f;
        }
        


       

    }

}