using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Intugate))]
public class Checkpoint : MonoBehaviour {

    public List<Intugate> rules = new List<Intugate>();

    void Awake()
    {
        GetComponents<Intugate>(rules);
        Debug.Log(rules.Count);
    }

}
