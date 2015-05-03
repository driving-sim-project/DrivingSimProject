using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class RecordedMotion {

    public int avgSpeed = 0;
    public float distance = 0f;
    public int topSpeed = 0;
    public int currentFrameNumber;
    public List<string> gazingNameList = new List<string>();
    public List<int> gazingPerList = new List<int>();
    public bool isAccident;
    public bool isOffTrack;
    public bool isFinish;
    public bool isAi = false;
    public string recordFile { get; private set; }
    public string objectName { get; private set; }

    bool final = false;
    int gazingIndex = -1;
    string recordPath;
    [System.NonSerialized]
    StreamWriter file;
    [System.NonSerialized]
    BinaryFormatter bf;
    

    public void CreateFile(string fileName, string npcName , bool Ai)
    {
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        recordPath = Application.dataPath + "/Replays/" + Application.loadedLevelName + "/" + System.DateTime.Now.ToString("_yyyy-MM-dd_HH-mm") + "/";
        recordFile = fileName;
        objectName = npcName;
        isAi = Ai;
        if (Directory.Exists(recordPath) == false)
            Directory.CreateDirectory(recordPath);
        file = new StreamWriter(recordPath + recordFile + ".dtm", true); //you can call it anything you want
    }

    public void AddFrame(PlayerFrame rm)
    {
        if (final == false)
        {
            UI.frames.Add(rm);
            foreach(string text in RecordedFrame.txtForm(rm)){
                file.Write(text);
            }
            file.WriteLine();
            file.Flush();
            if (distance < 1f)
                distance = 0f;
            avgSpeed += rm.speed;
            if (topSpeed < rm.speed)
                topSpeed = rm.speed;
            if (gazingNameList.Contains(rm.gazingObjectName) == false)
            {
                gazingNameList.Add(rm.gazingObjectName);
                gazingPerList.Add(1);
            }
            else
            {
                gazingIndex = gazingNameList.IndexOf(rm.gazingObjectName);
                gazingNameList[gazingIndex] += 1;
            }
            distance = rm.currentDistance;
        }
    }

    public void AddFrame(AiFrame rm)
    {
        if (final == false)
        {
            foreach (string text in RecordedFrame.txtForm(rm))
            {
                file.Write(text);
            }
            file.WriteLine();
            file.Flush();
        }
    }

    public void Finalize()
    {
        final = true;
        avgSpeed = (int)(avgSpeed / UI.frames.Count);
        file.Flush();
        file.Close();

        string extension = "";

        if (isAi == false)
        {
            UI.record = this;
            extension = ".dmp";
        }
        else
        {
            extension = ".dmb";
        }
        FileStream fileStream = new FileStream(recordPath + recordFile + extension, FileMode.Create); //you can call it anything you want
        bf = new BinaryFormatter();
        bf.Serialize(fileStream, this);
        fileStream.Flush();
        fileStream.Close();
    }
}
