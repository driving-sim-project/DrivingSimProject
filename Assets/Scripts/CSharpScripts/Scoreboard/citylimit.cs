using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Citylimit : Intugate
{
    private float avgsp;
    private float topsp;
    //private List<float> sp;
    public Citylimit()
    {
        Rulename = "City Limit Speed";
        picname = "";
        desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";
        sc = 0;
    }

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
    //public List<float> speed
    //{
    //    set { sp = value; }
    //}

    public override void score()
    {

        int a = 100;

        if (avgsp > 50)
        {
            a -= 25;
        }
        if (topsp > 50)
        {
            a -= 25;
        }
        if (isLooked == false)
            a -= 25;

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

    public override void GetData()
    {
        topspeed = UI.record.topSpeed;
        avgspeed = UI.record.avgSpeed;
    }
}
