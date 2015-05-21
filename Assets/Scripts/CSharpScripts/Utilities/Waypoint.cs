using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public AiNode[] waypoints;

    void Awake()
    {
        waypoints = GetComponentsInChildren<AiNode>();
    }

}
