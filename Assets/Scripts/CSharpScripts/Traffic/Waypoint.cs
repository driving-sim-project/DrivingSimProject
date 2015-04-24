using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public Collider[] waypoints;

    void Awake()
    {
        waypoints = GetComponentsInChildren<Collider>();
    }

}
