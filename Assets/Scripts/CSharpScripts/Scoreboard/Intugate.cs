using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Intugate  {

    protected string Rulename = "";
    protected string picname = "";
    protected string desc = "";
    protected int sc = 0;
    protected bool fa = false;
    protected int objID = 0;

    abstract public int getscore();

    abstract public void score();

    abstract public string loadname();

    abstract public string loadpic();

    abstract public string loaddesc();

    public bool failed
    {
        set { fa = value; }
        get { return fa; }
    }

    public int setRefObj
    {
        set { objID = value; }
        get { return objID; }
    }
}
