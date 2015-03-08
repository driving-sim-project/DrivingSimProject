using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class speedlim : Intugate
{

    private string Rulename = "Speedlimit";
    private string picname = "";
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
        set{topsp = value;}
    }
    public List<float> speed
    {
        set{sp = value;}
    }

    public override void score()
    {
        int a = 100;

        if(avgsp>80)
        {
            a -= 50;
        }
        if(topsp>80)
        {
            a -= 30;
        }
        foreach (float i in sp)
        {
            if(i>80)
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
