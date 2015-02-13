using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour {


	public Texture[] Toggler;
	public Texture2D[] canvasList;
    public Texture[] grade;
    public Texture[] rulepic;
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
    private int scoreavg = 0;
    private int sc = 0;
    private int s = 1;
    Calculate calc = new Calculate();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake (){
        rulelis = calc.loadrulen();
        scoring = calc.loadescore();
        for (int i = 0; i < rulelis.Count; i++)
        {
            scoreavg += scoring[i];
            
            if (scoring[i] > 50)
            {
                boo.Add(1);
                if (scoring[i] < 55)
                {
                    broo.Add(1);
                }
                else
                {
                    if (scoring[i] < 60)
                    {
                        broo.Add(2);
                    }
                    else
                    {
                        if (scoring[i] < 65)
                        {
                            broo.Add(3);
                        }
                        else
                        {

                            if (scoring[i] < 70)
                            {
                                broo.Add(4);
                            }
                            else
                            {
                                if (scoring[i] < 75)
                                {
                                    broo.Add(5);
                                }
                                else
                                {
                                    if (scoring[i] < 80)
                                    {
                                        broo.Add(6);
                                    }
                                    else
                                    {
                                        if (scoring[i] <= 100)
                                        {
                                            broo.Add(7);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                boo.Add(0);
                broo.Add(0);
            }
        }

        scoreavg = (scoreavg / rulelis.Count);
            if (scoreavg < 55 && scoreavg >= 50)
            {
                sc= 1;
            }
            else
            {
                if (scoreavg < 60 && scoreavg >= 55)
                {
                    sc = 2;
                }
                else
                {
                    if (scoreavg < 65 && scoreavg >= 60)
                    {
                        sc = 3;
                    }
                    else
                    {

                        if (scoreavg < 70 && scoreavg >= 65)
                        {
                            sc = 4;
                        }
                        else
                        {
                            if (scoreavg < 75 && scoreavg >= 70)
                            {
                               sc = 5;
                            }
                            else
                            {
                                if (scoreavg < 80 && scoreavg >= 75)
                                {
                                    sc = 6;
                                }
                                else
                                {
                                    if (scoreavg <= 100 && scoreavg >= 80)
                                    {
                                        sc = 7;
                                    }
                                    else
                                    {
                                        sc = 0;
                                        s = 0;
                                        
                                    }
                                }
                            }
                        }
                    }
                }
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
            GUI.Label(new Rect(Screen.width/2-75, Screen.height - 100, 100, 20),scoreavg + " %" );
            GUI.Label(new Rect(Screen.width - (Screen.width / 4)-75, Screen.height - 100, 100, 20), grade[sc]);



        }
        if (toolbarInt == 1)
        {
            
            GUI.DrawTexture(new Rect(80, 80, 100, 100),rulepic[boon[bull]], ScaleMode.StretchToFill, true, 10.0F);
            GUI.Label(new Rect(Screen.width / 2 - 50 , 100 , 100 , 20), rulelis[boon[bull]]);
            GUI.Label(new Rect(Screen.width / 2 - 100, 225, 400, 400), "dukdik-hiend \n kimochi ne");
            GUI.Label(new Rect(Screen.width / 4 - 75, Screen.height - 100, 100, 20), rulelis[0]);
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 100, 20), rulelis[0]);
            GUI.Label(new Rect(Screen.width - (Screen.width / 4) - 75, Screen.height - 100, 100, 20), rulelis[0]);
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
            GUI.Label(new Rect(65, 75, 100, 20), "Overall", style);
            for (int i = 0; i < rulelis.Count; i++)
            {


                GUI.Label(new Rect(75, 100 + (30 * (i + 1)), 100, 20), rulelis[i]);
                GUI.Label(new Rect(Screen.width / 3, 100 + (30 * (i + 1)), 100, 20), rulelis[i]);
                GUI.Label(new Rect(Screen.width - (Screen.width / 3), 100 + (30 * (i + 1)), 100, 20), rulelis[i]);
                GUI.Label(new Rect(Screen.width - 100, 100 + (30 * (i + 1)), 100, 20), rulelis[i]);

            }

            GUI.Label(new Rect(Screen.width / 4 - 75, Screen.height - 100, 100, 20), rulelis[0]);
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 100, 20), rulelis[0]);
            GUI.Label(new Rect(Screen.width - (Screen.width / 4) - 75, Screen.height - 100, 100, 20), rulelis[0]);



        }
	}


}
