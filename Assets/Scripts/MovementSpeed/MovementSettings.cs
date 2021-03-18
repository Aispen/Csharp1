using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Movement Setting", menuName = "Movement Setting")]
public class MovementSettings : ScriptableObject
{
    public float movementSpeed;
    public float rotationSpeed;
}
