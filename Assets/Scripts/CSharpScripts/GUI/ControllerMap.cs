using UnityEngine;
using System.Collections;

public class ControllerMap : MonoBehaviour {

    public Texture[] controllerMap;

    private int index;
    private float selectTime;

    void Awake()
    {
        selectTime = 0f;
        index = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > selectTime + 0.5f && Input.GetAxis("Vertical") > 0.5f)
        {
            selectTime = Time.time;
            if (index == controllerMap.Length - 1)
                Application.LoadLevel("startmenu");
            else
                index = (index + 1);
        }
            

	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), controllerMap[index], ScaleMode.StretchToFill);
    }
}
