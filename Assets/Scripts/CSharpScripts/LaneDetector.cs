﻿using UnityEngine;
using System.Collections;

public class LaneDetector : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
            Debug.Log(other.tag);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
