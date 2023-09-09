using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataAPIControler : Singleton<DataAPIControler>
{
    [SerializeField]
    private DataBaseLocal model;
    // Start is called before the first frame update
    private MissionData missions;
    public void OnInit(Action callback)
    {
        if (model.LoadData())
        {
            callback?.Invoke();
        }
        else
        {
            PlayerData playerData = new PlayerData();
            playerData.highestScore = 0;
            MissionData mission = new MissionData();
            playerData.missionData = mission;
            model.CreateNewData(playerData);
            callback?.Invoke();
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Music", 1);
        }
        // setting get data 
        missions = model.Read<MissionData>(DataPath.PLAYER_MISSION);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_MISSION, (data) =>
        {
            missions = (MissionData)data;
        });

    }
    public MissionData GetMissionData()
    {
        return model.Read<MissionData>(DataPath.PLAYER_MISSION);
    }
    public int GetHighestScore()
    {
        return model.Read<int>(DataPath.PLAYER_HIGHEST_SCORE);
    }
    public void ChangeHighestScore(int score, Action callBack)
    {
        model.UpdateData(DataPath.PLAYER_HIGHEST_SCORE, score, callBack);
    }
    public void ChangeMissionData(int id, int score, int boom, Action<bool> callBack)
    {
        MissionData mission = GetMissionData();

        if (mission == null)
            mission = new MissionData();

        mission.id = id;
        mission.curScore = score;
        mission.curBoom = boom;

        model.UpdateData(DataPath.PLAYER_MISSION, mission, () =>
        {
            callBack?.Invoke(true);
        });

        int highestScore = GetHighestScore();
        if (score > highestScore)
            ChangeHighestScore(score, () => { });
    }

    public int SetSound()
    {
        int sound = 0;
        if(PlayerPrefs.GetInt("Sound") == 0)
        {
            sound = 1;
        }
        PlayerPrefs.SetInt("Sound", sound);
        sound.TriggerEventData("Sound");
        return sound;
    }
    public int GetSound()
    {
        return PlayerPrefs.GetInt("Sound");
    }

    public int SetMusic()
    {
        int music = 0;
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            music = 1;
        }
        PlayerPrefs.SetInt("Music", music);
        music.TriggerEventData("Music");
        return music;
    }
    public int GetMusic()
    {
        return PlayerPrefs.GetInt("Music");
    }
}