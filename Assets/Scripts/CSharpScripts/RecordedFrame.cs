using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedFrame {

    public int speed;
    public float time;
    public bool headlight;
    public float throttle;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3[] wheelsPosition;
    public Quaternion[] wheelsRotation;


    public RecordedFrame( CarController car )
    {
        speed = car.speed;
        time = Time.time;
        position = car.transform.position;
        rotation = car.transform.rotation;
        headlight = car.headlight.active;
        throttle = car.accelKey;
        List<Vector3> wheelsPositionTemp = new List<Vector3>();
        List<Quaternion> wheelsRotationTemp = new List<Quaternion>();
        foreach(Wheel w in car.wheels){
            wheelsPositionTemp.Add(w.model.transform.position);
            wheelsRotationTemp.Add(w.model.transform.rotation);
        }
        wheelsPosition = wheelsPositionTemp.ToArray();
        wheelsRotation = wheelsRotationTemp.ToArray();
    }

}
