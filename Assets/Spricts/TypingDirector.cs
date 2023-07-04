using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{

    [SerializeField] float time;
    [SerializeField] Text tText;
    // 画面テキスト
    [SerializeField] Text fText;
    [SerializeField] Text qText;
    [SerializeField] Text aText;

    // テキストデータを読み込む
    [SerializeField] TextAsset _furigana;
    [SerializeField] TextAsset _question;
    //[SerializeField] TextAsset _answer;

    // テキストデータを格納するリスト
    private List<string> _fList = new List<string>();
    private List<string> _qList = new List<string>();
    //private List<string> _aList = new List<string>();

    // 画面表示する問題文
    private string _fString;
    private string _qString;
    private string _aString;

    // 何番目の問題か
    private int _qNum;

    // 問題の何文字目か
    private int _aNum;

    //正誤判断
    bool isCorrect = false;

    private Dictionary dictionary;

    private List<string> _romSliceList = new List<string>();

    // 何文字目の入力か書くリスト :
    // _furiCountList[i] = 0 → 1文字目の入力
    private List<int> _furiCountList = new List<int>();

    // 入力する文字が、いくつのアルファベットを持つか
    // _romNumList[2] = 0 → 2文字目は1回の入力のみ
    private List<int> _romNumList = new List<int>();

    private void Start()
    {
        Application.targetFrameRate = 60;

        dictionary = GetComponent<Dictionary>();

        // テキストデータをリストに入れる
        SetList();

        // 問題を更新
        NewQuestion();
    }



    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Check();
        }

        TimeCount();

    }

    void SetList()
    {
        string[] _fArray = _furigana.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _fList.AddRange(_fArray);

        string[] _qArray = _question.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _qList.AddRange(_qArray);

        //string[] _aArray = _answer.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        //_aList.AddRange(_aArray);
    }

    private void NewQuestion()
    {
        // 0番目に戻す
        _aNum = 0;

        // ランダムな数字を生成
        _qNum = Random.Range(0, _qList.Count);

        //_qNum = 1;

        _fString = _fList[_qNum];
        _qString = _qList[_qNum];

        CreateRomSliceList(_fString);

        _aString = string.Join("", _romSliceList);

        fText.text = _fString;
        qText.text = _qString;
        aText.text = _aString;
    }

    void CreateRomSliceList(string moji)
    {
        _romSliceList.Clear();
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < moji.Length; i++)
        {
            string a = dictionary.dic[moji[i].ToString()][0];

            if (moji[i].ToString() == "っ" && i + 1 < moji.Length)
            {
                a = dictionary.dic[moji[i+1].ToString()][0].ToString();
            }

            _romSliceList.Add(a);

            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        Debug.Log(string.Join(",", _romSliceList));
    }

    void ReCreateList(List<string> romList)
    {
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < romList.Count; i++)
        {
            string a = romList[i];

            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        //Debug.Log(string.Join(",", _romSliceList));
    }

    // 入力文字が正解の場合
    private void Correct()
    {
        Debug.Log((_aNum + 1) + "文字目:正解");

        // 次の文字を出力
        _aNum++;

        // 正解した文字を灰色で表示
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
                     + _aString.Substring(_aNum);
    }

    // 入力文字が不正解の場合
    private void Incorrect()
    {
        Debug.Log((_aNum + 1) + "文字目:不正解");

        // 間違えた文字を赤く表示
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
                     + "<color=#FF0000>" + _aString.Substring(_aNum, 1) + "</color>"
                     + _aString.Substring(_aNum + 1);
    }

    // 入力された文字の正誤判定
    private void Check()
    {
        // 入力中の文字が何番目か
        int furiCount = _furiCountList[_aNum];

        isCorrect = false;

        if (Input.GetKeyDown(_aString[_aNum].ToString()))
        {
            isCorrect = true;

            // 正解
            Correct();

            if (_aNum >= _aString.Length)
            {
                NewQuestion();
            }
        }
        else if (Input.GetKeyDown("n") && furiCount > 0 && _romSliceList[furiCount - 1] == "n")
        {
            _romSliceList[furiCount - 1] = "nn";
            _aString = string.Join("", _romSliceList);

            ReCreateList(_romSliceList);

            isCorrect = true;

            // 正解
            Correct();

            if (_aNum >= _aString.Length)
            {
                NewQuestion();
            }
        }
        else
        {
            string currentFuri = _fString[furiCount].ToString();

            List<string> stringList = dictionary.dic[currentFuri];

            Debug.Log(string.Join(",", stringList));

            for (int i = 0; i < stringList.Count; i++)
            {
                string rom = stringList[i];
                int romNum = _romNumList[_aNum];

                if (Input.GetKeyDown(rom[romNum].ToString()))
                {
                    _romSliceList[furiCount] = rom;
                    _aString = string.Join("", _romSliceList);

                    ReCreateList(_romSliceList);

                    isCorrect = true;

                    // 正解
                    Correct();

                    if (_aNum >= _aString.Length)
                    {
                        NewQuestion();
                    }
                    break;
                }
            }
        }
        if (!isCorrect)
        {
            // 不正解
            Incorrect();
        }
    }

    void TimeCount()
    {
        time -= Time.deltaTime;
        tText.text = time.ToString("f1");

        GameObject timebar = GameObject.Find("TimebarDirector");
        timebar.GetComponent<TimebarDirector>().DecreaseTime();

        if (time <= 0)
        {
            tText.text = "終了";
            SceneManager.LoadScene("ResultScene");
        }
    }
}
