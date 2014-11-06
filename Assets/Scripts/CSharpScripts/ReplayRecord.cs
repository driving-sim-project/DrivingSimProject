﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(CarController))]
public class ReplayRecord : MonoBehaviour {

    List<RecordedFrame> frames = new List<RecordedFrame>();
    CarController car;

    void Start()
    {
        car = GetComponent(typeof(CarController)) as CarController;
    }

	// Update is called once per frame
	void Update () {
        RecordedFrame currentFrame = new RecordedFrame(car);
        frames.Add(currentFrame);
        if (Input.GetButton("Enter"))
            Save();
	}

    void Save()
    {
        RecordedMotion motion = ScriptableObject.CreateInstance<RecordedMotion>();
        motion.frames = frames;
        motion.currentFrameNumber = 1;
        AssetDatabase.CreateAsset(motion, "Assets/ReplayRecording.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = motion;
    }
}
