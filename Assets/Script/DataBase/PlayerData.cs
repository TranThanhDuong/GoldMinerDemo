using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData 
{
    [SerializeField]
    public int highestScore;
    [SerializeField]
    public MissionData missionData = new MissionData();
}

[Serializable]
public class MissionData
{
    public int id;
    public int curScore;
    public int curBoom;
}


public static class DataUtilities
{
    public static string ToKey(this object data)
    {
        return "K_" + data.ToString();
    }
}