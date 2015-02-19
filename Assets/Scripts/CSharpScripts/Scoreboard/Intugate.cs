using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Intugate  {

    private string Rulename = "";
    private string desc = "";
    private int sc = 0;

    

    abstract public int score();


    abstract public string loadname();

}
