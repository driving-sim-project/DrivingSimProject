using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent(typeof(ReplayRecord))]
public class TrafficChecker : MonoBehaviour {

    private List<CollisionData> colliderList = new List<CollisionData>();
    public ColliderList[] checkpointList;
    public static bool isAccident {  get; private set; }
    public int trafficRulesViolentNums = 0;

    bool loading = false;

    ReplayRecord replayRec;
    RecordedFrame replayFrameTmp;

    public AudioClip accidentSFX;
    public Sprite accident;
    public Sprite nowloading;

    void Awake()
    {
        isAccident = false;
        Time.timeScale = 1f;
    }


    void Start()
    {
        replayRec = GetComponent(typeof(ReplayRecord)) as ReplayRecord;
    }

	// Update is called once per frame
	void Update () {
        if (isAccident == true)
        {
            if (Input.GetButtonDown("Enter"))
            {
                loading = true;
                ToMainMenu();
            }
        }
	}


    void OnTriggerEnter( Collider Other )
    {
        if( replayFrameTmp == null  ){
            replayFrameTmp = replayRec.currentFrame;
        }
        if (Other.tag == "TrafficLine")
        {
            if (colliderList.Exists(x => x.colliderID == Other.transform.parent.GetInstanceID()) == false)
            {
                Debug.Log("You're Hitting " + Other.transform.parent.GetInstanceID());
                colliderList.Add(new CollisionData(Other.transform.parent.tag, Other.transform.parent.GetInstanceID(), Time.time));
            }
        }
        else if (Other.tag == "SignDetectionLine")
        {
            if (replayRec.currentFrame.currentDistance - replayFrameTmp.currentDistance > 2f)
            {
                if (Other.transform.parent.GetComponent<TurnSignChecker>().startPoint.GetInstanceID() == Other.GetInstanceID())
                {
                    Other.transform.parent.GetComponent<TurnSignChecker>().EnterCorner(replayRec.currentFrame.currentDistance);
                }
                else if (Other.transform.parent.GetComponent<TurnSignChecker>().endPoint.GetInstanceID() == Other.GetInstanceID())
                {
                    Debug.Log(Other.transform.parent.GetComponent<TurnSignChecker>().LeaveCorner(replayRec.currentFrame.currentDistance));
                }
            }
            replayFrameTmp = replayRec.currentFrame;
        }    
        
    }

    void OnTriggerExit( Collider Other )
    {
        if (Other.tag == "TrafficLine")
        {
            if (colliderList.Exists(x => x.colliderID == Other.transform.parent.GetInstanceID()) == true)
            {
                colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).LeaveTime(Time.time);
            }
            Debug.Log("You're Leaving " + Other.transform.parent.GetInstanceID() + " from " + colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).collisionTime + " at " + colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).leaveTime);
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
        if (isAccident == true)
        {
            if (loading == true)
                GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), nowloading.texture, ScaleMode.StretchToFill);
            else
                GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), accident.texture, ScaleMode.StretchToFill);
        }
    }

    void ToMainMenu()
    {
        replayRec.Save();
        Application.LoadLevel(0);
    }

}
