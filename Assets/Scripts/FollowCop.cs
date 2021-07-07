using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCop : MonoBehaviour
{
   
    Vector3 offset;
    private void Start()
    {
        offset = new Vector3(transform.position.x, transform.position.y, -9f);
    }

    void LateUpdate()
    {
        Vector3 pos = PlayerController.instance.transform.position + offset;
        pos.z = offset.z;
        transform.position = pos;
    }
}
