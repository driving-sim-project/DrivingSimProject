using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[RequireComponent(typeof(CarController))]
public class ReplayPlayer : MonoBehaviour {

    public Texture2D cursorImage;
    public CameraSwitch cameraController;
    public Material hitCollision;
    public Material unhitCollision;
    public Collider carCollision;
    public AudioClip accidentSFX;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    List<string> recordList = new List<string>();
    RecordedFrame frame = null;
    List<AiFrame> aiFrames;
    List<PlayerFrame> playerFrames;
    RecordedMotion recording;

    bool gui = true;
    float startTime;
    int fn;
    CarController car;
    bool playing = false;
    string path = "";

	// Use this for initialization
	void Start () {
        if (SceneManager.GoScene != "replay")
        {
            Destroy(this);
        }
        else
        {
            path = Application.dataPath + "/Replays/" + Application.loadedLevelName + "/";
            if (Directory.Exists(path) == true)
            {
                foreach (string pathname in Directory.GetDirectories(path))
                {
                    recordList.Add(pathname);
                }
                foreach (string data in Directory.GetFiles(recordList[recordList.Count - 1]))
                {
                    if (name.Contains("npc") == true)
                    {
                        if (data.Contains(".dmb") == true && data.Contains(".meta") == false)
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            FileStream file = File.Open(data, FileMode.Open);
                            recording = (RecordedMotion)bf.Deserialize(file);
                            file.Close();
                            if (recording.objectName == name)
                            {
                                aiFrames = new List<AiFrame>();
                                StreamReader reader = new StreamReader(recordList[recordList.Count - 1] + "/" + recording.recordFile + ".dtm");
                                string lineTmp;
                                while ((lineTmp = reader.ReadLine()) != null)
                                {
                                    aiFrames.Add((AiFrame)RecordedFrame.dataFrame(recording.isAi, lineTmp.Split(new char[] {','})));
                                }
                                reader.Close();
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (data.Contains(".dmp") == true && data.Contains(".meta") == false)
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            FileStream file = File.Open(data, FileMode.Open);
                            recording = (RecordedMotion)bf.Deserialize(file);
                            file.Close();
                            playerFrames = new List<PlayerFrame>();
                            StreamReader reader = new StreamReader(recordList[recordList.Count - 1] + "/" + recording.recordFile + ".dtm");
                            string lineTmp;
                            while ((lineTmp = reader.ReadLine()) != null)
                            {
                                playerFrames.Add((PlayerFrame)RecordedFrame.dataFrame(recording.isAi, lineTmp.Split(new char[] { ',' })));
                            }
                            reader.Close();
                            break;
                        }
                    }
                }

            }
            Debug.Log(recording.objectName + " frames: " + (recording.isAi == false ? playerFrames.Count : aiFrames.Count));
            car = GetComponent(typeof(CarController)) as CarController;
            car.rigidbody.isKinematic = true;
            //recording = recordList[recordList.Count - 1];
            ReplaySetup();
        }        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Circle") == true)
            gui = !gui;

        if (Input.GetButtonDown("Enter") == true)
        {
            Application.LoadLevel("StartMenu");
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            if (fn + (Input.GetAxis("Vertical") * 10) < (recording.isAi == false ? playerFrames.Count : aiFrames.Count))
                fn += (int)(Input.GetAxis("Vertical") * 10);
            else
                fn = (recording.isAi == false ? playerFrames.Count : aiFrames.Count);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (fn + (Input.GetAxis("Vertical") * 10) > 0)
                fn += (int)(Input.GetAxis("Vertical") * 10);
            else 
                fn = 0;
        }

	    if(recording != null){
            if (fn < (recording.isAi == false ? playerFrames.Count : aiFrames.Count ) - 1)
            {
                if (recording.isAi == false)
                    frame = playerFrames[fn];
                else
                    frame = aiFrames[fn];
                if (Time.time > startTime + 0.01f)
                {
                    fn++;
                    transform.position = Converter.ConvertVector3(frame.position);
                    transform.rotation = Converter.ConvertQuaternion(frame.rotation);
                    car.headlight.SetActive(frame.headlight);
                    car.rearlight.SetActive(frame.rearlight);
                    car.sidelightSL = frame.sidelightL;
                    car.sidelightSR = frame.sidelightR;
                    car.taillight.SetActive(frame.throttle >= 0 ? false : true);
                    for (int i = 0; i < car.wheels.Length; i++)
                    {
                        car.wheels[i].model.transform.localPosition = Converter.ConvertVector3(frame.wheelsPosition[i]);
                        car.wheels[i].model.transform.localRotation = Converter.ConvertQuaternion(frame.wheelsRotation[i]);
                    }
                    if (recording.isAi == false)
                    {
                        car.speed = ((PlayerFrame)frame).speed;
                        car.steeringWheel.transform.localRotation = Converter.ConvertQuaternion(((PlayerFrame)frame).steeringWheelRotation);
                        cameraController.camerasList[0].transform.rotation = Converter.ConvertQuaternion(((PlayerFrame)frame).cameraRotaion);
                    }
                    
                }
                startTime = Time.time;
            }
        }
	}

    void OnTriggerStay(Collider Other)
    {
        if (Other.tag.Contains("TrafficLine") == true && recording.isAi == false)
            carCollision.renderer.material = hitCollision;
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.tag.Contains("TrafficLine") == true && recording.isAi == false)
            carCollision.renderer.material = unhitCollision;
    }

    void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(accidentSFX, collision.gameObject.transform.position);
        carCollision.renderer.material = hitCollision;
    }


    void OnGUI()
    {
        if (recording.isAi == false)
        {
            PlayerFrame tmpFrame = ((PlayerFrame)frame);
            GUIStyle box = GUI.skin.box;
            box.alignment = TextAnchor.UpperCenter;
            if (playing == true && gui == true)
            {
                GUI.Box(new Rect(10, 10, 300, 300), "Driving Data", box);
                GUI.Label(new Rect(20, 40, 300, 30), "Speed : " + tmpFrame.speed + " km/h");
                GUI.Label(new Rect(20, 60, 300, 30), "Throttle : " + (tmpFrame.throttle > 0 ?
                    (int)(tmpFrame.throttle * 100f) : 0) + " %");
                GUI.Label(new Rect(20, 80, 300, 30), "Brake : " + (tmpFrame.throttle <= 0 ?
                    (int)(Mathf.Abs(tmpFrame.throttle) * 100f) : 0) + " %");
                GUI.Label(new Rect(20, 100, 300, 30), "Steering Wheel : " + (tmpFrame.steering == 0 ? "Center 0" :
                    (tmpFrame.steering > 0 ? "Left " + (int)Mathf.Abs(tmpFrame.steering) :
                    "Right " + (int)Mathf.Abs(tmpFrame.steering))) + " degree");
                GUI.Label(new Rect(20, 120, 300, 30), "Looking at : " + tmpFrame.gazingObjectName);
                GUI.Label(new Rect(20, 140, 300, 30), "Driving time : " + playerFrames[playerFrames.Count - 1].time + " sec");
                GUI.Label(new Rect(20, 160, 300, 30), "Average speed : " + recording.avgSpeed + " km/h");
                GUI.Label(new Rect(20, 180, 300, 30), "Top speed : " + recording.topSpeed + " km/h");
                GUI.Label(new Rect(20, 200, 300, 30), "Distance : " + recording.distance + " m");
                GUI.Label(new Rect(20, 220, 300, 30), "Wheel Angle : " + tmpFrame.wheelAngle[0] + " Degree");
                GUI.Label(new Rect(20, 240, 300, 30), "Headlight : " + (tmpFrame.headlight == true ? "On" : "Off"));
                GUI.Label(new Rect(20, 260, 300, 30), "R Turn Light : " + (tmpFrame.sidelightR == true ? "On" : "Off"));
                GUI.Label(new Rect(20, 280, 300, 30), "L Turn Light : " + (tmpFrame.sidelightL == true ? "On" : "Off"));


                GUI.Box(new Rect(Screen.width - 320, 10, 300, 20 + 30 * recording.gazingNameList.Count), "Gazing Statistics", box);
                for (int i = 0; i < recording.gazingNameList.Count; i++)
                    GUI.Label(new Rect(Screen.width - 320, 30 + 20 * i, 300, 30),
                        (recording.gazingNameList[i]) + " : " + (recording.gazingPerList[i] * 100f) / (recording.isAi == false ? playerFrames.Count : aiFrames.Count) + "%");
                GUI.DrawTexture(new Rect(tmpFrame.eyePosition.x, Screen.height - tmpFrame.eyePosition.y, cursorWidth, cursorHeight), cursorImage);

            }
            else
            {
                GUI.Box(new Rect(10, 10, 300, 300), "Replay Data List", box);
                //for (int i = 0; i < recordList.Count; i++)
                //{
                //    GUI.Label(new Rect(20, 30 + 20 * i, 300, 30), recordList[i]);
                //}

            }
        }
    }

    void ReplaySetup()
    {
        if (recording != null)
        {
            recording.currentFrameNumber = 0;
            fn = recording.currentFrameNumber;
        }
        playing = true;
        if (recording.isAi == false)
        {
            UI.record = recording;
            UI.frames = playerFrames;
        }
            
    }
}
