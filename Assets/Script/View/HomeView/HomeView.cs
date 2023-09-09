using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void OnSetup(ViewParam param)
    {
        base.OnSetup(param);
        SoundManager.Instance.PlayMusic(SoundType.MUSIC_MENU);
    }

    public void OnStartGame()
    {
        //ViewManager.Instance.OnSwitchView(ViewIndex.ShopView);
        MissionData data = DataAPIControler.Instance.GetMissionData();
        ConfigMissionRecord missionRecord = ConfigManager.Instance.configMission.GetRecordByKeySearch(data.id + 1);
        if(missionRecord == null)
        {
            DialogManager.Instance.ShowDialog(DialogIndex.DialogText, new DialogTextParam { text = "New level is on developed, please wait!" });
            return;
        }
        LoadSceneManager.Instance.LoadSceneByIndex(missionRecord.sceneID, ()=> {

            MissionControl.Instance.Setup(missionRecord.id, data.curBoom, data.curScore, false, false);
            ViewManager.Instance.OnSwitchView(ViewIndex.EmptyView);
        });
    }

    public void OnRestart()
    {
        DialogYesNoParam param = new DialogYesNoParam();
        param.text = "This will reset your level back to 1. Do you really want to?";
        param.action = (isYes) =>
        {
            if(isYes)
            {
                DataAPIControler.Instance.ChangeMissionData(0, 0, 0, (b) => { });
            }    
        };

        DialogManager.Instance.ShowDialog(DialogIndex.DialogYesNo,param);
    }    

    public void OnHighestScore()
    {
        DialogTextParam param = new DialogTextParam();
        param.text = "<sprite name=\"goldheart\">" + DataAPIControler.Instance.GetHighestScore().ToNumberSeparateByComma();
        DialogManager.Instance.ShowDialog(DialogIndex.DialogHighestScore, param);
    }    

    public void OnLeaveGame()
    {
        Application.Quit();
    }
    public void OnSetting()
    {
        DialogManager.Instance.ShowDialog(DialogIndex.DialogSetting);
    }
}
