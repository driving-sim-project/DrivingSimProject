using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(CarController))]
public class ReplayRecord : MonoBehaviour {

    List<RecordedFrame> frames = new List<RecordedFrame>();
    CarController car;
    public RecordedFrame currentFrame { get; private set; }
    RecordedFrame tmpFrame;

    void Start()
    {
        car = GetComponent(typeof(CarController)) as CarController;
    }

	// Update is called once per frame
	void Update () {
        currentFrame = new RecordedFrame(car);
        if (tmpFrame == null)
            currentFrame.currentDistance = 0f;
        else
            currentFrame.currentDistance = Vector3.Distance(tmpFrame.position, currentFrame.position);
        if(tmpFrame != null)
            currentFrame.currentDistance += tmpFrame.currentDistance;
        frames.Add(currentFrame);
        tmpFrame = currentFrame;

	}

    public void Save()
    {
        RecordedMotion motion = ScriptableObject.CreateInstance<RecordedMotion>();
        motion.Init(frames, 1);
        AssetDatabase.CreateAsset(motion, "Assets/Resources/Replays/" + Application.loadedLevelName + System.DateTime.Now.ToString("_yyyy-MM-dd_HH-mm") + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = motion;
        Application.LoadLevel("startmenu");
    }
}
