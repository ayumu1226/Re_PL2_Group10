using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static bool isLoad = false;

    public void Awake()
    {
        if (isLoad)
        {
            Destroy(gameObject);
            return;
        }
        isLoad = true;
        DontDestroyOnLoad(gameObject);
    }

    public void BGMdown()
    {
        GetComponent<AudioSource>().volume = 0;
    }

    public void BGMup()
    {
        GetComponent<AudioSource>().volume = 0.5f;
    }
}