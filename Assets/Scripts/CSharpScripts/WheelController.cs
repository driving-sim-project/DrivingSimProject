using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {

    public GameObject car;

	// Update is called once per frame
	void Update () {
        //transform.RotateAroundLocal(Vector3.forward, );
        transform.localEulerAngles = new Vector3(car.transform.rotation.x, car.transform.rotation.y, car.transform.rotation.z + (Input.GetAxis("Horizontal") * -450f));
	}
}
