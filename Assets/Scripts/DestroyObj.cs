using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other);
        Destroy(other.gameObject);
        
    }
}
