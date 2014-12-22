using UnityEngine;
using System.Collections;

public class TurnSignChecker : MonoBehaviour {

    public Collider startPoint;
    public Collider endPoint;
    float maxDistance;
    float cornerAngle;
    float firstEnter = 0f;
    float firstLeft = 0f;

    TrafficChecker tc;

    public void setTrafficChecker(TrafficChecker trafficChecker){
        tc = trafficChecker;
    }

    void Start()
    {
        cornerAngle = Vector3.Angle(startPoint.transform.right, endPoint.transform.right);
        maxDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position) * Mathf.PI * 2 * ( cornerAngle / 360f );
    }

    public void EnterCorner(float distance)
    {
        if (firstEnter == 0f)
            firstEnter = distance;
        else if (distance > firstEnter + maxDistance)
            firstEnter = distance;
    }

    public bool LeaveCorner(float distance)
    {
        if (distance <= firstEnter + maxDistance)
            return true;
        else if (distance > firstEnter + maxDistance)
            firstEnter = 0f;
        return false;
    }

}
