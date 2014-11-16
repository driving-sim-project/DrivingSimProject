using UnityEngine;
using System.Collections;

public class LaneDetector : MonoBehaviour {

    static bool trigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (trigger == false && other.tag == "Car")
        {
            trigger = true;
            Debug.Log(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Car"){
            trigger = false;
            Debug.Log("Exit");
        }
    }
}
