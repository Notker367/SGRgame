using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("BooM" + other);
            //this.gameObject.AddComponent<Rigidbody>();
            Destroy(this.gameObject, 0.5f);
        }
    }


}
