using UnityEngine;
using System.Collections;

public class GazeCamera : MonoBehaviour {

    private Camera mainCam;

    public float rightPanelSize = 0.2f;
    public float leftPanelSize = 0f;
    public int gazeSpeed = 1;


	// Use this for initialization
	void Start () {
        mainCam = this.camera;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(transform.localRotation.eulerAngles.y);
        if (Input.mousePosition.x > Screen.width * (1.0f - rightPanelSize))
        {
            if (transform.localRotation.eulerAngles.y < 20f || transform.localRotation.eulerAngles.y > 330f)
                transform.Rotate(Vector3.up, gazeSpeed * Time.deltaTime);
        }
        else if (Input.mousePosition.x < Screen.width * leftPanelSize)
        {
            if (transform.localRotation.eulerAngles.y > 340f || transform.localRotation.eulerAngles.y < 30f)
                transform.Rotate(Vector3.up, -gazeSpeed * Time.deltaTime);
        }
        else
        {
            if (transform.localRotation.eulerAngles.y < 359f && transform.localRotation.eulerAngles.y > 30f)
                transform.Rotate(Vector3.up, gazeSpeed * Time.deltaTime);
            else if (transform.localRotation.eulerAngles.y > 1f && transform.localRotation.eulerAngles.y < 350f)
                transform.Rotate(Vector3.up, -gazeSpeed * Time.deltaTime);
        }
	}
}
