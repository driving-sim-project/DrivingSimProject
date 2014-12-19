using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    public GameObject[] camerasList;
    public string[] shortcuts;
    public bool changeAudioListener = true;
    void Update()
    {
        int i = 0;
        for (i = 0; i < camerasList.Length; i++)
        {
            if (Input.GetKeyUp(shortcuts[i]))
                SwitchCamera(i);
        }
    }

    void SwitchCamera(int index)
    {
        int i = 0;
        for (i = 0; i < camerasList.Length; i++)
        {
            if (i != index)
            {
                if (changeAudioListener)
                {
                    camerasList[i].GetComponent<AudioListener>().enabled = false;
                }
                camerasList[i].camera.enabled = false;
            }
            else
            {
                if (changeAudioListener)
                {
                    camerasList[i].GetComponent<AudioListener>().enabled = true;
                }
                camerasList[i].camera.enabled = true;
            }
        }

    }
}