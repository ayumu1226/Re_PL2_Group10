using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogInSceneManagement : MonoBehaviour
{
    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    public void GoToLogInScene()
    {
        SceneManager.LoadScene("LogInScene");
    }
}
