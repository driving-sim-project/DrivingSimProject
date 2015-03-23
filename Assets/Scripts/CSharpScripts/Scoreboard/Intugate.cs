using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Intugate : MonoBehaviour  {

    protected string Rulename = "";
    protected string picname = "";
    protected string desc = "";
    protected int sc = 0;
    protected bool fa = false;
    public Transform setRefObj;

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

}
