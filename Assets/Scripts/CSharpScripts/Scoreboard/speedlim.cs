using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class speedlim : Intugate
{

    private string Rulename = "Speedlimit";
    private string picname = "";
    private string desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";
    private int sc = 0;


    public override string loadpic()
    {
        return picname;
    }
    public override int score()
    {
        return sc;
    }

    public void score(RecordedMotion replayData)
    {
        int a = 100;

        if(replayData.avgSpeed>80)
        {
            a -= 50;
        }
        if(replayData.topSpeed>80)
        {
            a -= 30;
        }
        for (int i = 0; i < replayData.frames.Count; i++)
        {
            if(replayData.frames[i].speed>80)
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
