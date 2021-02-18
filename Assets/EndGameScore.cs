using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameScore : MonoBehaviour
{
    public static int endScore = 0;
    TMP_Text scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = endScore.ToString();
    }
}
