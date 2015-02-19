using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class speedlim : Intugate
{

    private string Rulename = "Speedlimit";
    private string desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";
    private int sc = 0;


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
        sc = a;
    }

   

    public override string loadname()
    {
        return Rulename;
    }
}
