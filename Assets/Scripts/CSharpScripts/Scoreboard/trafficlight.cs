using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class trafficlight : Intugate
{

    private List<string> lightcolor;
    private List<string> lanetoggle;
    private List<string> waythrough;
    private List<bool> Leftlight;
    private List<bool> rightlight;
    private List<bool> line;

    public trafficlight()
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

    public List<string> lightcl
    {
        set { lightcolor = value; }
    }

    public List<string> lanetog
    {
        set { lanetoggle = value; }
    }

    public List<string> waytho
    {
        set { waythrough = value; }
    }
    public List<bool> lined
    {
        set { line = value; }
    }
    public List<bool> sidelightL
    {
        set { Leftlight = value; }
    }

    public List<bool> sidelightR
    {
        set { rightlight = value; }
    }

    public override void score()
    {


        int a = 100;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;



        for (int i = 0; i < lightcolor.Count; i++)
        {
            if (lightcolor[i] == "red")
            {
                if (line[i] == true)
                {
                    a = 0;
                }
            }
            if (lanetoggle[i] == "left" && waythrough[i] != "left")
            {
                {
                    a -= 25;
                }
            }
            if (lanetoggle[i] == "right" && waythrough[i] != "right")
            {
                {
                    a -= 25;
                }
            }
            if (lanetoggle[i] == "straigth" && waythrough[i] != "straigth")
            {
                {
                    a -= 25;
                }
            }
            if (lanetoggle[i] == "left" && Leftlight[i] == false)
            {
                {
                    a -= 25;
                }
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
