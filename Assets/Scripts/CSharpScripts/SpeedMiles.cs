using UnityEngine;
using System.Collections;

public class SpeedMiles : MonoBehaviour {

    public CarController car;
    public int maxAngle;
    public int originAngle;
    public int maxSpeed;

    float speedAngle;
    float needleAngle;

	// Use this for initialization
	void Start () {
        transform.Rotate(0f, 0f, originAngle - transform.rotation.z - 10 );
	}
	
	// Update is called once per frame
	void Update () {
        speedAngle = -car.rigidbody.velocity.magnitude * 3.6f / maxSpeed * 100f * (maxAngle - originAngle) / 100f - 1;
        needleAngle = transform.rotation.eulerAngles.z;
        if (adjustDegree(needleAngle) == adjustDegree(speedAngle + originAngle))
            transform.Rotate(0f, 0f, 0f);
        else if (adjustDegree(needleAngle) > adjustDegree(speedAngle + originAngle) && adjustDegree(needleAngle) != 1)
            transform.Rotate(0f, 0f, adjustDegree(needleAngle) - adjustDegree(speedAngle + originAngle));
        else if (adjustDegree(needleAngle) < adjustDegree(speedAngle + originAngle) )
            transform.Rotate(0f, 0f, adjustDegree(needleAngle) - adjustDegree(speedAngle + originAngle));
        Debug.Log("Needle : " + adjustDegree(needleAngle));
        Debug.Log("Speed : " + adjustDegree(speedAngle + originAngle));
	}

    void OnGUI()
    {
        GUI.Label(new Rect(0, 60, 100, 200), "km/h: " + car.rigidbody.velocity.magnitude * 3.6f);
    }

    int adjustDegree(float degree)
    {
        degree -= originAngle;
        if (degree < 0)
            degree = 360 + degree;
        else
            degree = degree%360;
        degree = 360 - degree;
        return (int)degree;
    }

}
