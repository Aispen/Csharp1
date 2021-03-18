using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static bool timeToStop = false;
    [SerializeField] public static float timeValue = 20f;
    Text timeUI;

    // Start is called before the first frame update
    void Start()
    {
        timeUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue <= 0)
        {
            timeToStop = true;
            print("timer reached 0 or bellow: " + timeValue);
            return;
        }
        timeValue -= Time.deltaTime;
        timeUI.text = "Time: " + System.String.Format("{0:0.00}",timeValue);
    }

}
