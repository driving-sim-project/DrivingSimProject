using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour {

    public GameObject[] redLightObject;
    public GameObject[] yellowLightObject;
    public GameObject[] greenLightObject;

    public Collider stopLine;

    public int greenLightInterval = 0;

    public bool isRed = false;

    void setRedLight()
    {
        isRed = true;
        foreach (GameObject g in redLightObject)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in yellowLightObject)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in greenLightObject)
        {
            g.SetActive(false);
        }
    }

    void setYellowLight()
    {
        isRed = false;
        foreach (GameObject g in redLightObject)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in yellowLightObject)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in greenLightObject)
        {
            g.SetActive(false);
        }
    }

    void setGreenLight()
    {
        foreach (GameObject g in redLightObject)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in yellowLightObject)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in greenLightObject)
        {
            g.SetActive(true);
        }
    }

    public IEnumerator RedLight()
    {
        setYellowLight();
        yield return new WaitForSeconds(3);
        setRedLight();
    }

    public IEnumerator GreenLight()
    {
        yield return new WaitForSeconds(3);
        setGreenLight();
    }

}
