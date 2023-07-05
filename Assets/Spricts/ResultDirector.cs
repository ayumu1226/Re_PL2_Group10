using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDirector : MonoBehaviour
{
    [SerializeField] Text iText;
    [SerializeField] Text mText;
    [SerializeField] Text pmText;
    [SerializeField] Text sText;

    private string iString;
    private string mString;
    private string perString;
    private string sString;

    private float perNum;
    private float sNum;

    public void Start()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        perNum = (Typing.GetInputNum() - Typing.GetMiss()) * 100 / Typing.GetInputNum();
        //Debug.Log(perNum);
        sNum = Typing.GetInputNum() * 5 - Typing.GetMiss() * 10;

        iString = Typing.GetInputNum().ToString() + "ï∂éö";
        mString = Typing.GetMiss().ToString() + "ï∂éö";
        perString = perNum.ToString() + "Åì";
        sString = sNum.ToString();

        iText.text = iString;
        mText.text = mString;
        pmText.text = perString;
        sText.text = sString;
        
    }



}
