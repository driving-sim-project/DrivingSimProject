﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class trafficlight : Intugate
{

    private bool lightinfringe;
    private bool yellowshould;


    public trafficlight()
    {
        Rulename = "Traffic Light";
        picname = "";
        desc = "ขับรถในลักษณะกีดขวางการจราจร \n\n ปรับตั้งแต่ 400 – 1,000 บาท";
        sc = 0;
    }

    public override int getscore()
    {
        return sc;
    }

    public override string loadpic()
    {
        return picname;
    }

    public bool infringe
    {
        set { lightinfringe = value; } // infringe red light
    }
    public bool yellows
    {
        set { yellowshould = value; } // keep accel when yellow before the line 50m
    }
   
    public override void score()
    {


        int a = 100;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;



       
            if (lightinfringe == true)
            {
                a = 0;
            }
            if (yellowshould == false)
            {
                a -= 25;
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
