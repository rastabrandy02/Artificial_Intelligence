using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class DoctorAgent : Agent
{
    public float forceMultiplier;
    public GameObject target;
    Rigidbody rb;
    public GameObject[] checkPoints;
    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    // Agent Start
    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (transform.localPosition.y < -2.0f)
        {

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.localPosition = new Vector3(10, 4.0f, 85);
            SetReward(-1.0f);
            EndEpisode();
        }
        // Move the target to a new random spot
        
        int checkpointIndex = Random.Range(0, 5);
        
        Vector3 checkpointPos = checkPoints[checkpointIndex].transform.position;
        target.transform.localPosition = checkpointPos;


    }
    // Episode Start
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions, size = 3x2
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(transform.localPosition);
        // Agent velocity, size = 2
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);

        if (transform.localPosition.y < -2.0f)
        {

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.localPosition = new Vector3(10, 4.0f, 85);
            SetReward(-1.0f);
            EndEpisode();
        }
    }
    // sensor.AddObservation(item);
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rb.AddForce(controlSignal * forceMultiplier);
        
    }
    // Called when and action is received from either
    // the player input or the neural network
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = -Input.GetAxis("Horizontal");
        continuousActionsOut[1] = -Input.GetAxis("Vertical");
    }
    // Player control instead of neural network

    
    void OnTriggerEnter(Collider col)
    {
        // Reached target
        if (col.transform.CompareTag("Nurse"))
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if (col.transform.CompareTag("Default"))
        {
            SetReward(-0.01f);
            
        }
        if (col.transform.CompareTag("Obstacle"))
        {
            SetReward(-0.1f);
           

        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("Default"))
        {
            SetReward(-0.01f);

        }
        if (col.transform.CompareTag("Obstacle"))
        {
            SetReward(-0.1f);
            

        }
    }
    void OnCollisionEnter(Collision col)
    {
        // Reached target
        if (col.transform.CompareTag("Nurse"))
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if (col.transform.CompareTag("Obstacle"))
        {
            SetReward(-0.1f);
            

        }
        if (col.transform.CompareTag("Default"))
        {
            SetReward(-0.01f);

        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.transform.CompareTag("Default"))
        {
            SetReward(-0.01f);
            

        }
        if (col.transform.CompareTag("Obstacle"))
        {
            SetReward(-0.1f);
            

        }
    }
}
