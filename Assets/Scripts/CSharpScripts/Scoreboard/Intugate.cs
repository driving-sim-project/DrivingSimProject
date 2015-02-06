using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Intugate  {

    private string Rulename = "";
    private bool sign = false;
    private string desc = "";
    private bool leftview = false;
    private bool rightview = false;
    private bool backview = false;
    private bool emergencylight = false;
    private bool leftlight = false;
    private bool rightlight = false;
    private float speed = 0;
    private bool turn = false;
    private bool RGW = false;
    private float disbe = 0;


    abstract public void get();

    abstract public int loadin(string name);

    abstract public string loadst(string name);

    abstract public bool loadbo(string name);

    abstract public float loadflo(string name);

    abstract public string loadname();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
