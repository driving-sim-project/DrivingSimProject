using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[RequireComponent(typeof(CarController))]
public class ReplayRecord : MonoBehaviour
{
    CarController car;
    TrafficChecker trafficChecker;
    public RecordedFrame currentFrame { get; private set; }
    RecordedFrame tmpFrame;
    FileStream file;
    BinaryFormatter bf;
    string recordFile;

    void Awake()
    {
        recordFile = Application.dataPath + "/Replays/" + Application.loadedLevelName + System.DateTime.Now.ToString("_yyyy-MM-dd_HH-mm") + ".dtss";
        car = GetComponent(typeof(CarController)) as CarController;
        trafficChecker = GetComponent(typeof(TrafficChecker)) as TrafficChecker;
        if (Directory.Exists(Application.dataPath + "/Replays/") == false)
            Directory.CreateDirectory(Application.dataPath + "/Replays/");
        UI.record = new RecordedMotion();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        file = new FileStream(recordFile, FileMode.Append); //you can call it anything you want
        bf = new BinaryFormatter();
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
        UI.record.AddFrame(currentFrame);
        bf.Serialize(file, currentFrame);
        file.Flush();
        tmpFrame = currentFrame;

	}

    public void Save()
    {
        file.Flush();
        UI.record.Finalize();
        UI.record.isAccident = trafficChecker.isAccident;
        UI.record.isOffTrack = trafficChecker.isOffTrack;
        UI.record.isFinish = trafficChecker.isFinish;
        file.Close();
    }
}
