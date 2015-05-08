using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class crosslane : Intugate
{

    private bool iscrossing = false;
    bool wheelon = false;
    bool stop = false;
    bool longcross = false;
    List<float> speed;

    public crosslane()
    {
        Rulename = "Cross Lane";
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

   public bool iscross
    {
        set { iscrossing = value; }
    }
    
    public bool online
    {
        set { wheelon = value; }
    }

    public bool isStop
    {
        set { stop = value; }
    }

    public bool longCross
    {
        set { longcross = value; }
    }

    public override void score(  )
    {
        int a = 100;
        if (iscrossing == true)
            a -= 25;
        if (wheelon == true)
            a -= 25;
        if (longcross == true)
            a -= 25;
        if (stop == true)
            a = 0;
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
