using UnityEngine;
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

    public bool yellows
    {
        set { yellowshould = value; } // keep accel when yellow before the line 50m
    }
   
    public override void score()
    {


        int a = 100;
        



       
            
            if (yellowshould == false)
            {
                a -= 25;
            }
            if (fa == true)
            {
                a = 0;
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
