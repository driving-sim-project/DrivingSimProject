using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class speedlim : Intugate
{

    private string Rulename = "Speedlimit";
    private bool sign = true;
    private string desc = "ขับรถเร็วเกินอัตรากำหนด \n \n ปรับตั้งแต่ 200 - 500 บาท";
    private bool leftview = false;
    private bool rightview = false;
    private bool backview = false;
    private bool emergencylight = false;
    private bool leftlight = false;
    private bool rightlight = false;
    private float speed = 80;
    private bool turn = false;
    private bool RGW = false;
    private float disbe = 0;

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
