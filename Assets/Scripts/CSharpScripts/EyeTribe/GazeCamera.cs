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
    public float sensitivity = 1f;
    public float gazeSpeed = 1f;
    public GameObject leftMirror;
    public GameObject rightMirror;

    private GazeDataValidator gazeUtils;
    private Camera cam;
    private Component gazeIndicator;
    private Vector3 camPosition;
    private double eyesDistance;
    private double baseDist;
    private double depthMod;
    private GazeData gazeDataTmp;



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
        gazeDataTmp = null;
    }

    public void OnGazeUpdate(GazeData gazeData)
    {
        float lastDistance = 0f;
        if(gazeDataTmp != null){
            lastDistance = Vector2.Distance(
            new Vector2((float)gazeData.SmoothedCoordinates.X, (float)gazeData.SmoothedCoordinates.Y),
            new Vector2((float)gazeDataTmp.SmoothedCoordinates.X, (float)gazeDataTmp.SmoothedCoordinates.Y));
        }
        
        //Debug.Log(lastDistance);

        if(lastDistance > sensitivity){
            gazeUtils.Update(gazeData);
        }

        gazeDataTmp = gazeData;

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

            //Vector3 newPos = new Vector3(
            //    (float)(camPosition.x),
            //    (float)(camPosition.y),
            //    (float)(camPosition.z + depthMod));
            //cam.transform.localPosition = newPos;

            //camera 'look at' origo
            

            //tilt cam according to eye angle
            Point2D gp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(userPos);
            if( gp.X > 0f && gp.X < Screen.width && gp.Y > 0f && gp.Y < Screen.height){
                if (screenPoint == null)
                {
                    screenPoint = new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f);
                }
                else
                {
                    if (Vector3.Distance(screenPoint, new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f)) > sensitivity)
                    {
                        screenPoint = new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f);
                    }
                }
                if (screenPoint.x < Screen.width * 0.2f)
                    cam.transform.LookAt(leftMirror.transform);
                else if (screenPoint.x > Screen.width * 0.8f)
                    cam.transform.LookAt(rightMirror.transform);
                else
                    cam.transform.LookAt(new Vector3(0f, 0f, 0f));
                //handle collision detection
                currentGaze = checkGazeCollision(screenPoint);
            }
        }

	}

    private string checkGazeCollision(Vector3 screenPoint)
    {
        string gazingObjectName = "";
        Ray collisionRay = cam.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.SphereCast(collisionRay, 1f, out hit, 150f, 1 << 0))
        {
            if (null != hit.collider)
            {
                gazingObjectName = hit.collider.tag;
            }
        }
        else
        {
            gazingObjectName = "Forward";
        }
        return gazingObjectName;
    }


    void OnApplicationQuit()
    {
        GazeManager.Instance.RemoveGazeListener(this);
    }
}
