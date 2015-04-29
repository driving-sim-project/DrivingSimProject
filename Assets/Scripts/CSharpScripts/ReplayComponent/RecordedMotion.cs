using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedMotion {

    public List<RecordedFrame> frames = new List<RecordedFrame>();
    public int avgSpeed = 0;
    public float distance = 0f;
    public int topSpeed = 0;
    public int currentFrameNumber;
    public List<string> gazingNameList = new List<string>();
    public List<int> gazingPerList = new List<int>();
    public bool isAccident;
    public bool isOffTrack;
    public bool isFinish;
    int gazingIndex = -1;

    public void AddFrame(RecordedFrame rm)
    {
        frames.Add(rm);
        if (distance < 1f)
            distance = 0f;
        avgSpeed += rm.speed;
        if (topSpeed < rm.speed)
            topSpeed = rm.speed;
        if (gazingNameList.Contains(rm.gazingObjectName) == false)
        {
            gazingNameList.Add(rm.gazingObjectName);
            gazingPerList.Add(1);
        }
        else
        {
            gazingIndex = gazingNameList.IndexOf(rm.gazingObjectName);
            gazingPerList[gazingIndex] += 1;
        }
        distance = rm.currentDistance;
    }

    public void Finalize()
    {
        avgSpeed = (int)(avgSpeed / frames.Count);
    }
}
