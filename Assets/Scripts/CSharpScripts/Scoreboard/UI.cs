using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
    private List<int> boo;
    private List<int> broo;
    private List<int> boon;
    private int bull = 0;
    private List<int> scoring = new List<int>();
    private float score = 0;
    private int scoreavg = 0;
    private int sc = 0;
    private int s = 1;
    private List<string> desc = new List<string>();
    Calculate calc = new Calculate();
    public static List<Intugate> intu =new List<Intugate>();
    public static List<Intugate> inti = new List<Intugate>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake (){
        //rulelis = calc.loadrulen();
        //scoring = calc.loadescore();
        //desc = calc.loaddesc();
        foreach(Intugate i in intu)
        {
            rulelis.Add(i.loadname());
            scoring.Add(i.score());
            desc.Add(i.loaddesc());
            rulepic.Add(Resources.Load<Texture>("rule/" + i.loadname()));
            if(i.score()>49)
            {
                boo.Add(1);
            }
            else
            {
                boo.Add(0);
            }
            
        }
        calc.calc(scoring);
        score = calc.loadscore();
        broo = calc.loadgrade();
        if(score > 49)
        {
            s = 1;
        }
        else
        {
            s = 0;
        }
        }
        

	void OnGUI(){

		GUIStyle style = GUI.skin.GetStyle ("label");
        GUIStyle style1 = new GUIStyle();
		style.font = font;
		toolbarInt = GUI.Toolbar (new Rect (60, 40, 250, 30), toolbarInt, canvasList, style1);
        Debug.Log(rulelis.Count);
		if (GUI.changed) {

						
        }
        if (toolbarInt == 0)
        {
            GUI.Label(new Rect(65, 90, 100, 20), "Overall", style);
            for (int i = 0; i < rulelis.Count; i++)
            {
                

                GUI.Label(new Rect(75, 110 + (30 * (i + 1)), 100, 20), Toggler[boo[i]]);
                GUI.Label(new Rect(Screen.width/3, 110 + (30 * (i + 1)), 100, 20), rulelis[i]);
                GUI.Label(new Rect(Screen.width - (Screen.width / 3), 110 + (30 * (i + 1)), 100, 20), scoring[i]+" %");
                GUI.Label(new Rect(Screen.width - 115, 110 + (30 * (i + 1)), 100, 20), grade[broo[i]]);

            }

            GUI.Label(new Rect(Screen.width/4-75,Screen.height - 100, 100, 20),passed[s] );
            GUI.Label(new Rect(Screen.width/2-75, Screen.height - 100, 100, 20),score + " %" );
            GUI.Label(new Rect(Screen.width - (Screen.width / 4)-75, Screen.height - 100, 100, 20), grade[broo.Count-1]);



        }
        if (toolbarInt == 1)
        {
            
            GUI.DrawTexture(new Rect(80, 80, 100, 100),rulepic[bull], ScaleMode.StretchToFill, true, 10.0F);
            GUI.Label(new Rect(Screen.width / 2 - 50 , 100 , 100 , 20), rulelis[bull]);
            GUI.Label(new Rect(Screen.width / 2 - 100, 225, 400, 400), desc[bull]);
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 100, 20), grade[broo[bull]]);
            if (GUI.Button(new Rect(Screen.width - 115, Screen.height / 2, 80, 30), buttonpic[0]))
            {
                if (bull < rulelis.Count)
                {
                    bull++;
                }
                else
                {
                    bull = 0;
                }
            }
            if (GUI.Button(new Rect(75, Screen.height / 2, 80, 30), buttonpic[1]))
            {
                if (bull > 0)
                {
                    bull--;
                }
                else
                {
                    bull = rulelis.Count;
                }
            }



        }
        if (toolbarInt == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, 100, 100, 20), "drunk");
            GUI.Label(new Rect(Screen.width / 2 - 100, 225, 400, 400), "You are so very high risky driving");



        }
	}


}
