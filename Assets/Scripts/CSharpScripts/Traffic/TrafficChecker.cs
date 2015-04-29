using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent(typeof(ReplayRecord))]
[RequireComponent(typeof(CarController))]
public class TrafficChecker : MonoBehaviour {

    public TrafficLightController[] trafficLightList;
    public Checkpoint[] checkpointList;
    public bool isAccident { get; private set; }
    public bool isFinish { get; private set; }
    public int trafficRulesViolentNums = 0;
    public bool isOffTrack = false;

    bool isCrossingLane = false;
    bool loading = false;
    float cpDistance = 0f;

    ReplayRecord replayRec;

    public AudioClip accidentSFX;
    public Sprite accident;
    public Sprite nowloading;
    public Sprite offtrack;
    public Sprite finish;
    public GUIStyle directionBoxStyle = new GUIStyle(GUI.skin.box);
    int cpCounter = 0;
    int currentCp = -1;

    void Start()
    {
        replayRec = GetComponent(typeof(ReplayRecord)) as ReplayRecord;
        UI.inti = new List<Intugate>();
        List<Checkpoint> checkpointListTmp = new List<Checkpoint>();

        foreach (Checkpoint cpListTmp in checkpointList)
        {
            if (checkpointListTmp.Exists(x => x == cpListTmp) == false)
            {
                checkpointListTmp.Add(cpListTmp);
            }
        }

        foreach (Checkpoint cpList in checkpointListTmp)
        {
            foreach (Intugate cpRule in cpList.rules)
            {
                UI.inti.Add(cpRule);
                Debug.Log(cpRule.loadname() + " Added.");
            }
        }

        UI.intu = new List<Intugate>();

        if(checkpointList.Length > 0){
            foreach (Intugate rule in checkpointList[0].rules)
            {
                UI.intu.Add(rule);
                Debug.Log("Checkpoint" + rule.loadname() + " " + rule.setRefObj + " Added.");
            }
        }

        isAccident = false;
        isFinish = false;
        loading = false;
        Time.timeScale = 1f;
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

        if (replayRec.currentFrame != null)
        {
            foreach (string tag in replayRec.currentFrame.wheelsOnLine)
            {
                if (tag == "TrafficLine")
                {
                    isCrossingLane = true;
                }
                else if (tag == "Field")
                {
                    isOffTrack = true;
                }
            }
            replayRec.currentFrame.isCrossing = isCrossingLane;
        }
        else
            Debug.Log("Initializing");

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
            if (Other.tag == "SignDetectionLine")
            {
                if (Other.transform.parent.GetComponent<TurnSignChecker>().startPoint.GetInstanceID() == Other.GetInstanceID())
                {
                    Other.transform.parent.GetComponent<TurnSignChecker>().EnterCorner(replayRec.currentFrame.currentDistance);
                }
                else if (Other.transform.parent.GetComponent<TurnSignChecker>().endPoint.GetInstanceID() == Other.GetInstanceID())
                {
                    if (Other.transform.parent.GetComponent<TurnSignChecker>().LeaveCorner(replayRec.currentFrame.currentDistance) == true)
                    {
                        UI.intu.Find(x => x.setRefObj == Other.transform.parent).failed = true;
                    }
                }
            }
            else if(Other.tag == "Stopline")
            {
                foreach(TrafficLightController TrafficLightTmp in trafficLightList){
                    foreach( TrafficLightSign lightTmp in TrafficLightTmp.trafficLightList ){
                        if(Other == lightTmp.stopLine){
                            UI.intu.Find(x => x.setRefObj == TrafficLightTmp.transform).failed = true;
                        }
                    }
                }
                
            }
            else if(Other.tag == "Checkpoint")
            {
                if (replayRec.currentFrame.currentDistance > cpDistance + 10f)
                {
                    if (checkpointList[cpCounter].collider == Other)
                    {
                        cpDistance = replayRec.currentFrame.currentDistance;

                        foreach (Intugate cp in checkpointList[cpCounter].rules)
                        {
                            foreach(Intugate rule in UI.inti){
                                if (cp.loadname() == rule.loadname() && cp.setRefObj == rule.setRefObj)
                                {
                                    if (UI.intu.Exists(x => (x.loadname() == rule.loadname()) && (x.setRefObj == rule.setRefObj)) == false)
                                    {
                                        UI.intu.Add(rule);
                                        Debug.Log("Checkpoint" + rule.loadname() +" "+ rule.setRefObj + " Added.");
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
            else
            {
                if (currentCp != cpCounter - 1)
                {
                    currentCp = cpCounter - 1;
                }

                if (currentCp > -1)
                {
                    directionBoxStyle.fontSize = (int)(Screen.height * 0.05f);
                    Rect directionBox = new Rect(Screen.width / 4f, Screen.height / 4f, Screen.width / 2f, GUI.skin.box.CalcHeight(new GUIContent(checkpointList[currentCp].direction), Screen.width / 2f));
                    GUI.Box(directionBox, checkpointList[currentCp].direction, directionBoxStyle);
                }
            }
        }
        
    }

    void ToMainMenu()
    {
        replayRec.Save();
        Application.LoadLevel("scoreboard");
    }
}
