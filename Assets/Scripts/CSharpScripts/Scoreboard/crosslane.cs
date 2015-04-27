using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class crosslane : Intugate
{

    private List<bool> iscrossing;
    private List<bool> Leftlight;
    private List<bool> rightlight;
    bool wheelon = false;
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
    public bool online
    {
        set { wheelon = value; }
    }
    public List<float> sp
    {
        set { speed = value; }
    }
    

    public override void score(  )
    {
  

        int a = 100;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;
        int g = 0;
        int h = 0;

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
                        c++;
                        d = i;
                    }
                    if (g != 0 && f > 0 && speed[i] < 20 )
                    {
                        if ((g + 1) != i)
                        {
                            if (f > h)
                            {
                                h = f;
                            }
                            f = 0;
                            g = i;
                        }
                        else
                        {
                            f++;
                            g = i;
                        }
                    }
                    else
                    {
                        c++;
                        d = i;
                    }
                    
                    
                }
            }
          
        }
        if(wheelon == true)
        {
            a -= 25;
        }
        if (e > 4)
        {
            a -= 25;
        }
        if (h > 4)
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
