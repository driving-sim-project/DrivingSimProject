using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Intugate  {

    private string Rulename = "";
    private string picname = "";
    private string desc = "";
    private int sc = 0;

    

    abstract public int getscore();

    abstract public void score();

    abstract public string loadname();

    abstract public string loadpic();

    abstract public string loaddesc();


}
