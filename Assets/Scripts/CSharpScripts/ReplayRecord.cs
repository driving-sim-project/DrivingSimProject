using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ReplayRecord : MonoBehaviour {

    List<RecordedFrame> frames = new List<RecordedFrame>();

	// Update is called once per frame
	void Update () {
        RecordedFrame currentFrame = new RecordedFrame(transform.position, transform.rotation);
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
