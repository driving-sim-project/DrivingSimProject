using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedFrame {

    public float rpm;
    public int speed;
    public float time;
    public bool headlight;
    public float throttle;
    public float steering;
    public Vector3 position;
    public float currentDistance;
    public Quaternion rotation;
    public Vector3[] wheelsPosition;
    public Quaternion[] wheelsRotation;
    public Quaternion cameraRotaion;
    public string gazingObjectName;
    public bool rearlight;
    public Vector3 eyePosition;
    public Quaternion steeringWheelRotation;
    public int[] wheelAngle;
    public bool sidelightR;
    public bool sidelightL;

    public RecordedFrame( CarController car )
    {
        currentDistance = 0f;
        rpm = car.drivetrain.rpm / car.drivetrain.maxRPM;
        speed = car.speed;
        time = Time.time;
        position = car.transform.position;
        rotation = car.transform.rotation;
        headlight = car.headlight.active;
        rearlight = car.rearlight.active;
        sidelightL = car.sidelightL.active;
        sidelightR = car.sidelightR.active;
        throttle = car.accelKey;
        steering = Input.GetAxis("Horizontal") * -450f;
        List<Vector3> wheelsPositionTemp = new List<Vector3>();
        List<Quaternion> wheelsRotationTemp = new List<Quaternion>();
        List<int> wheelAngleTemp = new List<int>();
        foreach(Wheel w in car.wheels){
            wheelsPositionTemp.Add(w.model.transform.localPosition);
            wheelsRotationTemp.Add(w.model.transform.localRotation);
            wheelAngleTemp.Add((int)((w.model.transform.localRotation.eulerAngles.y % 180) > 90 ? 
                180 - (w.model.transform.localRotation.eulerAngles.y % 180):
                w.model.transform.localRotation.eulerAngles.y % 180));
        }
        wheelsPosition = wheelsPositionTemp.ToArray();
        wheelsRotation = wheelsRotationTemp.ToArray();
        wheelAngle = wheelAngleTemp.ToArray();
        steeringWheelRotation = car.steeringWheel.transform.localRotation;
        cameraRotaion = Camera.main.transform.rotation;
        eyePosition = Camera.main.GetComponent<GazeCamera>().screenPoint;
        gazingObjectName = Camera.main.GetComponent<GazeCamera>().currentGaze;
    }
}
