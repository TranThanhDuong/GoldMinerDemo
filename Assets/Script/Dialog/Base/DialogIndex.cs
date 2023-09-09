using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum DialogIndex
{
    DialogPause=1,
    DialogYesNo,
    DialogResult,
    DialogText,
    DialogSetting,
    DialogHighestScore,
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndexs = {
        DialogIndex.DialogPause,
        DialogIndex.DialogYesNo,
        DialogIndex.DialogResult,
        DialogIndex.DialogText,
        DialogIndex.DialogSetting,
        DialogIndex.DialogHighestScore,
    };
}
public class DialogParam
{

}

public class DialogTextParam:DialogParam
{
    public string text;
}

public class DialogYesNoParam : DialogParam
{
    public string text;
    public Action<bool> action;
}

public class DialogResultParam : DialogParam
{
    public int id;
    public bool isVictory;
    public bool buffStrength;
    public bool buffTime;
    public int score;
    public int startScore;
    public int curBoom;
    public int startBoom;
}