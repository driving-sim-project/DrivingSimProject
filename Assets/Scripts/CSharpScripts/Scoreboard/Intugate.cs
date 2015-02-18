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
    

    abstract public int score();


    abstract public string loadname();

}
