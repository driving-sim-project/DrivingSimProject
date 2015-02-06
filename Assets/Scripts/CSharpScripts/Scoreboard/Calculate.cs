using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Calculate {


    private List<float> speed;
    private float topspeed;
    private float avgspeed;
    private List<float> distance;
    private List<bool> leftlight;
    private List<bool> rightlight;
    private int time;
    private List<int> throttle;
    private List<int> brake;
    private List<string> lookat;
    private List<bool> lanetog;
    private List<float> gazing;
    private List<string> lane;
    private string rulen = "";
    private List<string> rgw = new List<string>();



    public void initialize( List<float> sp,float tsp,float avgsp,List<float> dis,List<bool> ll,List<bool> rl,int ti,List<int> thr,List<int> bra,List<string> la,List<bool> latg,List<float> gaz,List<string> lan)
    {
        this.speed = sp;
        this.topspeed = tsp;
        this.avgspeed = avgsp;
        this.distance = dis;
        this.leftlight = ll ;
        this.rightlight = rl;
        this.time = ti;
        this.throttle = thr;
        this.brake = bra;
        this.lookat = la;
        this.lanetog = latg;
        this.gazing = gaz;
        this.lane = lan;
        

    }

    public string loadrulen()
    {

        return rulen;
    }

    public int calc( Intugate intugate )
    {
        int a = 100;
        int co = 0;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        
        rulen = intugate.loadname();
        if(intugate.loadbo("sign"))
        {
            if(intugate.loadbo("stop"))
            {
                if(intugate.loadbo("RGW"))
                {
                    co = 1;
                }
                else
                {
                    co = 2;
                }
            }
            else
            {
                co = 3;
            }
        }
        else
        {
                co = 4;
            
        }
        switch(co)
        {
            case 1:
                for (int i = 0; i < rgw.Count; i++)
                {
                    
                }
                return a;

            case 2:

                return a;

            case 3:

                return a;

            case 4:
                if(intugate.loadflo("speed")>0)
                {
                    if(avgspeed > intugate.loadflo("speed"))
                    {
                        a -= 50;
                    }
                    else
                    {
                        if(topspeed > intugate.loadflo("speed"))
                        {
                            a -= 30;
                        }
                    }
                    for (int i = 0; i < speed.Count; i++)
                    {
                        if(speed[i]>intugate.loadflo("speed"))
                        {
                            a -= 1;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lanetog.Count; i++)
                    {
                        if(lanetog[i] && (leftlight[i] || rightlight[i]))
                        {
                            b.Add(i);
                        }
                        else
                        {
                            if(lanetog[i]){
                                a -= 1;
                                if(d!=0 && c>0)
                                {
                                    if((d+1)!=i)
                                    {
                                        e = c;
                                        c = 0;
                                        d = i;
                                    }
                                    else
                                    {
                                        c++;
                                        d=i;
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
                    if(e>4)
                    {
                        a -= 25;
                    }
                   
                }
                return a;

            default :

                return a;
        }

        
    }
    





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
