using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentCollectCoins : Agent
{
    [SerializeField] MovementSettings movementSettings;
    CharacterController controller;
    float moveSpeed;
    float rotationSpeed;

    public override void Initialize()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = movementSettings.movementSpeed;
        rotationSpeed = movementSettings.rotationSpeed;
    }

    public override void OnEpisodeBegin()
    {
        controller.enabled = false;
        controller.transform.localPosition = Vector3.zero;
        controller.enabled = true;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.Rotate(Vector3.up * moveX * rotationSpeed);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * moveZ;
        controller.SimpleMove(forward*curSpeed);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9) // Coin
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue+=10;
            SpawnScript.currentSpawnedCoins--;
            AddReward(1f);
        }
        if (other.gameObject.layer == 10) // Bomb
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue -= 5;
            SpawnScript.currentSpawnedBombs--;
            AddReward(-5f);
        }
        if (other.TryGetComponent<FallingOff>(out FallingOff offMap))
        {
            AddReward(-3f);
            //controller.transform.position = Vector3.zero;
            SpawnScript.ResetCollectibles();
            ResetPos();
        }
        Debug.Log(GetCumulativeReward());
    }

    private void ResetPos()
    {
        controller.enabled = false;
        controller.transform.localPosition = Vector3.zero;
        controller.enabled = true;
    }
}
