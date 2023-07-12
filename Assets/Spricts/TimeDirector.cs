using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimebarDirector : MonoBehaviour
{
    GameObject timebar;

    void Start()
    {
        Application.targetFrameRate = 60;

        timebar = GameObject.Find("timebar");
    }

    public void DecreaseTime()
    {
        timebar.GetComponent<Image>().fillAmount -= 0.000279f;
    }
}