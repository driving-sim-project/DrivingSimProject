using UnityEngine;
using System.Collections;

public class SpeedMiles : MonoBehaviour {

    public CarController car;
    public int maxAngle;
    public int originAngle;
    public int maxSpeed;

    float speedAngle;

	// Use this for initialization
    //void Start () {
    //    transform.Rotate(0f, 0f, originAngle - transform.rotation.z );
    //}
	
	// Update is called once per frame
	void Update () {
        speedAngle = ((-car.speed * 1f / maxSpeed) * 100f * maxAngle) / 100f - 1;
        //if (adjustDegree(needleAngle) == adjustDegree(speedAngle + originAngle))
        transform.localEulerAngles = new Vector3(car.transform.rotation.x, car.transform.rotation.y, speedAngle + originAngle );
        //else if (adjustDegree(needleAngle) > adjustDegree(speedAngle + originAngle) && adjustDegree(needleAngle) != 1)
        //    transform.Rotate(0f, 0f, adjustDegree(needleAngle) - adjustDegree(speedAngle + originAngle));
        //else if (adjustDegree(needleAngle) < adjustDegree(speedAngle + originAngle) )
        //    transform.Rotate(0f, 0f, adjustDegree(needleAngle) - adjustDegree(speedAngle + originAngle));
        //Debug.Log("Needle : " + adjustDegree(needleAngle));
        //Debug.Log("Speed : " + adjustDegree(speedAngle + originAngle));
	}

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(0, 60, 100, 200), "km/h: " + car.speed);
    //}

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
