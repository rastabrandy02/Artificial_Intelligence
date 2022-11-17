using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform target;
    public float maxVelocity;
    public float maxRotation;
    public float acceleration;
    public float maxSpeed;
    public float turnAcceleration;
    public float maxTurnSpeed;
    public float stopDistance;

    private Vector3 direction;
    private Vector3 movement;
    Quaternion rotation;
    private float distance;
    private float freq = 0f;
    float turnSpeed = 0;
    float movSpeed = 0;


    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <
        stopDistance) return;


        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            Seek();
        }
        //KinematicSteer();
        DynamicSteer();
        

    }

    void KinematicSteer()
    {
        
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                                  Time.deltaTime * maxTurnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * Time.deltaTime;
        
    }
    void DynamicSteer()
    {
        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxSpeed);

        //direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                                  Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed * Time.deltaTime;
        
        
    }
    void Seek()
    {
        movement = direction.normalized * maxVelocity;
        direction = target.transform.position - transform.position;
        direction.y = 0f;    // (x, z): position in the floor

        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y
    }


}
