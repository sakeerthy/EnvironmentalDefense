using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    // Use this for initialization
    public Text scoreTextB;
    public float time_left;

    void Start()
    {
        UpdateTime();
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        
        time_left -= Time.deltaTime;
        if (time_left <= 0.0f)
            scoreTextB.text = "You Win!";
        else
            scoreTextB.text = "Time Left: " + Mathf.RoundToInt(time_left);

    }
}
