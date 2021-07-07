using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static public CameraFollow instance;
    public Transform target;
    private Vector3 Offset;
    private float y;
    public float Speedfollow = 5f;
  
    [SerializeField] 
    private GameObject[] playerSkins;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        transform.Rotate(20.42f, 0, 0);
        
        foreach(GameObject go in playerSkins)
        {
            if(go.activeInHierarchy == true)
            {
                target = go.transform;
            }
        }
        Offset = new Vector3(0, transform.position.y, -2.3f);        
    }


    
    void LateUpdate()
    {
        Vector3 followPos = target.position + Offset;
        RaycastHit hit;
        if (Physics.Raycast(target.position, Vector3.down, out hit, 1.2f))
        {
            y = Mathf.Lerp(y, hit.point.y, Time.deltaTime * Speedfollow);
         
        }
        else
        {
            y = Mathf.Lerp(y, target.position.y, Time.deltaTime * Speedfollow);
         
        }
        followPos.y = Offset.y + y;
      
        transform.position = followPos;
    }
}
