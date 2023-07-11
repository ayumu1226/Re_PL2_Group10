using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Collections;
using NCMB;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class LogIn : MonoBehaviour
{
    public Text error;

    public InputField UserName;
    public InputField PassWord;
    public InputField PassWord2;



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

        String Password= HashPassword(PassWord.text);


        NCMBUser.LogInAsync(UserName.text, Password, (NCMBException e) => {
            if (e != null)
            {
                error.text = "ログインに失敗: " + e.ErrorMessage;
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
                SceneManager.LoadScene("ChooseModeScene");
            }
        });
    }

    public void SignUpButton()
    {
        NCMBUser user = new NCMBUser();
        user.UserName = UserName.text;
        user.Password = HashPassword(PassWord.text); ;

        if (!IsValidPassword(PassWord.text))
        {
            UnityEngine.Debug.Log("パスワードは大文字、小文字、数字を含める必要があります");
            error.text = "新規登録に失敗:パスワードは大文字、小文字、数字を含める必要があります ";
            return;
        }
       
        if (PassWord.text!=PassWord2.text)
        {
            UnityEngine.Debug.Log("上下の入力が一致していません");
            error.text = "新規登録に失敗:上下の入力が一致していません";
            return;
        }



        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                error.text = "新規登録に失敗: " + e.ErrorMessage;
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

    private string HashPassword(string password)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha1.ComputeHash(passwordBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }


    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }


}
