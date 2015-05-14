using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public abstract class RecordedFrame {
    
    public float time;
    public float rpm;
    public float throttle;
    public bool headlight;
    public Converter.Vector3 position;
    public Converter.Quaternion rotation;
    public Converter.Vector3[] wheelsPosition;
    public Converter.Quaternion[] wheelsRotation;
    public bool rearlight;
    public int[] wheelAngle;
    public bool sidelightR;
    public bool sidelightL;

    public static RecordedFrame dataFrame (bool isAi, string[] txtForm)
    {
        int index = 0;
        int legthTmp = 0;
        RecordedFrame frame;
        if (isAi == false)
        {
            frame = new PlayerFrame();
            frame.time = float.Parse(txtForm[index++]);
            frame.rpm = float.Parse(txtForm[index++]);
            frame.throttle = float.Parse(txtForm[index++]);
            frame.headlight = bool.Parse(txtForm[index++]);
            frame.position = new Converter.Vector3(new Vector3(float.Parse(txtForm[index]), float.Parse(txtForm[index+1]), float.Parse(txtForm[index+2])));
            index += 3;
            frame.rotation = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index+1]), float.Parse(txtForm[index+2]), float.Parse(txtForm[index+3])));
            index += 4;

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelsPosition = new Converter.Vector3[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelsPosition[i] = new Converter.Vector3(new Vector3(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2])));
                index += 3;
            }

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelsRotation = new Converter.Quaternion[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelsRotation[i] = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2]), float.Parse(txtForm[index + 3])));
                index += 4;
            }
            frame.rearlight = bool.Parse(txtForm[index++]);

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelAngle = new int[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelAngle[i] = int.Parse(txtForm[index++]);
            }

            frame.sidelightR = bool.Parse(txtForm[index++]);
            frame.sidelightL = bool.Parse(txtForm[index++]);

            ((PlayerFrame)frame).speed = int.Parse(txtForm[index++]);
            ((PlayerFrame)frame).steering = float.Parse(txtForm[index++]);
            ((PlayerFrame)frame).currentDistance = float.Parse(txtForm[index++]);
            ((PlayerFrame)frame).gazingObjectName = txtForm[index++];

            ((PlayerFrame)frame).cameraRotaion = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2]), float.Parse(txtForm[index + 3])));
            index += 4;

            ((PlayerFrame)frame).eyePosition = new Converter.Vector3(new Vector3(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2])));
            index += 3;

            ((PlayerFrame)frame).steeringWheelRotation = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2]), float.Parse(txtForm[index + 3])));
            index += 4;

            legthTmp = int.Parse(txtForm[index++]);
            ((PlayerFrame)frame).wheelsOnLine = new string[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                ((PlayerFrame)frame).wheelsOnLine[i] = txtForm[index++];
            }
            ((PlayerFrame)frame).isCrossing = bool.Parse(txtForm[index++]);

            ((PlayerFrame)frame).collisionTag[0] = txtForm[index++];
            ((PlayerFrame)frame).collisionTag[1] = txtForm[index++];
            ((PlayerFrame)frame).collisionTag[2] = txtForm[index++];
            ((PlayerFrame)frame).collisionTag[3] = txtForm[index++];

            ((PlayerFrame)frame).collisionID[0] = int.Parse(txtForm[index++]);
            ((PlayerFrame)frame).collisionID[1] = int.Parse(txtForm[index++]);
            ((PlayerFrame)frame).collisionID[2] = int.Parse(txtForm[index++]);
            ((PlayerFrame)frame).collisionID[3] = int.Parse(txtForm[index++]);

        }
        else
        {
            frame = new AiFrame();
            frame.time = float.Parse(txtForm[index++]);
            frame.rpm = float.Parse(txtForm[index++]);
            frame.throttle = float.Parse(txtForm[index++]);
            frame.headlight = bool.Parse(txtForm[index++]);
            frame.position = new Converter.Vector3(new Vector3(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2])));
            index += 3;
            frame.rotation = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2]), float.Parse(txtForm[index + 3])));
            index += 4;

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelsPosition = new Converter.Vector3[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelsPosition[i] = new Converter.Vector3(new Vector3(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2])));
                index += 3;
            }

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelsRotation = new Converter.Quaternion[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelsRotation[i] = new Converter.Quaternion(new Quaternion(float.Parse(txtForm[index]), float.Parse(txtForm[index + 1]), float.Parse(txtForm[index + 2]), float.Parse(txtForm[index + 3])));
                index += 4;
            }
            frame.rearlight = bool.Parse(txtForm[index++]);

            legthTmp = int.Parse(txtForm[index++]);
            frame.wheelAngle = new int[legthTmp];
            for (int i = 0; i < legthTmp; i++)
            {
                frame.wheelAngle[i] = int.Parse(txtForm[index++]);
            }
            frame.sidelightR = bool.Parse(txtForm[index++]);
            frame.sidelightL = bool.Parse(txtForm[index++]);
        }
        return frame;
    }

    public static string[] txtForm( PlayerFrame playerFrame )
    {
        List<string> data = new List<string>();
        data.Add(playerFrame.time.ToString() + ',');
        data.Add(playerFrame.rpm.ToString() + ',');
        data.Add(playerFrame.throttle.ToString() + ',');
        data.Add(playerFrame.headlight.ToString() + ',');

        data.Add(playerFrame.position.x.ToString() + ',');
        data.Add(playerFrame.position.y.ToString() + ',');
        data.Add(playerFrame.position.z.ToString() + ',');

        data.Add(playerFrame.rotation.x.ToString() + ',');
        data.Add(playerFrame.rotation.y.ToString() + ',');
        data.Add(playerFrame.rotation.z.ToString() + ',');
        data.Add(playerFrame.rotation.w.ToString() + ',');

        data.Add(playerFrame.wheelsPosition.Length.ToString() + ',');
        foreach (Converter.Vector3 vec3Tmp in playerFrame.wheelsPosition)
        {
            data.Add(vec3Tmp.x.ToString() + ',');
            data.Add(vec3Tmp.y.ToString() + ',');
            data.Add(vec3Tmp.z.ToString() + ',');
        }

        data.Add(playerFrame.wheelsRotation.Length.ToString() + ',');
        foreach (Converter.Quaternion quaTmp in playerFrame.wheelsRotation)
        {
            data.Add(quaTmp.x.ToString() + ',');
            data.Add(quaTmp.y.ToString() + ',');
            data.Add(quaTmp.z.ToString() + ',');
            data.Add(quaTmp.w.ToString() + ',');
        }

        data.Add(playerFrame.rearlight.ToString() + ',');

        data.Add(playerFrame.wheelAngle.Length.ToString() + ',');
        foreach (int i in playerFrame.wheelAngle) 
        {
            data.Add(i.ToString() + ',');
        }

        data.Add(playerFrame.sidelightR.ToString() + ',');
        data.Add(playerFrame.sidelightL.ToString() + ',');

        data.Add(playerFrame.speed.ToString() + ',');
        data.Add(playerFrame.steering.ToString() + ',');
        data.Add(playerFrame.currentDistance.ToString() + ',');
        data.Add(playerFrame.gazingObjectName.ToString() + ',');

        data.Add(playerFrame.cameraRotaion.x.ToString() + ',');
        data.Add(playerFrame.cameraRotaion.y.ToString() + ',');
        data.Add(playerFrame.cameraRotaion.z.ToString() + ',');
        data.Add(playerFrame.cameraRotaion.w.ToString() + ',');

        data.Add(playerFrame.eyePosition.x.ToString() + ',');
        data.Add(playerFrame.eyePosition.y.ToString() + ',');
        data.Add(playerFrame.eyePosition.z.ToString() + ',');

        data.Add(playerFrame.steeringWheelRotation.x.ToString() + ',');
        data.Add(playerFrame.steeringWheelRotation.y.ToString() + ',');
        data.Add(playerFrame.steeringWheelRotation.z.ToString() + ',');
        data.Add(playerFrame.steeringWheelRotation.w.ToString() + ',');


        data.Add(playerFrame.wheelsOnLine.Length.ToString() + ',');
        foreach (string s in playerFrame.wheelsOnLine) 
        {
            data.Add(s + ',');
        }

        data.Add(playerFrame.isCrossing.ToString() + ',');

        data.Add(playerFrame.collisionTag[0] + ',');
        data.Add(playerFrame.collisionTag[1] + ',');
        data.Add(playerFrame.collisionTag[2] + ',');
        data.Add(playerFrame.collisionTag[3] + ',');

        data.Add(playerFrame.collisionID[0].ToString() + ',');
        data.Add(playerFrame.collisionID[1].ToString() + ',');
        data.Add(playerFrame.collisionID[2].ToString() + ',');
        data.Add(playerFrame.collisionID[3].ToString());

        return data.ToArray();
    }

    public static string[] txtForm(AiFrame npcFrame)
    {
        List<string> data = new List<string>();
        data.Add(npcFrame.time.ToString() + ',');
        data.Add(npcFrame.rpm.ToString() + ',');
        data.Add(npcFrame.throttle.ToString() + ',');
        data.Add(npcFrame.headlight.ToString() + ',');

        data.Add(npcFrame.position.x.ToString() + ',');
        data.Add(npcFrame.position.y.ToString() + ',');
        data.Add(npcFrame.position.z.ToString() + ',');

        data.Add(npcFrame.rotation.x.ToString() + ',');
        data.Add(npcFrame.rotation.y.ToString() + ',');
        data.Add(npcFrame.rotation.z.ToString() + ',');
        data.Add(npcFrame.rotation.w.ToString() + ',');

        data.Add(npcFrame.wheelsPosition.Length.ToString() + ',');
        foreach (Converter.Vector3 vec3Tmp in npcFrame.wheelsPosition)
        {
            data.Add(vec3Tmp.x.ToString() + ',');
            data.Add(vec3Tmp.y.ToString() + ',');
            data.Add(vec3Tmp.z.ToString() + ',');
        }

        data.Add(npcFrame.wheelsRotation.Length.ToString() + ',');
        foreach (Converter.Quaternion quaTmp in npcFrame.wheelsRotation)
        {
            data.Add(quaTmp.x.ToString() + ',');
            data.Add(quaTmp.y.ToString() + ',');
            data.Add(quaTmp.z.ToString() + ',');
            data.Add(quaTmp.w.ToString() + ',');
        }

        data.Add(npcFrame.rearlight.ToString() + ',');

        data.Add(npcFrame.wheelAngle.Length.ToString() + ',');
        foreach (int i in npcFrame.wheelAngle)
        {
            data.Add(i.ToString() + ',');
        }

        data.Add(npcFrame.sidelightR.ToString() + ',');
        data.Add(npcFrame.sidelightL.ToString());

        return data.ToArray();
    }

}

