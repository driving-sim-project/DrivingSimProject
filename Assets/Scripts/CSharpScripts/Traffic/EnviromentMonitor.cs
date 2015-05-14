using UnityEngine;
using System.Collections;

public class EnviromentMonitor : MonoBehaviour {

    public GameObject currentObj = null;
    public Material hitMat;
    public Material unhitMat;

    void OnTriggerEnter(Collider Other)
    {
        if (SceneManager.GoScene == "replay")
        {
            renderer.material = hitMat;
        }
        Debug.Log(Other.gameObject.GetInstanceID());
    }

    void OnTriggerStay(Collider Other)
    {
        currentObj = Other.gameObject;
    }

    void OnTriggerExit(Collider Other)
    {
        if (SceneManager.GoScene == "replay")
        {
            renderer.material = unhitMat;
        }
        currentObj = null;
    }

}
