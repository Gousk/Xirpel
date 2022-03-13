using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Crash");
        GetComponent<MeshRenderer>().enabled = false;    
    }
}
