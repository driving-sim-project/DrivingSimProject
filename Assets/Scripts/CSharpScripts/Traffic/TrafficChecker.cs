﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent(typeof(ReplayRecord))]
[RequireComponent(typeof(CarController))]
public class TrafficChecker : MonoBehaviour {

    private List<CollisionData> colliderList = new List<CollisionData>();
    public Collider[] checkpointList;
    public string[] trafficList;
    public bool isAccident { get; private set; }
    public bool isFinish { get; private set; }
    public int trafficRulesViolentNums = 0;
    public bool isOffTrack = false;

    bool isCrossingLane = false;
    bool loading = false;
    float cpDistance = 0f;

    ReplayRecord replayRec;
    RecordedFrame replayFrameTmp;

    CarController car;

    public AudioClip accidentSFX;
    public Sprite accident;
    public Sprite nowloading;
    public Sprite offtrack;
    public Sprite finish;
    int cpCounter = 0;

    void Awake()
    {
        UI.inti = new List<Intugate>();
        foreach (string rulesList in trafficList)
        {
            foreach (string rule in rulesList.Split(','))
            {
                if(rule != ""){
                    UI.inti.Add((Intugate)System.Activator.CreateInstance(System.Type.GetType(rule)));
                }
            }
        }
        UI.intu = new List<Intugate>();
        isAccident = false;
        isFinish = false;

        Time.timeScale = 1f;
    }


    void Start()
    {
        replayRec = GetComponent(typeof(ReplayRecord)) as ReplayRecord;
        car = GetComponent(typeof(CarController)) as CarController;
    }

    void FixedUpdate()
    {
        isCrossingLane = false;
    }

	// Update is called once per frame
	void Update () {
        if (isAccident == true || isOffTrack == true || isFinish == true)
        {
            if (Input.GetButtonDown("Enter"))
            {
                loading = true;
                ToMainMenu();
            }
        }

        for (int i = 0; i < car.wheels.Length; i++ )
        {
            replayRec.currentFrame.wheelsOnLine[i] = car.wheels[i].onTag;
            if(car.wheels[i].onTag == "TrafficLine"){
                isCrossingLane = true;
            }
            else if(car.wheels[i].onTag == "Field")
            {
                isOffTrack = true;
            }
        }

        replayRec.currentFrame.isCrossing = isCrossingLane;

        replayFrameTmp = replayRec.currentFrame;

        //if(colliderList.Count > 0){
        //    isCrossingLane = true;
        //}
        //else
        //{
        //    isCrossingLane = false;
        //}

        //Debug.Log("is Crossing : " + isCrossingLane + "Hitting : " + colliderList.Count);
	}

    void OnTriggerStay( Collider Other )
    {
        if (Other.tag == "TrafficLine")
        {
            isCrossingLane = true;
        }
    }

    void OnTriggerEnter( Collider Other )
    {
        if (SceneManager.GoScene != "replay")
        {
            //if (Other.tag == "TrafficLine")
            //{
            //    if (colliderList.Exists(x => x.colliderID == Other.GetInstanceID()) == false)
            //    {
            //        //Debug.Log("You're Hitting " + Other.GetInstanceID());
            //        colliderList.Add(new CollisionData(Other.transform.parent.tag, Other.GetInstanceID()));
            //    }
            //}
            //else 
            if (Other.tag == "SignDetectionLine")
            {
                if (replayRec.currentFrame.currentDistance - replayFrameTmp.currentDistance > 2f)
                {
                    if (Other.transform.parent.GetComponent<TurnSignChecker>().startPoint.GetInstanceID() == Other.GetInstanceID())
                    {
                        Other.transform.parent.GetComponent<TurnSignChecker>().EnterCorner(replayRec.currentFrame.currentDistance);
                    }
                    else if (Other.transform.parent.GetComponent<TurnSignChecker>().endPoint.GetInstanceID() == Other.GetInstanceID())
                    {
                        ((noleft)UI.inti.Find(x => x.loadname()  == "No Left Turn")).failed = 
                        Other.transform.parent.GetComponent<TurnSignChecker>().LeaveCorner(replayRec.currentFrame.currentDistance);
                    }
                }
            }
            else if(Other.tag == "Checkpoint")
            {
                if (replayRec.currentFrame.currentDistance > cpDistance + 10f)
                {
                    if (checkpointList[cpCounter] == Other)
                    {
                        cpDistance = replayRec.currentFrame.currentDistance;

                        string[] trafficListTmp = trafficList[cpCounter].Split(',');

                        foreach (string name in trafficListTmp)
                        {
                            foreach(Intugate rule in UI.inti){
                                if (name == rule.GetType().ToString())
                                {
                                    if(UI.intu.Exists( x => x.GetType().ToString() == name ) == false){
                                        UI.intu.Add(rule);
                                    }
                                }
                            }
                        }

                        cpCounter += 1;
                        if (cpCounter == checkpointList.Length)
                        {
                            isFinish = true;
                        }

                    }
                    else
                    {
                        cpDistance = 0;
                        cpCounter = 1;
                    }
                }
            }
        }
    }

    //void OnTriggerExit( Collider Other )
    //{
    //    if(SceneManager.GoScene != "replay"){
    //        if (Other.tag == "TrafficLine")
    //        {
    //            if (colliderList.Exists(x => x.colliderID == Other.GetInstanceID()) == true)
    //            {
    //                colliderList.Remove(colliderList.Find(x => x.colliderID == Other.GetInstanceID()));
    //            }
    //            //Debug.Log("You're Leaving " + Other.GetInstanceID());
    //        }
    //    }
        
    //}

    void OnCollisionEnter( Collision collision )
    {
        AudioSource.PlayClipAtPoint(accidentSFX, collision.gameObject.transform.position);
        isAccident = true;
        Debug.Log("You crush " + collision.gameObject.tag + " !!");
    }


    void OnGUI()
    {
        if(SceneManager.GoScene != "replay"){
            if (isFinish == true)
            {
                if (loading == true)
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), nowloading.texture, ScaleMode.StretchToFill);
                else
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), finish.texture, ScaleMode.StretchToFill);
            }
            else if (isAccident == true)
            {
                if (loading == true)
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), nowloading.texture, ScaleMode.StretchToFill);
                else
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), accident.texture, ScaleMode.StretchToFill);
            }
            else if (isOffTrack == true)
            {
                if (loading == true)
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), nowloading.texture, ScaleMode.StretchToFill);
                else
                    GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), offtrack.texture, ScaleMode.StretchToFill);
            }
        }
        
    }

    void ToMainMenu()
    {
        replayRec.Save();
        Application.LoadLevel("scoreboard");
    }

}
