using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent(typeof(CarController))]
public class ReplayRecord : MonoBehaviour
{

    List<RecordedFrame> frames = new List<RecordedFrame>();
    CarController car;
    TrafficChecker trafficChecker;
    public RecordedFrame currentFrame { get; private set; }
    RecordedFrame tmpFrame;

    void Awake()
    {
        car = GetComponent(typeof(CarController)) as CarController;
        trafficChecker = GetComponent(typeof(TrafficChecker)) as TrafficChecker;
    }

	// Update is called once per frame
	void Update () {
        currentFrame = new RecordedFrame(car);
        if (tmpFrame == null){
            currentFrame.currentDistance = 0f;
        }
        else
        {
            currentFrame.currentDistance = Vector3.Distance(Converter.ConvertVector3(tmpFrame.position), Converter.ConvertVector3(currentFrame.position));
            currentFrame.currentDistance += tmpFrame.currentDistance;
        }
        frames.Add(currentFrame);
        tmpFrame = currentFrame;
	}

    public void Save()
    {
        if (Directory.Exists(Application.dataPath + "/Replays/") == false)
            Directory.CreateDirectory(Application.dataPath + "/Replays/");
        RecordedMotion motion = new RecordedMotion();
        motion.Init(frames, 1);
        motion.isAccident = trafficChecker.isAccident;
        motion.isOffTrack = trafficChecker.isOffTrack;
        motion.isFinish = trafficChecker.isFinish;
        
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.dataPath + "/Replays/" + Application.loadedLevelName + System.DateTime.Now.ToString("_yyyy-MM-dd_HH-mm") + ".dtss"); //you can call it anything you want
        bf.Serialize(file, motion);
        file.Close();
        Application.LoadLevel("startmenu");
    }
}
