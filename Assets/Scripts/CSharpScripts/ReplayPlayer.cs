using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CarController))]
public class ReplayPlayer : MonoBehaviour {

    public RecordedMotion recording;
    public GameObject[] wheelsModel;
    public Texture2D cursorImage;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    float startTime;
    int fn;
    CarController car;

	// Use this for initialization
	void Start () {
        startTime = 0f;
        car = GetComponent(typeof(CarController)) as CarController;
	    if(recording != null){
            recording.currentFrameNumber = 0;
            fn = recording.currentFrameNumber;
            Debug.Log("framecount : " + recording.frames.Count);
        }
	}
	
	// Update is called once per frame
	void Update () {
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
                    car.taillight.SetActive(recording.frames[fn].throttle > 0? false:true);
                    for (int i = 0; i < wheelsModel.Length; i++)
                    {
                        wheelsModel[i].transform.position = recording.frames[fn].wheelsPosition[i];
                        wheelsModel[i].transform.rotation = recording.frames[fn].wheelsRotation[i];
                    }
                    Camera.main.transform.rotation = recording.frames[fn].cameraRotaion;
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
        GUI.Box(new Rect(10, 10, 200, 200), "Driving Data");
        GUI.Label(new Rect(20, 40, 200, 30), "Speed : " + recording.frames[fn].speed + " km/h");
        GUI.Label(new Rect(20, 60, 200, 30), "Throttle : " + recording.frames[fn].throttle);
        GUI.Label(new Rect(20, 80, 200, 30), "Steering Wheel : " + recording.frames[fn].steering);
        GUI.Label(new Rect(20, 100, 200, 30), "Looking at : " + recording.frames[fn].gazingObjectName);
        GUI.DrawTexture(new Rect(recording.frames[fn].eyePosition.x, Screen.height - recording.frames[fn].eyePosition.y, cursorWidth, cursorHeight), cursorImage);
    }
}
