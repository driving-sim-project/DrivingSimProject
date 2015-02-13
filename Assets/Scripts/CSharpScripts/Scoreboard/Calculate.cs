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
    private List<int> score = new List<int>();
    private float limsp = 80;



    public void initialize( List<float> sp,float tsp,float avgsp,List<float> dis,List<bool> ll,List<bool> rl,int ti,List<int> thr,List<int> bra,List<string> la,List<bool> latg,List<float> gaz,string lan)
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

    public List<string> loadrulen()
    {

        return rulen;
    }

    public List<int> loadescore()
    {
        return score;
    }

    public void calc( Intugate intugate , int start = 0 , int stop = 0 , int passed = 0)
    {
        int a = 100;
        int co = 0;
        List<int> b = new List<int>();
        int c = 0;
        int d = 0;
        int e = 0;
        
        rulen.Add(intugate.loadname());
        if(intugate.loadbo("sign"))
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
        switch(co)
        {
            case 1:
                if (passed == 0)
                {
                    a -= 60;
                }
                else
                {
                    for (int i = start; i < stop; i++)
                    {
                        if ((lookat[i] != "traffic light" && gazing[i] < 50) && (i < start + 10))
                        {
                            a -= 5;
                        }
                    }

                }

                score.Add(a);
                break;


            case 2:
                if (passed == 0)
                {
                    a -= 60;
                }
                else
                {
                    for (int i = start; i < (start + 10); i++)
                    {
                        if (lookat[i] != "sign" && gazing[i] < 50)
                        {
                            a -= 5;
                        }
                    }
                }

                score.Add(a);
                break;

            case 3:
                if(intugate.loadflo("speed")>0)
                {
                    limsp = intugate.loadflo("speed");
                    if(avgspeed > limsp)
                    {
                        a -= 50;
                    }
                    else
                    {
                        if(topspeed > limsp)
                        {
                            a -= 30;
                        }
                    }
                    for (int i = 0; i < speed.Count; i++)
                    {
                        if(speed[i]>limsp)
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
                                        if (c > e) {
                                            e = c;
                                        }
                                        
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
                score.Add(a);
                break;

            default :

                score.Add(a);
                break;
        }

        
    }
    
    public int analy()
    {
        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;
        int e = 0;
        int f = 0;
        int g = 0;
        int h = 0;
        int k = 0;
        if(avgspeed > 60)
        {
        
        }
        
        for (int i = 0; i < speed.Count; i++)
        {
            if (lanetog[i])
            {
                b += 1;                
            }
            if(speed[i]>limsp)
            {
                c += 1;
            }
            if(throttle[i]>70)
            {
                if (d != 0 && e > 0)
                {
                    if((d+1)!=i)
                    {
                        if (e > f) {

                            f = e;
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
                if(brake[i]>70)
                {

                }
                else
                {

                }
            }
        }
        return a;

    }





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
