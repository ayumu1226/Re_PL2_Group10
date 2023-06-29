using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimebarDirector : MonoBehaviour
{
    GameObject timebar;

    void Start()
    {
        timebar = GameObject.Find("timebar");
    }

    public void DecreaseTime()
    {
        timebar.GetComponent<Image>().fillAmount -= 0.00027777f;
    }
}