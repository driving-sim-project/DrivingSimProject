using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Calculate {


    private float score = 0; 
    private List<int> gd = new List<int>();

    public List<int> loadgrade()
    {
        return gd;
    }
    public float loadscore()
    {
        return score;
    }

    public void calc(List<int> sco)
    {
        int a = 0;
        float b = 0;
        float c = 0;
        float d = 0;
        int e = 0;

        foreach(int i in sco)
        {
            a += i;
            c = i / 10;
            d = i % 10;
            e = (int)c*2-9;
            c += d;
             if(d>4)
            {
               if(c < 8 && c>4){
                
                e += 1;
                    }
            }
             if (c < 5)
             {
                 e = 0;
             }
           if (c>8)
{
    e = 7;
}
            gd.Add(e);
            
            
            
        }
        b = a / sco.Count;
        d = a % sco.Count;
        d += b;
        e = (int)b * 2 - 9;
        if (c < 8 && c > 4)
        {

            if (d > 4)
            {
                e += 1;
                
            }
        }
        else
        {
            if (c < 5)
            {
                e = 0;
            }
            else
            {
                e = 7;
            }
        }
        score=b;
        gd.Add(e);

        
    }

    
    public int analy(RecordedMotion rm)
    {
        int a = 0;
        int b = 0;
        int c = 0;
        int d= 0;
        int e = 0;
        List<int> f = new List<int>();
        f.Add(0);
        int g = 0;
        List<int> h = new List<int>();
        h.Add(0);
        int i = 0;
        int j = 0;
        float k = 0;        
        int l = rm.frames.Count / 20;
        int m = 0;
        foreach (RecordedFrame rmm in rm.frames)
        {
            if (rmm.isCrossing)
            {
                if(!(rmm.sidelightL || rmm.sidelightR))
                {
                    b += 1;
                }    
            }
            if(rmm.speed>80)
            {
                c += 1;
            }
            if(rmm.throttle>80)
            {   
                
                if(k  < 1)
                {
                    j++;
                }
                if (d != 0 && e > 0)
                {
                    if((d+1)!=i)
                    {
                        if (e > f[f.Count-1]) {

                            f.Add(e);
                            h.Add(i);
                        }
                        
                        e = 0;
                        d = i;
                    }
                    else
                    {
                        e++;
                        d=i;
                    }
                }
                else{
                    d = i;
                    e++;
                }
            }
            else
            {
                if(rmm.throttle<-79)
                {
                    m++;
                }
            }
            k = rmm.throttle;
            i++;
        }
        return a;

    }
}
