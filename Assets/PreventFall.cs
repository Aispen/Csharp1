using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventFall : MonoBehaviour
{
    private float Xorigin;
    private float Zorigin;
    private float Height;

    private void Awake()
    {
        // Setting Startpoint
        Xorigin = transform.position.x;
        Zorigin = transform.position.z;
        Height = transform.position.y;
        // Asking Height
        if (Height < 0)
        {
            print("FALLPREVENT: Height is bellow zero! Setting secure height.");
            Height += 3;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Character respawns to the starting point when falling.
        if (transform.position.y < -3)
        {
            transform.position = new Vector3(Xorigin, Height, Zorigin);
        }

    }
}
