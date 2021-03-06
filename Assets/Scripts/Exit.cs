using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("old") || other.CompareTag("young"))
        {
            Destroy(other.gameObject);
        }
    }
}
