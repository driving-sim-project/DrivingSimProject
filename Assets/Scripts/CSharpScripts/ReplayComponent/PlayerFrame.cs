﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerFrame : RecordedFrame {

    public int speed;
    public float steering;
    public float currentDistance;
    public string gazingObjectName;
    public int gazingObjectID;
    public Converter.Quaternion cameraRotaion;
    public Converter.Vector3 eyePosition;
    public Converter.Quaternion steeringWheelRotation;
    public string[] wheelsOnLine;
    public bool isCrossing;
    //public string[] collisionTag = new string[4];
    //public int[] collisionID = new int[4];

    public PlayerFrame()
    {
        
    }

    public PlayerFrame( CarController car )
    {
        currentDistance = 0f;
        rpm = car.drivetrain.rpm / car.drivetrain.maxRPM;
        speed = car.speed;
        time = Time.time;
        position = new Converter.Vector3(car.transform.position);
        rotation = new Converter.Quaternion(car.transform.rotation);
        headlight = car.headlight.activeInHierarchy;
        rearlight = car.rearlight.activeInHierarchy;
        sidelightL = car.sidelightSL;
        sidelightR = car.sidelightSR;
        throttle = car.accelKey;
        steering = Input.GetAxis("Horizontal") * -450f;
        List<Converter.Vector3> wheelsPositionTemp = new List<Converter.Vector3>();
        List<Converter.Quaternion> wheelsRotationTemp = new List<Converter.Quaternion>();
        List<int> wheelAngleTemp = new List<int>();
        List<string> wheelTagTemp = new List<string>();
        foreach (Wheel w in car.wheels)
        {
            wheelsPositionTemp.Add(new Converter.Vector3(w.model.transform.localPosition));
            wheelsRotationTemp.Add(new Converter.Quaternion(w.model.transform.localRotation));
            wheelAngleTemp.Add((int)((w.model.transform.localRotation.eulerAngles.y % 180) > 90 ?
                180 - (w.model.transform.localRotation.eulerAngles.y % 180) :
                w.model.transform.localRotation.eulerAngles.y % 180));
            wheelTagTemp.Add(w.onTag);
        }
        wheelsPosition = wheelsPositionTemp.ToArray();
        wheelsRotation = wheelsRotationTemp.ToArray();
        wheelAngle = wheelAngleTemp.ToArray();
        wheelsOnLine = wheelTagTemp.ToArray();
        steeringWheelRotation = new Converter.Quaternion(car.steeringWheel.transform.localRotation);
        cameraRotaion = new Converter.Quaternion(Camera.main.transform.rotation);
        eyePosition = new Converter.Vector3(Camera.main.GetComponent<GazeCamera>().screenPoint);
        gazingObjectName = Camera.main.GetComponent<GazeCamera>().currentGaze;
        gazingObjectID = Camera.main.GetComponent<GazeCamera>().currentGazeObj != null ? Camera.main.GetComponent<GazeCamera>().currentGazeObj.gameObject.GetInstanceID() : 0;
        //collisionTag[0] = front != null ? front.tag : "None";
        //collisionTag[1] = left != null ? left.tag : "None";
        //collisionTag[2] = right != null ? right.tag : "None";
        //collisionTag[3] = back != null ? back.tag : "None";
        //collisionID[0] = front != null ? front.GetInstanceID() : 0;
        //collisionID[1] = left != null ? left.GetInstanceID() : 0;
        //collisionID[2] = right != null ? right.GetInstanceID() : 0;
        //collisionID[3] = back != null ? back.GetInstanceID() : 0;
    }

}
