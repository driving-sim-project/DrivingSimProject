using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class turn : Intugate
{
    private bool rightlane = true;
    private bool openlight = true;
    private bool laneintime = true;

    public turn()
    {
        Rulename = "turn";
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

    public bool rlane
    {
        set { rightlane = value; } // in the right lane to go through
    }
    public bool olight
    {
        set { openlight = value; } // open lightside when left turn or right turn and no light when go straight
    }
    public bool lit
    {
        set { laneintime = value; } // go in the right lane in 30 m before
    }

   

    public override void score()
    {

        int a = 100;
        
        if (rightlane == false)
        {
            a -= 25;
        }
        if (openlight == false)
        {
            a -= 25;
        }
        if (laneintime == false)
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

    public override void GetData()
    {
    }


}
