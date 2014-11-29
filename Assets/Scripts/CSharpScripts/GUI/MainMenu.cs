using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Texture titleBackground;
    public Texture buttonFrame;
    public string[] textMenu;

    private float[] guiPosition = new float[2];
    private GUIStyle titleBox;
    private int selectedMenu;
    private float selectTime;

    void Awake()
    {
        guiPosition[0] = Screen.width/2f - 320;
        guiPosition[1] = Screen.height/2f - 240;
        Screen.lockCursor = true;
        selectedMenu = 0;
        selectTime = 0f;
    }

	// Update is called once per frame
	void Update () {
        if(Time.time > selectTime + 0.5f && Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f){
            if (Input.GetAxis("Horizontal") > 0f)
                selectedMenu += 1;

            if (Input.GetAxis("Horizontal") < -0f)
                selectedMenu -= 1;

            if (selectedMenu < 0)
                selectedMenu = textMenu.Length - 1;

            selectedMenu = selectedMenu % textMenu.Length;
            selectTime = Time.time;
        }        
	}

    void FixedUpdate()
    {
        guiPosition[0] = Screen.width / 2f - Screen.width / 4f;
        guiPosition[1] = Screen.height / 2f - Screen.height / 4f;
    }

    void OnGUI()
    {

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleBackground, ScaleMode.StretchToFill);
        GUI.Box(new Rect(guiPosition[0], guiPosition[1], guiPosition[0] * 2, guiPosition[1] * 2), "");

        titleBox = GUI.skin.label;
        titleBox.alignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < textMenu.Length; i++)
        {
            if (i == selectedMenu)
                titleBox.normal.textColor = Color.yellow;
            else
                titleBox.normal.textColor = Color.white;
            GUI.DrawTexture(new Rect(guiPosition[0] + 10, guiPosition[1] + (i * 50f) + 20, (guiPosition[0] * 2) - 20f, 40f), buttonFrame, ScaleMode.StretchToFill);
            GUI.Label(new Rect(guiPosition[0] + 10, guiPosition[1] + (i * 50f) + 20, (guiPosition[0] * 2) - 20f, 40f), textMenu[i], titleBox);
        }

        if (Input.GetAxis("Vertical") > 0.5f)
        {
            titleBox = GUI.skin.box;
            titleBox.alignment = TextAnchor.MiddleCenter;
            switch (selectedMenu)
            {
                case 0:
                    GUI.Box(new Rect(guiPosition[0], guiPosition[1], guiPosition[0] * 2, guiPosition[1] * 2), "Please Wait.....", titleBox);
                    Application.LoadLevel("FreeDrive");
                    break;

                case 1:
                    GUI.Box(new Rect(guiPosition[0], guiPosition[1], guiPosition[0] * 2, guiPosition[1] * 2), "Please Wait.....", titleBox);
                    Application.LoadLevel("Replay");
                    break;

                case 2:
                    GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 4, Screen.height / 4), "Exit", titleBox);
                    Application.Quit();
                    break;

                default:
                    Debug.Log(textMenu[selectedMenu]);
                    break;
            }
        }
        
    }
}
