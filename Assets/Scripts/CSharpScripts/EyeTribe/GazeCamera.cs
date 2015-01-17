using UnityEngine;
using System.Collections;
using TETCSharpClient;
using TETCSharpClient.Data;
using Assets.Scripts;

[RequireComponent(typeof(Camera))]
public class GazeCamera : MonoBehaviour, IGazeListener {

    //public float rightPanelSize = 0.2f;
    //public float leftPanelSize = 0f;
    //public float panelSize = 0.5f;
    //public int gazeSpeed = 1;

    public string currentGaze;
    public Vector3 screenPoint;

    private GazeDataValidator gazeUtils;
    private Camera cam;
    private Component gazeIndicator;
    private Vector3 camPosition;
    private double eyesDistance;
    private double baseDist;
    private double depthMod;

    void Start()
    {
        Screen.autorotateToPortrait = false;
        
        //gazeIndicator = cam.transform.GetChild(0);

        gazeUtils = new GazeDataValidator(30);
        GazeManager.Instance.Activate
        (
            GazeManager.ApiVersion.VERSION_1_0,
            GazeManager.ClientMode.Push
        );

        GazeManager.Instance.AddGazeListener(this);
    }

    void Awake()
    {
        if (SceneManager.GoScene == "replay")
            this.enabled = false;
        cam = GetComponent<Camera>();
        camPosition = cam.transform.localPosition;
    }

    public void OnGazeUpdate(GazeData gazeData)
    {
        gazeUtils.Update(gazeData);
    }

	// Update is called once per frame
	void Update () {
        Point2D userPos = gazeUtils.GetLastValidSmoothedGazeCoordinates();

        if (null != userPos)
        {
            //mapping cam panning to 3:2 aspect ratio
            //double tx = (userPos.X * 5) - 2.5f;
            //double ty = (userPos.Y * 3) - 1.5f;

            //position camera X-Y plane and adjust distance
            eyesDistance = gazeUtils.GetLastValidUserDistance();
            depthMod = 0.1f * eyesDistance;

            Vector3 newPos = new Vector3(
                (float)(camPosition.x),
                (float)(camPosition.y),
                (float)(camPosition.z + depthMod));
            cam.transform.localPosition = newPos;

            //camera 'look at' origo
            //cam.transform.LookAt(Vector3.forward);

            //tilt cam according to eye angle
            Point2D gp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(userPos);
            double angle = (gp.X / Screen.width);
            Debug.Log("Current angle : " + angle);

            if (angle < 0.1f)
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, GetComponentInParent<CarController>().transform.eulerAngles.y - 30f , cam.transform.eulerAngles.z);
            else if(angle > 0.9f)
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, GetComponentInParent<CarController>().transform.eulerAngles.y + 10f, cam.transform.eulerAngles.z);
            else if(angle > 0.3f && angle < 0.8f)
                cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, GetComponentInParent<CarController>().transform.eulerAngles.y , cam.transform.eulerAngles.z);



            screenPoint = new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f);

            //handle collision detection
            currentGaze = checkGazeCollision(screenPoint);
        }

	}

    private string checkGazeCollision(Vector3 screenPoint)
    {
        string gazingObjectName = "Forward";
        Ray collisionRay = cam.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(collisionRay, out hit, 150f, 1 << 0))
        {
            if (null != hit.collider)
            {
                gazingObjectName = hit.collider.tag;
            }
        }
        return gazingObjectName;
    }


    void OnApplicationQuit()
    {
        GazeManager.Instance.RemoveGazeListener(this);
    }
}
