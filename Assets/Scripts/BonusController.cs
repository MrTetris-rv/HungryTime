using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    public int timeBonus = 15;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){

            PlayerController.instance.isBonus = true;
            transform.gameObject.SetActive(false);
        }
    }  
}
