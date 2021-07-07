using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCount : MonoBehaviour
{
    public Text distanceText;
    private Transform player;
    int score = 0;

    private void Start()
    {
        player = CameraFollow.instance.target;
        score = 0;
        distanceText.text = "0";
    }

    private void Update()
    {
        score = (score +1) * (int)Time.deltaTime;
        distanceText.text = score.ToString();
    }
}
