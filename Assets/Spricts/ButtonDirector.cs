using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonDirector : MonoBehaviour
{
    public static int mode;
    public static int level;

    private void Start()
    {
        mode = 0;
        level = 0;
    }

    public void button1() // 通常モード
    {
        mode = 1;
        SceneManager.LoadScene("ChooseLevelScene");
    }
    public void button2() // 慎重モード
    {
        mode = 2;
        SceneManager.LoadScene("ChooseLevelScene");
    }
    public void button3() // 練習モード
    {
        mode = 3;
        SceneManager.LoadScene("ChooseLevelScene");
    }

    public void button4() // easy
    {
        level = 1;
        SceneManager.LoadScene("GameScene");
    }
    public void button5() // normal
    {
        level = 2;
        SceneManager.LoadScene("GameScene");
    }
    public void button6() // hard
    {
        level = 3;
        SceneManager.LoadScene("GameScene");
    }

    public void buttonback()
    {
        SceneManager.LoadScene("ChooseModeScene");
    }

    public void buttontitle()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public static int GetMode()
    {
        return mode;
    }

    public static int GetLevel()
    {
        return level;
    }
}
