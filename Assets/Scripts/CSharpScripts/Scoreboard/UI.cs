using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class UI : MonoBehaviour {

	public Texture[] Toggler;
	public Texture2D[] canvasList;
    public Texture[] grade;
    public List<Texture> rulepic;
    public Texture[] buttonpic;
    public Texture[] passed;
    public Font font;

	private int toolbarInt = 0;
    private List<string> rulelis = new List<string>();
    private List<int> boo = new List<int>();
    private List<int> broo = new List<int>();
    private List<int> boon = new List<int>();
    private int bull = 0;
    private List<int> scoring = new List<int>();
    private float score = 0;
    private int scoreavg = 0;
    private int sc = 0;
    private int s = 1;
    int analystatus = 0;
    int failed = 0;
    private List<string> desc = new List<string>();
    Calculate calc = new Calculate();

    public static List<PlayerFrame> frames = new List<PlayerFrame>();
    public static RecordedMotion record = null;
    public static List<Intugate> intu = new List<Intugate>();
    public static List<Intugate> inti = new List<Intugate>();

	// Use this for initialization
    void Start()
    {

        List<float> spdTemp = new List<float>();
        List<bool> iscrossing = new List<bool>();
        List<bool> Leftlight = new List<bool>();
        List<bool> rightlight = new List<bool>();

        foreach (PlayerFrame frame in UI.frames)
        {
            if (inti.Exists(x => x.loadname() == "Speedlimit"))
                spdTemp.Add(frame.speed);
            if (inti.Exists(x => x.loadname() == "City Limit Speed"))
                spdTemp.Add(frame.speed);

            if (inti.Exists(x => x.loadname() == "Cross Lane"))
            {
                iscrossing.Add(frame.isCrossing);
                Leftlight.Add(frame.sidelightL);
                rightlight.Add(frame.sidelightR);
                foreach(string wheelText in frame.wheelsOnLine){
                    if (wheelText == "TrafficLine")
                    {
                        ((crosslane)inti.Find(x => x.loadname() == "Cross Lane")).online = true;
                        break;
                    }
                }
            }

        }

        if (inti.Exists(x => x.loadname() == "Speedlimit"))
        {
            ((speedlim)inti.Find(x => x.loadname() == "Speedlimit")).topspeed = record.topSpeed;
            ((speedlim)inti.Find(x => x.loadname() == "Speedlimit")).avgspeed = record.avgSpeed;
            ((speedlim)inti.Find(x => x.loadname() == "Speedlimit")).speed = spdTemp;
        }
        if (inti.Exists(x => x.loadname() == "City Limit Speed"))
        {
            ((Citylimit)inti.Find(x => x.loadname() == "City Limit Speed")).topspeed = record.topSpeed;
            ((Citylimit)inti.Find(x => x.loadname() == "City Limit Speed")).avgspeed = record.avgSpeed;
            ((Citylimit)inti.Find(x => x.loadname() == "City Limit Speed")).speed = spdTemp;
        }
        if (inti.Exists(x => x.loadname() == "Cross Lane"))
        {
            ((crosslane)inti.Find(x => x.loadname() == "Cross Lane")).iscross = iscrossing;
            ((crosslane)inti.Find(x => x.loadname() == "Cross Lane")).sidelightL = Leftlight;
            ((crosslane)inti.Find(x => x.loadname() == "Cross Lane")).sidelightR = rightlight;
        }
        
        foreach (Intugate i in intu)
        {
            i.score();
            rulelis.Add(i.loadname());
            scoring.Add(i.getscore());
            desc.Add(i.loaddesc());
            rulepic.Add(Resources.Load<Texture>("rule/" + i.loadpic()));
            if (i.getscore() > 50)
            {
                boo.Add(1);
            }
            else
            {
                boo.Add(0);
                failed = 1;
            }

        }
        if (inti.Count > intu.Count)
        {
            for (int i = 0; i < inti.Count - 1; i++)
            {
                if (rulelis.Contains(inti[i].loadname()) == false)
                {
                    rulelis.Add(inti[i].loadname());
                    scoring.Add(0);
                    desc.Add(inti[i].loaddesc());
                    rulepic.Add(Resources.Load<Texture>("rule/" + inti[i].loadpic()));
                    boo.Add(0);
                }

            }
        }
        calc.calc(scoring);
        score = calc.loadscore();
        broo = calc.loadgrade();
        if (score > 49 && failed == 0 && record.isFinish == true)
        {
            s = 1;
        }
        else
        {
            if(record.isAccident == true)
            {
                s = 2;
            }
            else
            {
                if(record.isOffTrack == true)
                {
                    s = 3;
                }
                else
                {
                    s = 0;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Enter"))
            Application.LoadLevel("StartMenu");
	}

	void Awake (){
        //rulelis = calc.loadrulen();
        //scoring = calc.loadescore();
        //desc = calc.loaddesc();

        
    }

	void OnGUI(){

		GUIStyle style = GUI.skin.GetStyle ("label");
        GUIStyle style1 = new GUIStyle();
        GUIStyle style2 = new GUIStyle() ;
		style.font = font;
        style.fontSize = Screen.height/25;
        style2.font = font;
        style2.fontSize = Screen.height / 5;
		toolbarInt = GUI.Toolbar (new Rect (60, 40, 250, 30), toolbarInt, canvasList, style1);
		if (GUI.changed) {

						
        }
        if (toolbarInt == 0)
        {
            GUI.Label(new Rect(65, 90, 100, 50), "Overall", style);
            for (int i = 0; i < rulelis.Count; i++)
            {
                

                GUI.Label(new Rect(75, 110 + (30 * (i + 1)), Screen.width/20, Screen.height/20), Toggler[boo[i]]);
                GUI.Label(new Rect(Screen.width/3, 110 + (30 * (i + 1)), 200, 40), rulelis[i]);
                GUI.Label(new Rect(Screen.width - (Screen.width / 3), 110 + (30 * (i + 1)), 200, 40), scoring[i]+" %");
                GUI.Label(new Rect(Screen.width - 115, 110 + (30 * (i + 1)), 200, 40), grade[broo[i]]);

            }

            GUI.Label(new Rect(Screen.width/4-75,Screen.height - 200, 400, 150),passed[s] );
            GUI.Label(new Rect(Screen.width/2-75, Screen.height - 150, 400, 150),score + "%" ,style2);
            GUI.Label(new Rect(Screen.width - (Screen.width / 4)-75, Screen.height - 200, 400, 150), grade[broo[broo.Count-1]]);



        }
        if (toolbarInt == 1)
        {
            
            GUI.DrawTexture(new Rect(80, 80, 100, 100),rulepic[bull], ScaleMode.StretchToFill, true, 10.0F);
            GUI.Label(new Rect(Screen.width / 2 - 75 , 100 , 200 , 50), rulelis[bull]);
            GUI.Label(new Rect(Screen.width / 2 - 175, 150, 400, 400), desc[bull]);
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 200, 150, 150), grade[broo[bull]]);
            if (GUI.Button(new Rect(Screen.width - 175, Screen.height / 2, 80, 50), buttonpic[0]))
            {
                if (bull < rulelis.Count-1)
                {
                    bull++;
                }
                else
                {
                    bull = 0;
                }
            }
            if (GUI.Button(new Rect(75, Screen.height / 2, 80, 50), buttonpic[1]))
            {
                if (bull > 0)
                {
                    bull--;
                }
                else
                {
                    bull = rulelis.Count-1;
                }
            }



        }
        if (toolbarInt == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, 100, 200, 50), "drunk");
            GUI.Label(new Rect(Screen.width / 2 - 100, 225, 400, 400), "You are so very high risky driving");
        }
	}


}
