using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Citylimit : Intugate
{

    private string Rulename = "City Limit Speed";
    private string picname = "citylimit";
    private string desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";
    private float avgsp;
    private float topsp;
    private List<float> sp;
    private int sc = 0;


    public override string loadpic()
    {
        return picname;
    }
    public override int getscore()
    {
        return sc;
    }
    public float avgspeed
    {
        set { avgsp = value; }
    }
    public float topspeed
    {
        set { topsp = value; }
    }
    public List<float> speed
    {
        set { sp = value; }
    }

    public override void score()
    {
        Debug.Log(sp);
        Debug.Log(avgsp);
        Debug.Log(topsp);
        int a = 100;

        if (avgsp > 50)
        {
            a -= 50;
        }
        if (topsp > 50)
        {
            a -= 30;
        }
        foreach (float i in sp)
        {
            if (i > 50)
            {
                a -= 1;
            }
        }
        sc = a;
    }



    public override string loadname()
    {
        return Rulename;
    }

    public override string loaddesc()
    {
        return desc;
    }
}
