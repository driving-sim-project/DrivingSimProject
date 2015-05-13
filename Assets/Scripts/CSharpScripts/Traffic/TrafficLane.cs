using UnityEngine;
using System.Collections;

public class TrafficLane : MonoBehaviour {

    public bool forward = false;
    public bool leftTurn = false;
    public bool rightTurn = false;
    public Collider startPoint;
    public Collider lanePath;
    public Collider endPoint;
    public Collider endPointL;
    public Collider endPointR;

	// Use this for initialization
	void Start () {
        setStartPoint(true);
        setEndPoint(false);
	}

    public void setEndPoint(bool active)
    {
        if (forward == true)
            endPoint.gameObject.SetActive(active);
        if (leftTurn == true)
            endPointL.gameObject.SetActive(active);
        if (rightTurn == true)
            endPointR.gameObject.SetActive(active);
    }

    //public void resetLane()
    //{
    //    endPoint.gameObject.SetActive(false);
    //    endPointL.gameObject.SetActive(false);
    //    endPointR.gameObject.SetActive(false);
    //}

    public void setStartPoint(bool active)
    {
        startPoint.gameObject.SetActive(active);
    }

}
