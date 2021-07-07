using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    float rotationSpeed = 100;

    void Start()
    {
        rotationSpeed += Random.Range(0, rotationSpeed / 4.0f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         
            transform.gameObject.SetActive(false);
        }
    }
}
