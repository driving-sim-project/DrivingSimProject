using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficChecker : MonoBehaviour {

    private List<CollisionData> colliderList = new List<CollisionData>();

    public static bool isAccident {  get; private set; }

    public Sprite accident;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    void OnTriggerEnter( Collider Other )
    {
        
        if(colliderList.Exists(x => x.colliderID == Other.transform.parent.GetInstanceID()) == false){
            Debug.Log("You're Hitting " + Other.transform.parent.GetInstanceID());
            colliderList.Add(new CollisionData(Other.transform.parent.tag, Other.transform.parent.GetInstanceID(), Time.time));
        }
    }

    void OnTriggerExit( Collider Other )
    {
        if (colliderList.Exists(x => x.colliderID == Other.transform.parent.GetInstanceID()) == true)
        {
            colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).LeaveTime(Time.time);
        }
        Debug.Log("You're Leaving " + Other.transform.parent.GetInstanceID() + " from " + colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).collisionTime + " at " + colliderList.Find(x => x.colliderID == Other.transform.parent.GetInstanceID()).leaveTime);
    }

    void OnCollisionEnter( Collision collision )
    {
        Time.timeScale = 0.0f;
        isAccident = true;
        Debug.Log("You crush " + collision.gameObject.tag + "!!");
    }


    void OnGUI()
    {
        if (isAccident == true)
        {
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), accident.texture, ScaleMode.StretchToFill);
        }
    }

}
