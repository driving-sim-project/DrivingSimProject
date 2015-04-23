using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarController))]
public class AiDriver : MonoBehaviour {

    CarController car;

    public GameObject frontSensor;
    public bool headlight = false;
    public float throttle = 0.5f;
    public int speedLimit = 60;
    public Waypoint waypoint;
    public float frontDistance = 10f;
    public bool loopRun = false;
    int waypointCounter = 0;

	// Use this for initialization
	void Start () {
        car = GetComponent<CarController>();
        car.headlight.SetActive(headlight);
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 front = frontSensor.transform.forward;
        front.Set(front.x, 0f, front.z);
        RaycastHit hit;
        Debug.DrawRay(frontSensor.transform.position, front * frontDistance, Color.red);
        if (loopRun == true)
            waypointCounter %= waypoint.waypoints.Length;
        if (waypointCounter < waypoint.waypoints.Length)
            frontSensor.transform.LookAt(waypoint.waypoints[waypointCounter].transform);
        float angleTmp = Vector3.Angle(car.transform.forward, frontSensor.transform.forward);
        if (Vector3.Cross(car.transform.forward, frontSensor.transform.forward).y < 0)
            angleTmp *= -1;
        float steeringAngle = 0f;
        if (angleTmp == 0)
        {
            steeringAngle = 0f;
        }
        else if (angleTmp < 0)
        {
            steeringAngle = -1f;
        }
        else if (angleTmp > 0)
        {
            steeringAngle = 1f;
        }

        //if (Mathf.Abs(angleTmp) < car.wheels[0].maxSteeringAngle / 2)
        //    steeringAngle *= 0.66f;
        steeringAngle *= Mathf.Clamp(Mathf.Abs(angleTmp), 0f, car.wheels[0].maxSteeringAngle) / car.wheels[0].maxSteeringAngle;

        if (waypointCounter == waypoint.waypoints.Length)
        {
            car.accelKey = -1f;
        }
        else if(Mathf.Abs(angleTmp) > 30f){
            if (Mathf.Abs(angleTmp) > 60f)
                    car.accelKey = -1f;
            if (car.speed < 5)
                car.accelKey = throttle;
        }
        else if (car.speed < speedLimit)
        {
            if (car.speed < 30)
                car.accelKey = throttle * 1.5f;
            else if (Mathf.Abs(steeringAngle) >= 0.1f)
            {
                if(car.speed > 60)
                    car.accelKey = -throttle;
                if (car.speed < 60)
                    car.accelKey = throttle * 0.3f;
            }
            else
                car.accelKey = throttle;
        }
        else
        {
            car.accelKey = 0f;
        }

        Vector3 carDirection = new Vector3(car.transform.forward.x, 0f, car.transform.forward.z);
        Debug.DrawRay(frontSensor.transform.position, carDirection * frontDistance, Color.blue);
        if (Physics.SphereCast(frontSensor.transform.position, 0.2f, carDirection, out hit, frontDistance))
        {
            Debug.Log(hit.transform.tag);
            car.accelKey = -throttle;
            //if (hit.transform.collider == waypoint.waypoints[waypointCounter])
            //{
            //    car.steering = 0f;
            //}
        }
        
        car.steering = steeringAngle;

        if(car.drivetrain.drivenGear == 0 ){
            car.drivetrain.drivenGear = 1;
        }
        
	}

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Waypoint" && waypointCounter < waypoint.waypoints.Length)
        {
            if (Other == waypoint.waypoints[waypointCounter])
            {
                waypointCounter++;
                Debug.Log("Waypoint Counter : " + waypointCounter);
            }
        }
    }
}
