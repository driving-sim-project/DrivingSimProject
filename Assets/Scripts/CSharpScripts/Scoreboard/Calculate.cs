using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Calculate {


    private List<float> speed = new List<float>();
    private float topspeed;
    private float avgspeed;
    private List<float> distance;
    private List<bool> leftlight;
    private List<bool> rightlight;
    private int time;
    private List<int> throttle;
    private List<int> brake;
    private List<string> lookat = new List<string>();
    private List<bool> lanetog;
    private List<float> gazing = new List<float>();
    private string lane;
    private List<string> rulen = new List<string>();
    private List<string> rgw = new List<string>();
    private List<float> score = new List<float>();
    private float limsp = 80;
    private List<string> desc = new List<string>();



    public void calc(List<int> sco)
    {
        int a = 0;
        float b = 0;
        float c = 0;
        float d = 0;

        foreach(int i in sco)
        {
            a += i;
            c = i / 10;
            d = i % 10;
            if(d>4)
            {
                c += (d / 10); 
            }
            score.Add(c);
            
        }
        b = a / sco.Count;
        score.Add(b);

        
    }
    
    public int analy()
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
        int k = 0;        
        int l = speed.Count / 20;
        for (int i = 0; i < speed.Count; i++)
        {
            if (lanetog[i])
            {
                if(!(leftlight[i] || rightlight[i]))
                {
                    b += 1;
                }    
            }
            if(speed[i]>limsp)
            {
                c += 1;
            }
            //if()
            //{

            //}
            if(throttle[i]>80)
            {
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
                if(brake[i]>80)
                {
                    if((d+1)==i)
                    {
                     
                    }
                }
                else
                {

                }
            }
        }
        return a;

    }
}
