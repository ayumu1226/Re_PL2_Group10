using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultToTitle : MonoBehaviour
{
    float t = 0;
    Button btn;

    private void Start()
    {
        t = 0;
        btn = GetComponent<Button>();
        btn.interactable = false;
    }
    private void Update()
    {
        t = TimeCounter(t);
        if (t > 4.5)
        {
            btn.interactable = true;
        }
    }
    public static float TimeCounter(float b)
    {
        float a = b;
        a += Time.deltaTime;
        return a;
    }

    public void Resultbutton2()
    {
        GameObject gameObject = GameObject.Find("BGM");
        gameObject.GetComponent<BGM>().BGMup();

        SceneManager.LoadScene("ChooseModeScene");
    }
}
