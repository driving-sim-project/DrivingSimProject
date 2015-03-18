using UnityEngine;
using System.Collections;

public class TurnSignChecker : MonoBehaviour {

    public Collider startPoint;
    public Collider endPoint;
    public string ruleText;
    float maxDistance;
    float cornerAngle;
    float firstEnter = 0f;
    bool violent = false;

    void Start()
    {
        cornerAngle = Vector3.Angle(startPoint.transform.right, endPoint.transform.right);
        if (cornerAngle == 0f)
            maxDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position) * 1.5f;
        else
            maxDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position) * Mathf.PI * 2 * (cornerAngle / 360f);
    }

    public void EnterCorner(float distance)
    {
        if (firstEnter == 0f || distance > firstEnter + maxDistance + 1f)
            firstEnter = distance;
    }

    public bool LeaveCorner(float distance)
    {
        if (violent == false)
        {
            if (distance <= firstEnter + maxDistance)
                violent = true;
        }
        return violent;
    }

}
