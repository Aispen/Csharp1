using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentMoveToGoal : Agent
{
    [SerializeField] private Transform targetTransform;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(6,14),4,Random.Range(1,-6));
        targetTransform.localPosition = new Vector3(Random.Range(-7, 0), 6.5f, Random.Range(-1, -8));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        //float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime ;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<AgentGoal>(out AgentGoal goal))
        {
            SetReward(1f);
            EndEpisode();
        }
        if(other.TryGetComponent<OffMap>(out OffMap offmap))
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
