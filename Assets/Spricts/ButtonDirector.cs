using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonDirector : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void button1() // �ʏ탂�[�h
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
    public void button2() // �T�d���[�h
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
    public void button3() // ���K���[�h
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }

    public void button4() // easy
    {
        SceneManager.LoadScene("GameScene");
    }
    public void button5() // normal
    {
        SceneManager.LoadScene("GameScene");
    }
    public void button6() // hard
    {
        SceneManager.LoadScene("GameScene");
    }
}
