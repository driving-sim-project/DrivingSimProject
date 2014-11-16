using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RecordedFrame {

    public int speed;
    public float time;
    public bool headlight;
    public float throttle;
    public float steering;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3[] wheelsPosition;
    public Quaternion[] wheelsRotation;
    public Quaternion cameraRotaion;
    public string gazingObjectName;
    public bool rearlight;
    public Vector3 eyePosition;
    public Quaternion wheelControllerRotation;
    public int[] wheelAngle;

    public RecordedFrame( CarController car )
    {
        speed = car.speed;
        time = Time.time;
        position = car.transform.position;
        rotation = car.transform.rotation;
        headlight = car.headlight.active;
        rearlight = car.rearlight.active;
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
        cameraRotaion = Camera.main.transform.rotation;
        RaycastHit hit;
        eyePosition = Input.mousePosition;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eyePosition), out hit, 100f, 1 << 0))
        {
            gazingObjectName = hit.collider.tag;
        }
    }

}
