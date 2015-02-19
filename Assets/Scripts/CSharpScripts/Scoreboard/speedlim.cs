using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class speedlim : Intugate
{

    private string Rulename = "Speedlimit";
    private string desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";


    public override int score()
    {
        throw new System.NotImplementedException();
    }

    public int score(RecordedMotion replayData)
    {
        int a = 100;
        if(replayData.avgSpeed>this.speed)
        {
            a -= 50;
        }
        if(replayData.topSpeed>this.speed)
        {
            a -= 30;
        }
        return a;
    }

   

    public override string loadname()
    {
        return Rulename;
    }
}
