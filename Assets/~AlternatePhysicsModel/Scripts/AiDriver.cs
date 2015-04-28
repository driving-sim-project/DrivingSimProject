using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarController))]
public class AiDriver : MonoBehaviour {

    CarController car;

    public GameObject[] leftSensor = new GameObject[2];
    public GameObject[] rightSensor = new GameObject[2];
    public GameObject frontSensor;
    public bool headlight = false;
    public float throttle = 0.5f;
    public int speedLimit = 60;
    public Waypoint waypoint;
    float frontDistance = 5f;
    public bool loopRun = false;
    public int waypointCounter = 0;

    bool decelerate = false;
    int nodeSpeed = 0;
    float calDistance;

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
        Debug.DrawRay(frontSensor.transform.position, front * calDistance, Color.red);
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
            if (Mathf.Abs(angleTmp) > 60f && car.speed > 30)
            {
                car.accelKey = -1f;
            }
            if (car.speed < 30){
                car.accelKey = throttle;
            }
            else
            {
                car.accelKey = -throttle * 0.5f;
            }
        }
        else if(decelerate == true){
            if(car.speed > nodeSpeed)
                car.accelKey = -throttle * 0.5f;
            else if (car.speed < speedLimit / 2)
                car.accelKey = throttle;
            else
                car.accelKey = throttle * 0.5f;
        }
        else if (car.speed < speedLimit)
        {
            if (Mathf.Abs(steeringAngle) >= 0.15f)
            {
                if(car.speed > 60)
                    car.accelKey = -throttle;
                if (car.speed < 60)
                    car.accelKey = throttle;
            }
            else
            {
                if(car.speed < 20)
                    car.accelKey = throttle * 1.75f;
                else
                    car.accelKey = throttle;
            }

        }
        else
        {
            car.accelKey = 0f;
        }

        Vector3 carDirection = new Vector3(car.transform.forward.x, 0f, car.transform.forward.z);

        if (car.speed > 100)
            calDistance = frontDistance * 5f;
        else if (car.speed > 70)
            calDistance = frontDistance * 3f;
        else
            calDistance = frontDistance;


        Debug.DrawRay(frontSensor.transform.position, carDirection * calDistance, Color.blue);

        if (Physics.Raycast(frontSensor.transform.position, carDirection, out hit, calDistance))
        {
            if (hit.transform.tag.Contains("Car") == true)
            {
                car.accelKey = -throttle;
            }
            else if (hit.transform.tag == "Stopline")
            {
                car.accelKey = -1f;
            }
        }

        Vector3[] left = new Vector3[2];
        left[0] = leftSensor[0].transform.forward;
        left[1] = leftSensor[1].transform.forward;
        left[0].Set(left[0].x, 0f, left[0].z);
        left[1].Set(left[1].x, 0f, left[1].z);
        Vector3[] right = new Vector3[2];
        right[0] = rightSensor[0].transform.forward;
        right[1] = rightSensor[1].transform.forward;
        right[0].Set(right[0].x, 0f, right[0].z);
        right[1].Set(right[1].x, 0f, right[1].z);
        

        if (Physics.Raycast(leftSensor[0].transform.position, carDirection, out hit, 5f))
        {
            if (hit.transform.tag.Contains("Car") == true)
            {
                car.accelKey = -throttle;
            }
        }
        Debug.DrawRay(leftSensor[0].transform.position, frontSensor.transform.forward * 5f, Color.blue);

        if (Physics.Raycast(rightSensor[0].transform.position, carDirection, out hit, 5f))
        {
            if (hit.transform.tag.Contains("Car") == true)
            {
                car.accelKey = -throttle;
            }
        }
        Debug.DrawRay(rightSensor[0].transform.position, frontSensor.transform.forward * 5f, Color.blue);


        if (car.sidelightSL == true)
        {
            carDirection = new Vector3(-car.transform.right.x, 0f, -car.transform.right.z);
            if (Physics.Raycast(leftSensor[1].transform.position, carDirection, out hit, 2.5f))
            {
                if (hit.transform.tag.Contains("Car") == true)
                {
                    car.accelKey = -throttle * 0.5f;
                }
            }
            Debug.DrawRay(leftSensor[1].transform.position, carDirection * 2.5f, Color.green);
        }

        if (car.sidelightSR == true)
        {
            carDirection = new Vector3(car.transform.right.x, 0f, car.transform.right.z);
            if (Physics.Raycast(rightSensor[1].transform.position, carDirection, out hit, 2.5f))
            {
                if (hit.transform.tag.Contains("Car") == true)
                {
                    car.accelKey = -throttle * 0.5f;
                }
            }
            Debug.DrawRay(rightSensor[1].transform.position, carDirection * 2.5f, Color.green);
        }

        if (Mathf.Abs(steeringAngle) < 0.05f)
            steeringAngle = 0f;
        car.steering = steeringAngle;

        if(car.drivetrain.drivenGear == 0 )
            car.drivetrain.drivenGear = 1;
        
	}

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Waypoint" && waypointCounter < waypoint.waypoints.Length)
        {
            if (Other == waypoint.waypoints[waypointCounter])
            {
                AiNode nodeTmp = Other.GetComponent<AiNode>();
                car.sidelightSL = nodeTmp.sidelightL;
                car.sidelightSR = nodeTmp.sidelightR;
                car.headlight.SetActive(nodeTmp.headlight);
                decelerate = nodeTmp.decelerate;
                nodeSpeed = nodeTmp.speed;
                waypointCounter++;
            }
        }
    }
}
