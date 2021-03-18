using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]MovementSettings movementSettings;
    CharacterController controller;
    float moveSpeed;
    float rotationSpeed;
    private float moveX;
    private float moveZ;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = movementSettings.movementSpeed;
        rotationSpeed = movementSettings.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal"); // rotation
        moveZ = Input.GetAxis("Vertical"); // movement
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * moveX * rotationSpeed);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * moveZ;
        controller.SimpleMove(forward * curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            SpawnScript.currentSpawnedCoins--;
            ScoreScript.scoreValue += 10;
        }
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue -= 5;
            SpawnScript.currentSpawnedBombs--;
        }
        if (other.TryGetComponent<FallingOff>(out FallingOff offMap))
        {
            ResetPositionZero();
        }
    }
    public void ResetPositionZero()
    {
        controller.enabled = false;
        controller.transform.position = Vector3.zero;
        controller.enabled = true;
    }
}
