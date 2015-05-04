using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CarController))]
public class ReplayRecord : MonoBehaviour
{
    public RecordedFrame currentFrame { get; private set; }

    RecordedMotion record = null;
    CarController car;
    TrafficChecker trafficChecker;
    RecordedFrame tmpFrame;
    bool isAi = false;

    void Start()
    {
        if (SceneManager.GoScene == "replay")
        {
            Destroy(GetComponent<TrafficChecker>());
            Destroy(this);
        }
        else
        {
            car = GetComponent(typeof(CarController)) as CarController;
            trafficChecker = GetComponent(typeof(TrafficChecker)) as TrafficChecker;
            record = new RecordedMotion();
            if (null != GetComponent<AiDriver>())
                isAi = true;
            record.CreateFile(transform.GetInstanceID().ToString(), name, isAi);
        }
            
    }

	// Update is called once per frame
	void Update () {
        if (null != tmpFrame)
        {
            if (Time.time > tmpFrame.time + 0.02f)
                RecordFrame();
        }
        else
        {
            RecordFrame();
        }
        
	}

    public void Save()
    {
        if (isAi == false)
        {
            record.isAccident = trafficChecker.isAccident;
            record.isOffTrack = trafficChecker.isOffTrack;
            record.isFinish = trafficChecker.isFinish;
        }
        record.Finalize();
    }

    private void RecordFrame()
    {
        if (isAi)
        {
            currentFrame = new AiFrame(car);
            record.AddFrame((AiFrame)currentFrame);
            tmpFrame = currentFrame;
        }
        else
        {
            currentFrame = new PlayerFrame(car);
            if (tmpFrame == null)
            {
                ((PlayerFrame)currentFrame).currentDistance = 0f;
            }
            else
            {
                ((PlayerFrame)currentFrame).currentDistance = Vector3.Distance(Converter.ConvertVector3(tmpFrame.position), Converter.ConvertVector3(currentFrame.position));
                ((PlayerFrame)currentFrame).currentDistance += ((PlayerFrame)tmpFrame).currentDistance;
            }
            record.AddFrame(((PlayerFrame)currentFrame));
            tmpFrame = ((PlayerFrame)currentFrame);
        }
    }
}
