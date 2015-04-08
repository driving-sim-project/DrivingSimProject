using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour {

    public TrafficLight[] trafficLightList;
    int trafficLightIndicator = 0;
    float lastTrafficLightSignal = 0;

	// Use this for initialization
	void Start () {
        trafficLightSetup();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > lastTrafficLightSignal + trafficLightList[trafficLightIndicator].greenLightInterval)
        {
            trafficLightIndicator += 1;
            trafficLightIndicator %= trafficLightList.Length;
            trafficLightSetup();
        }
	}

    void trafficLightSetup()
    {
        lastTrafficLightSignal = Time.time;
        foreach (TrafficLight tl in trafficLightList)
        {
            if (tl == trafficLightList[trafficLightIndicator]){
                StartCoroutine(tl.GreenLight());
            }
            else
            {
                if(tl.isRed == false){
                    StartCoroutine(tl.RedLight());
                }
            }
                
        }
    }
}
