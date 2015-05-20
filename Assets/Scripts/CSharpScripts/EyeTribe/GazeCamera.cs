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

    public Transform currentGazeObj;
    public string currentGaze;
    public Vector3 screenPoint;
    public float sensitivity = 1f;
    public float gazeSpeed = 1f;

    private GazeDataValidator gazeUtils;
    private Camera cam;
    private Component gazeIndicator;
    //private Vector3 camPosition;
    //private double eyesDistance;
    private double baseDist;
    //private double depthMod;
    private GazeData gazeDataTmp;



    void Start()
    {
        if (SceneManager.GoScene == "replay")
            Destroy(this);
        cam = GetComponent<Camera>();
        //camPosition = cam.transform.localPosition;
        gazeDataTmp = null;
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
        //eyesDistance = gazeUtils.GetLastValidUserDistance();
        if (null == userPos)
        {
            userPos = new Point2D(Input.mousePosition.x, Input.mousePosition.y);
            //eyesDistance = 1;
        }
            

        //mapping cam panning to 3:2 aspect ratio
        //double tx = (userPos.X * 5) - 2.5f;
        //double ty = (userPos.Y * 3) - 1.5f;

        //position camera X-Y plane and adjust distance
        
        //depthMod = 0.1f * eyesDistance;

        //Vector3 newPos = new Vector3(
        //    (float)(camPosition.x),
        //    (float)(camPosition.y),
        //    (float)(camPosition.z + depthMod));
        //cam.transform.localPosition = newPos;

        //camera 'look at' origo


        //tilt cam according to eye angle
        //Point2D gp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(userPos);
        //Debug.Log(userPos.X);
        if (userPos.X > 0f && userPos.X < Screen.width && userPos.Y > 0f && userPos.Y < Screen.height)
        {
            if (Vector3.Distance(screenPoint, new Vector3((float)userPos.X, (float)userPos.Y, cam.nearClipPlane + .1f)) > sensitivity)
            {
                screenPoint = new Vector3((float)userPos.X, (float)userPos.Y, cam.nearClipPlane + .1f);
            }
            
            if (screenPoint.x < Screen.width * 0.2f)
                cam.transform.localRotation = Quaternion.Euler(0f, -30f, 0f);
            else if (screenPoint.x > Screen.width * 0.8f)
                cam.transform.localRotation = Quaternion.Euler(0f, 15f, 0f);
            else
                cam.transform.localRotation = Quaternion.identity;
            //handle collision detection
            checkGazeCollision(screenPoint);
        }
        else
        {
            currentGaze = "Out of Screen";
            currentGazeObj = null;
        }

	}

    private void checkGazeCollision(Vector3 screenPoint)
    {
        string gazingObjectName = "";
        Ray collisionRay = cam.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.SphereCast(collisionRay, 0.02f, out hit, 150f, 1 << 0))
        {
            if (null != hit.collider)
            {
                gazingObjectName = hit.collider.tag;
                currentGazeObj = hit.transform;
            }
        }
        else
        {
            gazingObjectName = "Scene";
            currentGazeObj = null;
        }
        currentGaze = gazingObjectName;
    }


    void OnApplicationQuit()
    {
        GazeManager.Instance.RemoveGazeListener(this);
    }
}
