using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] public GameObject[] prefabModels;
    public static StartGame instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Instantiate(prefabModels[Shop._shop.selectModel], Vector3.zero, Quaternion.identity);
       
    }
}
