using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultToGame : MonoBehaviour
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
        TimeCounter();
        if(t > 4.5)
        {
            btn.interactable = true;
        }
    }
    public float TimeCounter()
    {
        t += Time.deltaTime;
        return t;
    }

    public void Resultbutton1()
    {
        SceneManager.LoadScene("GameScene");
    }
}
