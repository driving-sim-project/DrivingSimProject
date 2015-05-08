using UnityEngine;
using System.Collections;

// This class is repsonsible for controlling inputs to the car.
// Change this code to implement other input types, such as support for analogue input, or AI cars.
[RequireComponent(typeof (Drivetrain))]
public class CarController : MonoBehaviour {

    //Headlight & Taillight object
    public GameObject headlight;
    public GameObject taillight;
    public GameObject rearlight;
    public GameObject sidelightL;
    public GameObject sidelightR;
    public GameObject steeringWheel;

	// Add all wheels of the car here, so brake and steering forces can be applied to them.
	public Wheel[] wheels;
	
	// A transform object which marks the car's center of gravity.
	// Cars with a higher CoG tend to tilt more in corners.
	// The further the CoG is towards the rear of the car, the more the car tends to oversteer. 
	// If this is not set, the center of mass is calculated from the colliders.
	public Transform centerOfMass;

	// A factor applied to the car's inertia tensor. 
	// Unity calculates the inertia tensor based on the car's collider shape.
	// This factor lets you scale the tensor, in order to make the car more or less dynamic.
	// A higher inertia makes the car change direction slower, which can make it easier to respond to.
	public float inertiaFactor = 1.5f;
	
	// current input state
	float brake;
	float throttle;
	float throttleInput;
	public float steering;
	float lastShiftTime = -1;
	float handbrake;
		
	// cached Drivetrain reference
	public Drivetrain drivetrain;

	// How long the car takes to shift gears
	public float shiftSpeed = 0.8f;
	

	// These values determine how fast throttle value is changed when the accelerate keys are pressed or released.
	// Getting these right is important to make the car controllable, as keyboard input does not allow analogue input.
	// There are different values for when the wheels have full traction and when there are spinning, to implement 
	// traction control schemes.
		
	// How long it takes to fully engage the throttle
	public float throttleTime = 1.0f;
	// How long it takes to fully engage the throttle 
	// when the wheels are spinning (and traction control is disabled)
	public float throttleTimeTraction = 10.0f;
	// How long it takes to fully release the throttle
	public float throttleReleaseTime = 0.5f;
	// How long it takes to fully release the throttle 
	// when the wheels are spinning.
	public float throttleReleaseTimeTraction = 0.1f;

	// Turn traction control on or off
	public bool tractionControl = true;
	
	
	// These values determine how fast steering value is changed when the steering keys are pressed or released.
	// Getting these right is important to make the car controllable, as keyboard input does not allow analogue input.
	
	// How long it takes to fully turn the steering wheel from center to full lock
	public float steerTime = 1.2f;
	// This is added to steerTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerTime = 0.1f;

	// How long it takes to fully turn the steering wheel from full lock to center
	public float steerReleaseTime = 0.6f;
	// This is added to steerReleaseTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerReleaseTime = 0f;
	// When detecting a situation where the player tries to counter steer to correct an oversteer situation,
	// steering speed will be multiplied by the difference between optimal and current steering times this 
	// factor, to make the correction easier.
	public float steerCorrectionFactor = 4.0f;

    public float accelKey = 0;

    public int speed = 0;

    public bool sidelightSR = false;
    public bool sidelightSL = false;
    float sidelightInterval = 0.5f;
    float sidelightLastInterval = 0f;
    bool ai = false;
    bool replayer = false;

	// Used by SoundController to get average slip velo of all wheels for skid sounds.
	public float slipVelo {
		get {
			float val = 0.0f;
			foreach(Wheel w in wheels)
				val += w.slipVelo / wheels.Length;
			return val;
		}
	}

    void Awake()
    {
        if (GetComponent<AiDriver>() != null)
            ai = true;
        else
        {
            if (SceneManager.GoScene == "replay")
            {
                replayer = true;
            }
        }        
    }

	// Initialize
	void Start () 
	{
        //if (SceneManager.GoScene != "replay")
        //{
            
        //}
        if (centerOfMass != null)
            rigidbody.centerOfMass = centerOfMass.localPosition;
        rigidbody.inertiaTensor *= inertiaFactor;
        drivetrain = GetComponent(typeof(Drivetrain)) as Drivetrain;
	}
	
	void Update () 
	{
        if (replayer == false && ai == false)
        {
            // Steering
            Vector3 carDir = transform.forward;
            float fVelo = rigidbody.velocity.magnitude;
            Vector3 veloDir = rigidbody.velocity * (1 / fVelo);
            float angle = -Mathf.Asin(Mathf.Clamp(Vector3.Cross(veloDir, carDir).y, -1, 1));
            float optimalSteering = angle / (wheels[0].maxSteeringAngle * Mathf.Deg2Rad);
            if (fVelo < 1)
                optimalSteering = 0;

            float steerInput = Input.GetAxis("Horizontal");
            //Debug.Log(steerInput);

            //if (Input.GetKey(KeyCode.LeftArrow))
            //    steerInput = -1;
            //if (Input.GetKey(KeyCode.RightArrow))
            //    steerInput = 1;

            steeringWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + (Input.GetAxis("Horizontal") * -450f));

            if (steerInput < steering)
            {
                float steerSpeed = (steering > 0) ? (1 / (steerReleaseTime + veloSteerReleaseTime * fVelo)) : (1 / (steerTime + veloSteerTime * fVelo));
                if (steering > optimalSteering)
                    steerSpeed *= 1 + (steering - optimalSteering) * steerCorrectionFactor;
                steering -= steerSpeed * Time.deltaTime;
                if (steerInput > steering)
                    steering = steerInput;
            }
            else if (steerInput > steering)
            {
                float steerSpeed = (steering < 0) ? (1 / (steerReleaseTime + veloSteerReleaseTime * fVelo)) : (1 / (steerTime + veloSteerTime * fVelo));
                if (steering < optimalSteering)
                    steerSpeed *= 1 + (optimalSteering - steering) * steerCorrectionFactor;
                steering += steerSpeed * Time.deltaTime;
                if (steerInput < steering)
                    steering = steerInput;
            }

            steering = steerInput;

            // Throttle/Brake            
            accelKey = Input.GetAxis("Vertical");
            //bool brakeKey = Input.GetKey (KeyCode.DownArrow);

            //Debug.Log(accelKey);

            if (drivetrain.automatic && drivetrain.gear == 0)
            {
                accelKey = Input.GetAxis("Vertical");
            }
            if (GetComponent<TrafficChecker>().isAccident == true || GetComponent<TrafficChecker>().isOffTrack == true || GetComponent<TrafficChecker>().isFinish == true)
                accelKey = -1f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                throttle += Input.GetAxis("Vertical");
                throttleInput += Input.GetAxis("Vertical");
            }
            else if (accelKey > 0.0f)
            {
                //if (drivetrain.slipRatio < 0.10f)
                //    throttle += Time.deltaTime / throttleTime;
                //else if (!tractionControl)
                //    throttle += Time.deltaTime / throttleTimeTraction;
                //else
                //    throttle -= Time.deltaTime / throttleReleaseTime;
                throttle = accelKey;
                throttleInput = accelKey;
            }
            else
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    throttle -= Time.deltaTime / throttleReleaseTime;
                //else
                //    throttle -= Time.deltaTime / throttleReleaseTimeTraction;
                throttle = 0;
                throttleInput = 0;
            }
            throttle = Mathf.Clamp01(throttle);

            if (accelKey < 0.0f)
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    brake += Time.deltaTime / throttleTime;
                //else
                //    brake += Time.deltaTime / throttleTimeTraction;
                taillight.gameObject.SetActive(true);
                brake = -accelKey;
                throttleInput = -accelKey;
            }
            else
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    brake -= Time.deltaTime / throttleReleaseTime;
                //else
                //    brake -= Time.deltaTime / throttleReleaseTimeTraction;
                taillight.gameObject.SetActive(false);
                brake = 0;
                throttleInput = 0;
            }
            //brake = Mathf.Clamp01 (brake);
            //throttleInput = throttleInput;

            // Handbrake
            //handbrake = Mathf.Clamp01(handbrake + (Input.GetKey(KeyCode.Space) ? Time.deltaTime : -Time.deltaTime));

            // Gear shifting
            float shiftThrottleFactor = Mathf.Clamp01((Time.time - lastShiftTime) / shiftSpeed);
            drivetrain.throttle = throttle * shiftThrottleFactor;
            drivetrain.throttleInput = throttleInput;

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    lastShiftTime = Time.time;
            //    drivetrain.ShiftUp();
            //}
            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    lastShiftTime = Time.time;
            //    drivetrain.ShiftDown();
            //}

            // Apply inputs
            foreach (Wheel w in wheels)
            {
                w.brake = brake;
                w.handbrake = handbrake;
                w.steering = steering;
            }

            if (drivetrain.gear == 0)
                rearlight.gameObject.SetActive(true);
            else
                rearlight.gameObject.SetActive(false);

            //Headlight
            if (Input.GetButtonDown("R3"))
                headlight.gameObject.SetActive(!headlight.gameObject.activeInHierarchy);

            //Sidelight switch
            //R
            if (Input.GetButtonDown("R2"))
            {
                sidelightSL = false;
                sidelightSR = sidelightSR == true ? sidelightSR = false : sidelightSR = true;
                sidelightLastInterval = Time.time;
            }
            //L
            if (Input.GetButtonDown("R1"))
            {
                sidelightSR = false;
                sidelightSL = sidelightSL == true ? sidelightSL = false : sidelightSL = true;
                sidelightLastInterval = Time.time;
            }

            

            speed = (int)(rigidbody.velocity.magnitude * 3.6f);
        }else if(ai == true){
            // Steering
            //Vector3 carDir = transform.forward;
            //float fVelo = rigidbody.velocity.magnitude;
            //Vector3 veloDir = rigidbody.velocity * (1 / fVelo);
            //float angle = -Mathf.Asin(Mathf.Clamp(Vector3.Cross(veloDir, carDir).y, -1, 1));
            //float optimalSteering = angle / (wheels[0].maxSteeringAngle * Mathf.Deg2Rad);
            //if (fVelo < 1)
            //    optimalSteering = 0;

            //float steerInput = Input.GetAxis("Horizontal");
            //Debug.Log(steerInput);

            //if (Input.GetKey(KeyCode.LeftArrow))
            //    steerInput = -1;
            //if (Input.GetKey(KeyCode.RightArrow))
            //    steerInput = 1;

            //steeringWheel.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + (Input.GetAxis("Horizontal") * -450f));

            //if (steerInput < steering)
            //{
            //    float steerSpeed = (steering > 0) ? (1 / (steerReleaseTime + veloSteerReleaseTime * fVelo)) : (1 / (steerTime + veloSteerTime * fVelo));
            //    if (steering > optimalSteering)
            //        steerSpeed *= 1 + (steering - optimalSteering) * steerCorrectionFactor;
            //    steering -= steerSpeed * Time.deltaTime;
            //    if (steerInput > steering)
            //        steering = steerInput;
            //}
            //else if (steerInput > steering)
            //{
            //    float steerSpeed = (steering < 0) ? (1 / (steerReleaseTime + veloSteerReleaseTime * fVelo)) : (1 / (steerTime + veloSteerTime * fVelo));
            //    if (steering < optimalSteering)
            //        steerSpeed *= 1 + (optimalSteering - steering) * steerCorrectionFactor;
            //    steering += steerSpeed * Time.deltaTime;
            //    if (steerInput < steering)
            //        steering = steerInput;
            //}

            //steering = steerInput;

            // Throttle/Brake            
            //accelKey = Input.GetAxis("Vertical");
            //bool brakeKey = Input.GetKey (KeyCode.DownArrow);

            //Debug.Log(accelKey);

            //if (GetComponent<TrafficChecker>().isAccident == true || GetComponent<TrafficChecker>().isOffTrack == true || GetComponent<TrafficChecker>().isFinish == true)
            //    accelKey = -1f;
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    throttle += Input.GetAxis("Vertical");
            //    throttleInput += Input.GetAxis("Vertical");
            //}
            if (accelKey > 0.0f)
            {
                //if (drivetrain.slipRatio < 0.10f)
                //    throttle += Time.deltaTime / throttleTime;
                //else if (!tractionControl)
                //    throttle += Time.deltaTime / throttleTimeTraction;
                //else
                //    throttle -= Time.deltaTime / throttleReleaseTime;
                throttle = accelKey;
                throttleInput = accelKey;
            }
            else
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    throttle -= Time.deltaTime / throttleReleaseTime;
                //else
                //    throttle -= Time.deltaTime / throttleReleaseTimeTraction;
                throttle = 0;
                throttleInput = 0;
            }
            throttle = Mathf.Clamp01(throttle);

            if (accelKey < 0.0f)
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    brake += Time.deltaTime / throttleTime;
                //else
                //    brake += Time.deltaTime / throttleTimeTraction;
                taillight.gameObject.SetActive(true);
                brake = -accelKey;
                throttleInput = -accelKey;
            }
            else
            {
                //if (drivetrain.slipRatio < 0.2f)
                //    brake -= Time.deltaTime / throttleReleaseTime;
                //else
                //    brake -= Time.deltaTime / throttleReleaseTimeTraction;
                taillight.gameObject.SetActive(false);
                brake = 0;
                throttleInput = 0;
            }
            //brake = Mathf.Clamp01 (brake);
            //throttleInput = throttleInput;

            // Handbrake
            //handbrake = Mathf.Clamp01(handbrake + (Input.GetKey(KeyCode.Space) ? Time.deltaTime : -Time.deltaTime));

            // Gear shifting
            float shiftThrottleFactor = Mathf.Clamp01((Time.time - lastShiftTime) / shiftSpeed);
            drivetrain.throttle = throttle * shiftThrottleFactor;
            drivetrain.throttleInput = throttleInput;

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    lastShiftTime = Time.time;
            //    drivetrain.ShiftUp();
            //}
            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    lastShiftTime = Time.time;
            //    drivetrain.ShiftDown();
            //}

            // Apply inputs
            foreach (Wheel w in wheels)
            {
                w.brake = brake;
                w.handbrake = handbrake;
                w.steering = steering;
            }

            if (drivetrain.gear == 0)
                rearlight.gameObject.SetActive(true);
            else
                rearlight.gameObject.SetActive(false);

            //Headlight
            //if (Input.GetButtonDown("R3"))
            //    headlight.gameObject.SetActive(!headlight.gameObject.active);

            //Sidelight switch
            //R
            //if (Input.GetButtonDown("R2"))
            //{
            //    sidelightSL = false;
            //    sidelightSR = sidelightSR == true ? sidelightSR = false : sidelightSR = true;
            //    sidelightLastInterval = Time.time;
            //}
            //L
            //if (Input.GetButtonDown("R1"))
            //{
            //    sidelightSR = false;
            //    sidelightSL = sidelightSL == true ? sidelightSL = false : sidelightSL = true;
            //    sidelightLastInterval = Time.time;
            //}

            speed = (int)(rigidbody.velocity.magnitude * 3.6f);
        }

        if (sidelightSL == true)
        {
            if (Time.time <= sidelightLastInterval + sidelightInterval)
            {
                sidelightL.SetActive(true);
            }
            else if (Time.time >= sidelightLastInterval + (sidelightInterval * 2f))
            {
                sidelightLastInterval = Time.time;
            }
            else if (Time.time >= sidelightLastInterval + sidelightInterval)
            {
                sidelightL.SetActive(false);
            }
        }
        else
            sidelightL.SetActive(false);

        if (sidelightSR == true)
        {
            if (Time.time <= sidelightLastInterval + sidelightInterval)
            {
                sidelightR.SetActive(true);
            }
            else if (Time.time >= sidelightLastInterval + (sidelightInterval * 2f))
            {
                sidelightLastInterval = Time.time;
            }
            else if (Time.time >= sidelightLastInterval + sidelightInterval)
            {
                sidelightR.SetActive(false);

            }

        }
        else
            sidelightR.SetActive(false);

	}


	// Debug GUI. Disable when not needed.
    //void OnGUI()
    //{
    //    GUI.Label(new Rect(0, 60, 100, 200), "km/h: " + rigidbody.velocity.magnitude * 3.6f);
    //    //tractionControl = GUI.Toggle(new Rect(0, 80, 300, 20), tractionControl, "Traction Control (bypassed by shift key)");
    //}
}
