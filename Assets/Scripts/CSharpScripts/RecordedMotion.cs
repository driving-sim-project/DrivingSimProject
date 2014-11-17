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
    public string[] gazingNameList;
    public int[] gazingPerList;


    public void Init(List<RecordedFrame> ftemp, int cfntemp)
    {
        Vector3 posTemp;
        List<string> gazingNameListTemp = new List<string>();
        List<int> gazingPerListTemp = new List<int>();
        int gazingIndex = -1;
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
            if (gazingNameListTemp.Contains(rm.gazingObjectName) == false)
            {
                gazingNameListTemp.Add(rm.gazingObjectName);
                gazingPerListTemp.Add(1);
            }
            else
            {
                gazingIndex = gazingNameListTemp.IndexOf(rm.gazingObjectName);
                gazingPerListTemp[gazingIndex] += 1;
            }
        }
        if (distance < 1f)
            distance = 0f;
        avgSpeed = (int)(avgSpeed * 1f / frames.Count);
        gazingNameList = gazingNameListTemp.ToArray();
        gazingPerList = gazingPerListTemp.ToArray();
    }
}
