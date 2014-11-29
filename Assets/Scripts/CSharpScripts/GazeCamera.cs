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

    public Collider currentHit;

    private GazeDataValidator gazeUtils;
    private Camera cam;

    private double eyesDistance;
    private double baseDist;
    private double depthMod;

    void Start()
    {
        gazeUtils = new GazeDataValidator(30);
        GazeManager.Instance.AddGazeListener(this);
        cam = GetComponent<Camera>();
        if(!GazeManager.Instance.IsActivated)
            GazeManager.Instance.Activate(GazeManager.ApiVersion.VERSION_1_0, GazeManager.ClientMode.Push);
    }

    public void OnGazeUpdate(GazeData gazeData)
    {
        gazeUtils.Update(gazeData);
    }

	// Update is called once per frame
	void Update () {
        Point2D userPos = gazeUtils.GetLastValidUserPosition();

        if (null != userPos)
        {
            //mapping cam panning to 3:2 aspect ratio
            double tx = (userPos.X * 5) - 2.5f;
            double ty = (userPos.Y * 3) - 1.5f;

            //position camera X-Y plane and adjust distance
            eyesDistance = gazeUtils.GetLastValidUserDistance();
            depthMod = 2 * eyesDistance;

            //Vector3 newPos = new Vector3(
            //    (float)tx,
            //    (float)ty,
            //    (float)(baseDist + depthMod));
            //cam.transform.position = newPos;

            //camera 'look at' origo
            //cam.transform.LookAt(GetComponentInParent<CarController>().transform);

            //tilt cam according to eye angle
            //double angle = gazeUtils.GetLastValidEyesAngle();
            //cam.transform.localEulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z + (float)angle);
        }

        Point2D gazeCoords = gazeUtils.GetLastValidSmoothedGazeCoordinates();

        if (null != gazeCoords)
        {
            //map gaze indicator
            Point2D gp = UnityGazeUtils.getGazeCoordsToUnityWindowCoords(gazeCoords);

            Vector3 screenPoint = new Vector3((float)gp.X, (float)gp.Y, cam.nearClipPlane + .1f);

            Vector3 planeCoord = cam.ScreenToWorldPoint(screenPoint);

            //handle collision detection
            Debug.Log(checkGazeCollision(screenPoint));
        }

	}

    private string checkGazeCollision(Vector3 screenPoint)
    {
        string gazingObjectName = "Forward";
        Ray collisionRay = cam.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(collisionRay, out hit, 150f, 1 << 0))
        {
            if (null != hit.collider && currentHit != hit.collider)
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
