using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private bool _jumpState;
    private float _horizontalInput;
    private float _verticalInput;
    private new Rigidbody rigidbody;
    private int _superJumpsRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpState = true;
        }

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(_horizontalInput * 3, rigidbody.velocity.y, _verticalInput * 3);

        // Dont allow multijump
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        // Jumping
        if (_jumpState == true)
        {
            float jumpPower = 5f;
            if(_superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                _superJumpsRemaining--;
            }

            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            _jumpState = false;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            SpawnScript.currentSpawnedCoins--;
            ScoreScript.scoreValue+=10;
            _superJumpsRemaining++;
        }
        if(other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue-=5;
            SpawnScript.currentSpawnedBombs--;
        }
    }

}
