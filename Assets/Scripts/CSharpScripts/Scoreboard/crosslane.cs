using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class crosslane : Intugate
{

    private string Rulename = "Cross Lane";
    private string desc = "ขับรถในลักษณะกีดขวางการจราจร \n\n ปรับตั้งแต่ 400 – 1,000 บาท";
    private int sc = 0;

    public override int score()
    {
        return sc;
    }


    public void score( RecordedFrame[] replayRange )
    {
        int a = 100;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;

        foreach (RecordedFrame rm in replayRange)
        {
            if (rm.isCrossing && (rm.sidelightL || rm.sidelightR))
            {
                b.Add(f);
            }
            else
            {
                if (rm.isCrossing)
                {
                    a -= 1;
                    if (d != 0 && c > 0)
                    {
                        if ((d + 1) != f)
                        {
                            if (c > e)
                            {
                                e = c;
                            }

                            c = 0;
                            d = f;
                        }
                        else
                        {
                            c++;
                            d = f;
                        }
                    }
                    else
                    {
                        c++;
                        d = f;
                    }
                }
            }
            f++;
        }
        if (e > 4)
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
