using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeCount : MonoBehaviour
{
    float timer = 0;
    public Text textTimer;
    private void Start()
    {
        textTimer.text = timer.ToString();
    }
    private void Update()
    {
        timer = timer + 2 * Time.deltaTime;
        textTimer.text = Mathf.Round(timer).ToString() + " m";
    }

 
}
