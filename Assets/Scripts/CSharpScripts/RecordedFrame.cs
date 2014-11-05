using UnityEngine;
using System.Collections;

[System.Serializable]
public class RecordedFrame {

    public Vector3 position;
    public Quaternion rotation;
    public float time;

    public RecordedFrame( Vector3 newPosition, Quaternion newRotation )
    {
        position = newPosition;
        rotation = newRotation;
        time = Time.time;
    }

}
