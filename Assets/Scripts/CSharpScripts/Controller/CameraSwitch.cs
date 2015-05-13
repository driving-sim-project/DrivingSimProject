using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] camerasList;
    public string[] shortcuts;
    public bool changeAudioListener = true;

    void Start()
    {
        if (SceneManager.GoScene != "replay")
        {
            enabled = false;
        }
    }

    void Update()
    {
         for (int i = 0; i < camerasList.Length; i++)
        {
            if (Input.GetKeyUp(shortcuts[i]))
                SwitchCamera(i);
        }
    }

    void SwitchCamera(int index)
    {
        for (int i = 0; i < camerasList.Length; i++)
        {
            if (i != index)
            {
                if (changeAudioListener)
                {
                    camerasList[i].GetComponent<AudioListener>().enabled = false;
                }
                camerasList[i].camera.enabled = false;
                camerasList[i].gameObject.SetActive(false);
            }
            else
            {
                if (changeAudioListener)
                {
                    camerasList[index].GetComponent<AudioListener>().enabled = true;
                }
                camerasList[index].camera.enabled = true;
                camerasList[i].gameObject.SetActive(true);
            }
        }

    }
}