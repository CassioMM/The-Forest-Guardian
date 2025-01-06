using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockT : MonoBehaviour
{
    float zRotation;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zRotation = Mathf.Clamp(zRotation, 0, 0);


        transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
}
