using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class crosslane : Intugate
{

    private List<bool> iscrossing;
    private List<bool> Leftlight;
    private List<bool> rightlight;

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

   public List<bool> iscross
    {
        set { iscrossing = value; }
    }
    
    public List<bool> sidelightL
   {
       set { Leftlight = value; }
   }

    public List<bool> sidelightR
    {
        set { rightlight = value; }
    }

    public override void score(  )
    {
  

        int a = 100;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;

        for (int i = 0; i < iscrossing.Count;i++ )
        {
            Debug.Log(i);
            Debug.Log(iscrossing[i]);
            if (iscrossing[i] && (Leftlight[i] || rightlight[i]))
            {
                Debug.Log("light : "+ iscrossing[i]);
                b.Add(f);
            }
            else
            {
                if (iscrossing[i]==true)
                {
                    Debug.Log(iscrossing[i]);
                    if (d != 0 && c > 0)
                    {
                        if ((d + 1) != i)
                        {
                            if (c > e)
                            {
                                e = c;
                            }

                            c = 0;
                            d = i;
                        }
                        else
                        {
                            c++;
                            d = i;
                        }
                    }
                    else
                    {
                        Debug.Log("no : "+iscrossing[i]);
                        c++;
                        d = i;
                    }
                }
            }
          
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
