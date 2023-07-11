using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NCMB;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    public InputField UserName;
    public InputField PassWord;
    private string currentPlayerName;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LogInButton()
    {
        NCMBUser user = new NCMBUser();
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
                SceneManager.LoadScene("ChooseLevelScene");
            }
        });
    }

    public void SignUpButton()
    {

        if (!IsValidPassword(PassWord.text))
        {
            UnityEngine.Debug.Log("パスワードは大文字、小文字、数字を含める必要があります");
            return;
        }

        NCMBUser user = new NCMBUser();
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("新規登録に成功");
                NCMBUser currentUser = NCMBUser.CurrentUser;
                if (currentUser != null)
                {
                    UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
                    LogInButton();
                }
                else
                {
                    UnityEngine.Debug.Log("未ログインまたは取得に失敗");
                }
  
            }
        });
    }
    private bool IsValidPassword(string password)
    {
        // 大文字、小文字、数字をそれぞれ含むか確認
        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            else if (char.IsLower(c))
            {
                hasLowerCase = true;
            }
            else if (char.IsDigit(c))
            {
                hasDigit = true;
            }
        }

        return hasUpperCase && hasLowerCase && hasDigit;
    }



    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    public void Userlogout()
    {
        NCMBUser.LogOutAsync((NCMBException e) => {
            if (e == null)
            {
                UnityEngine.Debug.Log("ログアウト成功");
            }
            else
            {
                UnityEngine.Debug.Log("ログアウトに失敗: " + e.ErrorMessage);
            }
        });
    }
}
