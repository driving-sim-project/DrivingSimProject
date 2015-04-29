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
    public Converter.Vector3 position;
    public float currentDistance;
    public Converter.Quaternion rotation;
    public Converter.Vector3[] wheelsPosition;
    public Converter.Quaternion[] wheelsRotation;
    public Converter.Quaternion cameraRotaion;
    public string gazingObjectName;
    public bool rearlight;
    public Converter.Vector3 eyePosition;
    public Converter.Quaternion steeringWheelRotation;
    public int[] wheelAngle;
    public bool sidelightR;
    public bool sidelightL;
    public bool isCrossing;
    public string[] wheelsOnLine;

    public RecordedFrame()
    {

    }

    public RecordedFrame( CarController car )
    {
        currentDistance = 0f;
        rpm = car.drivetrain.rpm / car.drivetrain.maxRPM;
        speed = car.speed;
        time = Time.time;
        position = new Converter.Vector3( car.transform.position );
        rotation = new Converter.Quaternion( car.transform.rotation );
        headlight = car.headlight.active;
        rearlight = car.rearlight.active;
        sidelightL = car.sidelightSL;
        sidelightR = car.sidelightSR;
        throttle = car.accelKey;
        steering = Input.GetAxis("Horizontal") * -450f;
        List<Converter.Vector3> wheelsPositionTemp = new List<Converter.Vector3>();
        List<Converter.Quaternion> wheelsRotationTemp = new List<Converter.Quaternion>();
        List<int> wheelAngleTemp = new List<int>();
        List<string> wheelTagTemp = new List<string>();
        foreach(Wheel w in car.wheels){
            wheelsPositionTemp.Add(new Converter.Vector3(w.model.transform.localPosition));
            wheelsRotationTemp.Add(new Converter.Quaternion(w.model.transform.localRotation));
            wheelAngleTemp.Add((int)((w.model.transform.localRotation.eulerAngles.y % 180) > 90 ? 
                180 - (w.model.transform.localRotation.eulerAngles.y % 180):
                w.model.transform.localRotation.eulerAngles.y % 180));
            wheelTagTemp.Add(w.onTag);
        }
        wheelsPosition = wheelsPositionTemp.ToArray();
        wheelsRotation = wheelsRotationTemp.ToArray();
        wheelAngle = wheelAngleTemp.ToArray();
        wheelsOnLine = wheelTagTemp.ToArray();
        steeringWheelRotation = new Converter.Quaternion( car.steeringWheel.transform.localRotation );
        cameraRotaion = new Converter.Quaternion( Camera.main.transform.rotation );
        eyePosition = new Converter.Vector3( Camera.main.GetComponent<GazeCamera>().screenPoint );
        gazingObjectName = Camera.main.GetComponent<GazeCamera>().currentGaze;
    }
}
