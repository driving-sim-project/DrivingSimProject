using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AiFrame : RecordedFrame {

    public AiFrame()
    {

    }

    public AiFrame(CarController car)
    {
        throttle = car.accelKey;
        rpm = car.GetComponent<Drivetrain>().rpm;
        time = Time.time;
        position = new Converter.Vector3(car.transform.position);
        rotation = new Converter.Quaternion(car.transform.rotation);
        headlight = car.headlight.activeInHierarchy;
        rearlight = car.rearlight.activeInHierarchy;
        sidelightL = car.sidelightSL;
        sidelightR = car.sidelightSR;
        List<Converter.Vector3> wheelsPositionTemp = new List<Converter.Vector3>();
        List<Converter.Quaternion> wheelsRotationTemp = new List<Converter.Quaternion>();
        List<int> wheelAngleTemp = new List<int>();
        foreach (Wheel w in car.wheels)
        {
            wheelsPositionTemp.Add(new Converter.Vector3(w.model.transform.localPosition));
            wheelsRotationTemp.Add(new Converter.Quaternion(w.model.transform.localRotation));
            wheelAngleTemp.Add((int)((w.model.transform.localRotation.eulerAngles.y % 180) > 90 ?
                180 - (w.model.transform.localRotation.eulerAngles.y % 180) :
                w.model.transform.localRotation.eulerAngles.y % 180));
        }
        wheelsPosition = wheelsPositionTemp.ToArray();
        wheelsRotation = wheelsRotationTemp.ToArray();
        wheelAngle = wheelAngleTemp.ToArray();
    }
	
}
