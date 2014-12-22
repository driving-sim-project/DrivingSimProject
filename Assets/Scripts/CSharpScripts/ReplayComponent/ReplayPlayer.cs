using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CarController))]
public class ReplayPlayer : MonoBehaviour {

    public GameObject[] wheelsModel;
    public Texture2D cursorImage;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    List<RecordedMotion> recordList = new List<RecordedMotion>();
    RecordedMotion recording;
    bool gui = true;
    float startTime;
    int fn;
    CarController car;
    bool playing = false;

    void Awake()
    {
        foreach (RecordedMotion file in Resources.LoadAll<RecordedMotion>("Replays/"))
        {
            if(file.name.Contains(Application.loadedLevelName))
                recordList.Add(file);
        }
    }

	// Use this for initialization
	void Start () {
        car = GetComponent(typeof(CarController)) as CarController;
        recording = recordList[recordList.Count - 1];
        ReplaySetup();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Circle") == true)
            gui = !gui;

        if (Input.GetButtonDown("Enter") == true)
        {
            Application.LoadLevel("StartMenu");
        }

	    if(recording != null){
            if (fn < recording.frames.Count - 1)
            {
                if(Time.time + startTime > recording.frames[fn].time){
                    fn++;
                    transform.position = recording.frames[fn].position;
                    transform.rotation = recording.frames[fn].rotation;
                    car.speed = recording.frames[fn].speed;
                    car.headlight.SetActive(recording.frames[fn].headlight);
                    car.rearlight.SetActive(recording.frames[fn].rearlight);
                    car.sidelightL.SetActive(recording.frames[fn].sidelightL);
                    car.sidelightR.SetActive(recording.frames[fn].sidelightR);
                    car.taillight.SetActive(recording.frames[fn].throttle > 0? false:true);
                    for (int i = 0; i < wheelsModel.Length; i++)
                    {
                        wheelsModel[i].transform.localPosition = recording.frames[fn].wheelsPosition[i];
                        wheelsModel[i].transform.localRotation = recording.frames[fn].wheelsRotation[i];
                    }
                    car.steeringWheel.transform.localRotation = recording.frames[fn].steeringWheelRotation;
                    //Camera.main.transform.rotation = recording.frames[fn].cameraRotaion;
                }
            }
            else
            {
                fn = 0;
                startTime = Time.time;
            }
        }
	}

    void OnGUI()
    {
        GUIStyle box = GUI.skin.box;
        box.alignment = TextAnchor.UpperCenter;
        if(playing == true && gui == true){
            GUI.Box(new Rect(10, 10, 300, 300), "Driving Data", box);
            GUI.Label(new Rect(20, 40, 300, 30), "Speed : " + recording.frames[fn].speed + " km/h");
            GUI.Label(new Rect(20, 60, 300, 30), "Throttle : " + (recording.frames[fn].throttle > 0 ?
                (int)(recording.frames[fn].throttle * 100f) : 0) + " %");
            GUI.Label(new Rect(20, 80, 300, 30), "Brake : " + (recording.frames[fn].throttle <= 0 ?
                (int)(Mathf.Abs(recording.frames[fn].throttle) * 100f) : 0) + " %");
            GUI.Label(new Rect(20, 100, 300, 30), "Steering Wheel : " + (recording.frames[fn].steering == 0 ? "Center 0" :
                (recording.frames[fn].steering > 0 ? "Left " + (int)Mathf.Abs(recording.frames[fn].steering) :
                "Right " + (int)Mathf.Abs(recording.frames[fn].steering))) + " degree");
            GUI.Label(new Rect(20, 120, 300, 30), "Looking at : " + recording.frames[fn].gazingObjectName);
            GUI.Label(new Rect(20, 140, 300, 30), "Driving time : " + recording.frames[recording.frames.Count - 1].time + " sec");
            GUI.Label(new Rect(20, 160, 300, 30), "Average speed : " + recording.avgSpeed + " km/h");
            GUI.Label(new Rect(20, 180, 300, 30), "Top speed : " + recording.topSpeed + " km/h");
            GUI.Label(new Rect(20, 200, 300, 30), "Distance : " + recording.distance + " km");
            GUI.Label(new Rect(20, 220, 300, 30), "Wheel Angle : " + recording.frames[fn].wheelAngle[0] + " Degree");
            GUI.Box(new Rect(Screen.width - 320, 10, 300, 30 * recording.gazingNameList.Length + 1), "Gazing Statistics", box);
            for (int i = 0; i < recording.gazingNameList.Length; i++)
                GUI.Label(new Rect(Screen.width - 320, 30 + 20 * i, 300, 30),
                    recording.gazingNameList[i] + " : " + (recording.gazingPerList[i] * 100f) / recording.frames.Count + "%");
            GUI.DrawTexture(new Rect(recording.frames[fn].eyePosition.x, Screen.height - recording.frames[fn].eyePosition.y, cursorWidth, cursorHeight), cursorImage);
        }
        else
        {
            GUI.Box(new Rect(10, 10, 300, 300), "Replay Data List", box);
            for (int i = 0; i < recordList.Count; i++)
            {
                GUI.Label(new Rect(20, 30 + 20 * i, 300, 30), recordList[i].name);
            }

        }

    }

    void ReplaySetup()
    {
        startTime = Time.time;
        if (recording != null)
        {
            recording.currentFrameNumber = 0;
            fn = recording.currentFrameNumber;
        }
        playing = true;
    }
}
