using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionControl : Singleton<MissionControl>
{
    [SerializeField]
    int missionID;
    [SerializeField]
    int timer;
    [SerializeField]
    MissionUi missionUi;
    [SerializeField]
    bool isPause;

    public event Action<int> OnTimeChange;
    public event Action<int,int> OnScoreChange;

    bool buffStrength;
    bool buffTime;
    int startScore;
    int totalScore;
    int missionScore;
    int curBoom;
    int startBoom;

    public bool IsPause => isPause;
    public int Timer => timer;
    public int TotalScore => totalScore;
    public int MissionLevel => missionID;
    public int MissionScore => missionScore;

    //public void Start()
    //{//Test call
    //    ConfigManager.Instance.InitConfig(() =>
    //    {
    //        Setup(missionID, 10, 2000, true, true);
    //    });
    //}

    public void SetPause(bool isPause)
    {
        this.isPause = isPause;
    }

    public void Setup(int id, int score, int boom, bool buffStrength = false, bool buffTime = false)
    {
        SoundManager.Instance.PlayMusic(SoundType.MUSIC_PLAY);
        isPause = false;
        missionID = id;
        startBoom = curBoom = boom;
        this.buffStrength = buffStrength;
        this.buffTime = buffTime;
        timer = buffTime ? 75 : 60;
        startScore = totalScore = score;
        ConfigMissionRecord missionRecord = ConfigManager.Instance.configMission.GetRecordByKeySearch(missionID);
        ConfigDataObjectsRecord configDataObjects = ConfigManager.Instance.configDataObjects.GetRecordByKeySearch(missionRecord.dataObjectsID);

        missionScore = DataAPIControler.Instance.GetMissionData().curScore + (int)(configDataObjects.totalScore * 0.8f);
        CreateListItem(configDataObjects);
        StartCoroutine(TimerCount());
    }    

    void CreateListItem(ConfigDataObjectsRecord data)
    {
        List<DataObjectScene> dataObjects = data.lsObjects;

        foreach(DataObjectScene dataObject in dataObjects)
        {
            ConfigObjectRecord obj = ConfigManager.Instance.configObject.GetRecordByKeySearch(dataObject.id);
            if(obj != null)
            {
                GameObject gObj = (GameObject)Instantiate(Resources.Load("Item/" + obj.prefab), dataObject.pos, Quaternion.identity);
            }    
        }    
    }    

    IEnumerator TimerCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (timer > 0)
            {
                TimeChange(-1);
            }
            else
            {
                SumaryMission();
                break;
            }    
        }
    }    

    void SumaryMission()
    {
        isPause = true;
        DialogResultParam param = new DialogResultParam();
        if (totalScore >= missionScore)
        {
            //Win
            param.isVictory = true;
            SoundManager.Instance.PlayMusic(SoundType.MUSIC_WIN);
        }   
        else
        {
            //Lose
            param.isVictory = false;
            SoundManager.Instance.PlayMusic(SoundType.MUSIC_LOSE);
        }
        param.id = missionID;
        param.score = totalScore;
        param.curBoom = curBoom;
        param.startBoom = startBoom;
        param.startScore = startScore;
        param.buffTime = buffTime;
        param.buffStrength = buffStrength;
        DialogManager.Instance.ShowDialog(DialogIndex.DialogResult, param);
    }

    void TimeChange(int time)
    {
        timer += time;
        OnTimeChange?.Invoke(timer);
    }    

    public void AddScore(int score)
    {
        totalScore += score;
        OnScoreChange?.Invoke(totalScore, score);
    }    
}
