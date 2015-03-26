using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

    public string direction = "";
    public List<Intugate> rules = new List<Intugate>();
    
    void Awake()
    {
        GetComponents<Intugate>(rules);
        Debug.Log(rules.Count);
    }

}
