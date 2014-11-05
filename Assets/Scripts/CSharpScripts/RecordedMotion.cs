using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedMotion : ScriptableObject {

    public List<RecordedFrame> frames;
    public int currentFrameNumber;


}
