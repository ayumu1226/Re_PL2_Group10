using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public static int flag=0;

    public void Mode1Button()
    {
        flag = 1;
        SceneManager.LoadScene("LeaderBoard");
    }

    public void Mode2Button()
    {
        flag = 2;
        SceneManager.LoadScene("LeaderBoard");
    }

    public static int GetFlag()
    {
        return flag;
    }
}
