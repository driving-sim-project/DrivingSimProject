using UnityEngine;
using System.Collections;

public class ReplayPlayer : MonoBehaviour {

    public RecordedMotion recording;
    int fn;

	// Use this for initialization
	void Start () {
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
                Debug.Log(fn);
                if(Time.time > recording.frames[fn].time){
                    fn++;
                    transform.position = recording.frames[fn].position;
                    transform.rotation = recording.frames[fn].rotation;
                }
            }
        }
	}
}
