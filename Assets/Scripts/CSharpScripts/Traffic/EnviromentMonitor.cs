using UnityEngine;
using System.Collections;

public class EnviromentMonitor : MonoBehaviour {

    public Collider currentObj;
    public Material hitMat;
    public Material unhitMat;

    void OnTriggerEnter(Collider Other)
    {
        renderer.material = hitMat;
    }

    void OnTriggerStay(Collider Other)
    {
        currentObj = Other;
        //Debug.Log(name +" "+ Other.tag);
        //foreach (ContactPoint contact in collisionInfo.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        //}
    }

    void OnTriggerExit(Collider Other)
    {
        renderer.material = unhitMat;
    }

}
