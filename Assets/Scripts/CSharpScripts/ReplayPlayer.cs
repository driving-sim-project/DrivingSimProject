using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CarController))]
public class ReplayPlayer : MonoBehaviour {

    public RecordedMotion recording;
    public GameObject[] wheelsModel;

    int fn;
    CarController car;

	// Use this for initialization
	void Start () {
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
                if(Time.time > recording.frames[fn].time){
                    fn++;
                    transform.position = recording.frames[fn].position;
                    transform.rotation = recording.frames[fn].rotation;
                    car.speed = recording.frames[fn].speed;
                    car.headlight.SetActive(recording.frames[fn].headlight);
                    car.taillight.SetActive(recording.frames[fn].throttle > 0? false:true);
                    for (int i = 0; i < wheelsModel.Length; i++)
                    {
                        wheelsModel[i].transform.position = recording.frames[fn].wheelsPosition[i];
                        wheelsModel[i].transform.rotation = recording.frames[fn].wheelsRotation[i];
                    }
                }
            }
        }
	}
}
