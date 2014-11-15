using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedMotion : ScriptableObject {

    public List<RecordedFrame> frames;
    public int avgSpeed = 0;
    public float distance = 0f;
    public int topSpeed = 0;
    public int currentFrameNumber;

    Vector3 posTemp;

    public void Init(List<RecordedFrame> ftemp, int cfntemp)
    {
        frames = ftemp;
        currentFrameNumber = cfntemp;
        posTemp = frames[0].position;
        foreach (RecordedFrame rm in frames)
        {
            avgSpeed += rm.speed;
            if(topSpeed < rm.speed) 
                topSpeed = rm.speed;
            distance += Vector3.Distance(posTemp, rm.position)/1000f;
            posTemp = rm.position;
        }
        avgSpeed = (int)(avgSpeed * 1f / frames.Count);
    }
}
