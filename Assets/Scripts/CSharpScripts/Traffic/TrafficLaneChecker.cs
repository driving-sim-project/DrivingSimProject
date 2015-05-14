using UnityEngine;
using System.Collections;

public class TrafficLaneChecker : MonoBehaviour {

    public TrafficLane[] trafficLane;
    int laneIndex = 0;
    public bool inRightLane { private set; get; }
    public bool signalLight { private set; get; }
    public bool inLaneRange { private set; get; }

    bool signalL = false;
    bool signalR = false;


    void Start()
    {
        inRightLane = true;
        signalLight = true;
        inLaneRange = true;
    }

    public void startPointEntered( Collider startPoint, bool leftLight, bool rightLight)
    {
        signalR = rightLight;
        signalL = leftLight;
        for(int i = 0; i < trafficLane.Length; i++){
            trafficLane[i].setStartPoint(false);
            trafficLane[i].setEndPoint(true);
            if (trafficLane[i].startPoint == startPoint)
                laneIndex = i;
        }
    }

    public void stayInPath( bool isCrossing , bool leftLight, bool rightLight )
    {
        if (isCrossing == true)
        {
            inRightLane = false;
            inLaneRange = false;
        }

        if ((leftLight == false && signalL == true) || (rightLight == false && signalR == true))
        {
            signalLight = false;
            signalL = false;
            signalR = false;
        }
    }

    public void endPointEntered( Collider endPoint )
    {
        if (trafficLane[laneIndex].forward == true && endPoint == trafficLane[laneIndex].endPoint)
        {
            if (signalL == true || signalR == true)
                signalLight = false;
        }
        else if (trafficLane[laneIndex].leftTurn == true && endPoint == trafficLane[laneIndex].endPointL)
        {
            if (signalL != true)
                signalLight = false;
        }
        else if (trafficLane[laneIndex].rightTurn == true && endPoint == trafficLane[laneIndex].endPointR)
        {
            if (signalR != true)
                signalLight = false;
        }
        else
        {
            inRightLane = false;
            signalLight = false;
            inLaneRange = false;
        }
        foreach(TrafficLane tl in trafficLane){
            tl.setEndPoint(false);
            tl.setStartPoint(true);
        }
        Debug.Log(inRightLane + "/" + signalLight + "/" + inLaneRange);
    }

}
