using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Audio;

public class Typing : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioClip AttackSE;
    public AudioClip breakSE;
    public AudioClip missSE;
    public AudioClip endSE;

    public static int inputNum = 0;
    public static int point = 0;
    public static int miss = 0;
    public static int sum = 0;

    // startとendロゴ
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

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
        audioSource = GetComponent<AudioSource>();

        sum = 0;
        inputNum = 0;
        point = 0;
        miss = 0;

        Application.targetFrameRate = 60;

        dictionary = GetComponent<Dictionary>();

        // テキストデータをリストに入れる
        SetList();

        // 問題を更新
        NewQuestion();
    }



    private void Update()
    {
        if (time >= 0 && Input.anyKeyDown)
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
        sum++;
        Debug.Log(sum);

        if(sum > 1)
        {
            PlaySE(breakSE);
        }

        // ランダムな数字を生成
        _qNum = Random.Range(0, _qList.Count);

        //_qNum = 1;

        _fString = _fList[_qNum];
        _qString = _qList[_qNum];

        CreateRomSliceList(_fString);

        _aString = string.Join("", _GetRomSliceListWithoutSkip());

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

            if (moji[i].ToString() == "ゃ" || moji[i].ToString() == "ゅ" || moji[i].ToString() == "ょ" || moji[i].ToString() == "ぁ" || moji[i].ToString() == "ぃ" || moji[i].ToString() == "ぅ" || moji[i].ToString() == "ぇ" || moji[i].ToString() == "ぉ")
            {
                a = "SKIP";
            }
            else if (moji[i].ToString() == "っ" && i + 1 < moji.Length)
            {
                a = dictionary.dic[moji[i + 1].ToString()][0].Substring(0, 1);
            }
            else if (i + 1 < moji.Length)
            {
                //大文字+小文字の判定
                string addNextMoji = moji[i].ToString() + moji[i + 1].ToString();
                if (dictionary.dic.ContainsKey(addNextMoji))
                {
                    a = dictionary.dic[addNextMoji][0]; ;
                }
            }
            _romSliceList.Add(a);

            if( a!="SKIP")
            {
                for (int j = 0; j < a.Length; j++)
                {
                    _furiCountList.Add(i);
                    _romNumList.Add(j);
                }
            }
            
        }
        //Debug.Log(string.Join(",", _romSliceList));
    }

    //小文字の挿入
    void AddSmallMoji()
    {
        int nextMojiNum = _furiCountList[_aNum] + 1;

        if (_fString.Length - 1 < nextMojiNum)
        {
            return;
        }

        string nextMoji = _fString[nextMojiNum].ToString();
        string a = dictionary.dic[nextMoji][0];

        if (a[0] != 'x' && a[0] != 'l')
        {
            return;
        }

        _romSliceList.Insert(nextMojiNum, a);
        _romSliceList.RemoveAt(nextMojiNum + 1);

        ReCreateList(_romSliceList);
        _aString = string.Join("", _GetRomSliceListWithoutSkip());
    }

    void ReCreateList(List<string> romList)
    {
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < romList.Count; i++)
        {
            string a = romList[i];
            if (a == "SKIP")
            {
                continue;
            }

            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        //Debug.Log(string.Join(",", _romSliceList));
    }

    List<string> _GetRomSliceListWithoutSkip()
    {
        List<string> returnList = new List<string>();
        foreach (string rom in _romSliceList)
        {
            if (rom == "SKIP")
            {
                continue;
            }
            returnList.Add(rom);
        }
        return returnList;
    }

    // 入力文字が正解の場合
    private void Correct()
    {
        point++;
        PlaySE(AttackSE);

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
        miss++;
        PlaySE(missSE);

        Debug.Log((_aNum + 1) + "文字目:不正解");
        
        // 間違えた文字を赤く表示
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
                     + "<color=#FF0000>" + _aString.Substring(_aNum, 1) + "</color>"
                     + _aString.Substring(_aNum + 1);
    }

    // 入力された文字の正誤判定
    private void Check()
    {
        inputNum++;

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
            _aString = string.Join("", _GetRomSliceListWithoutSkip());

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

            if (furiCount < _fString.Length - 1)
            {
                string addNextMoji = _fString[furiCount].ToString() + _fString[furiCount + 1].ToString();
                if (dictionary.dic.ContainsKey(addNextMoji))
                {
                    Check2(addNextMoji, furiCount, false);
                }

            }

            if (!isCorrect)
            {
                string moji = _fString[furiCount].ToString();
                Check2(moji, furiCount, true);
            }

        }

        if (!isCorrect)
        {
            // 不正解
            Incorrect();
        }
    }

    void Check2(string currentFuri, int furiCount, bool addSmallMoji)
    {
        List<string> stringList = dictionary.dic[currentFuri];

        Debug.Log(string.Join(",", stringList));

        for (int i = 0; i < stringList.Count; i++)
        {
            string rom = stringList[i];
            int romNum = _romNumList[_aNum];

            bool preCheck = true;

            for (int j = 0; j < romNum; j++)
            {
                if (rom[j] != _romSliceList[furiCount][j])
                {
                    preCheck = false;
                }
            }

            if (preCheck && Input.GetKeyDown(rom[romNum].ToString()))
            {
                _romSliceList[furiCount] = rom;
                _aString = string.Join("", _GetRomSliceListWithoutSkip());

                ReCreateList(_romSliceList);

                isCorrect = true;

                if (addSmallMoji)
                {
                    AddSmallMoji();
                }

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

    void TimeCount()
    {
        time -= Time.deltaTime;
        tText.text = time.ToString("f1");

        GameObject timebar = GameObject.Find("TimebarDirector");
        timebar.GetComponent<TimebarDirector>().DecreaseTime();

        if (-0.017 < time && time <= 0)
        {
            PlaySE(endSE);

            end.SetActive(true);
        }
        if(time <= -3)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }

    public static int GetPoint()
    {
        return point;
    }

    public static int GetMiss()
    {
        return miss;
    }

    public static int GetInputNum()
    {
        return inputNum;
    }

    public static int GetSum()
    {
        return sum;
    }

    public void PlaySE(AudioClip clip)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("効果音なし");
        }
    }
}
