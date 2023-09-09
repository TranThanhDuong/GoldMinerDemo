using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogResult : BaseDialog
{
    [SerializeField]
    Sprite victorySpr;
    [SerializeField]
    Sprite loseSpr;
    [SerializeField]
    Image titleImg;
    [SerializeField]
    GameObject btnNextLevel;
    [SerializeField]
    TextMeshProUGUI scoreLB;

    DialogResultParam resultParam;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        resultParam = (DialogResultParam)param;
        if(resultParam.isVictory)
        {
            titleImg.sprite = victorySpr;
            btnNextLevel.SetActive(true);
        }
        else
        {
            titleImg.sprite = loseSpr;
            btnNextLevel.SetActive(false);
        }
        scoreLB.text = "<sprite name=\"goldheart\">" + resultParam.score.ToNumberSeparateByComma();
    }

    public void OnNextLevel()
    {
        ConfigMissionRecord rm = ConfigManager.Instance.configMission.GetRecordByKeySearch(resultParam.id + 1);
        DataAPIControler.Instance.ChangeMissionData(resultParam.id, resultParam.score, resultParam.curBoom + 2, (data) => { });
        if (rm != null)
        {
            LoadSceneManager.Instance.LoadSceneByIndex(rm.sceneID, () =>
            {
                MissionControl.Instance.Setup(rm.id, resultParam.curBoom, resultParam.score, false, false);
            });
        }
        else
        {
            DialogManager.Instance.ShowDialog(DialogIndex.DialogText, new DialogTextParam { text = "New level is on developed, please wait!" });
        }
        DialogManager.Instance.HideDialog(index);
    }

    public void OnRetry()
    {
        ConfigMissionRecord rm = ConfigManager.Instance.configMission.GetRecordByKeySearch(resultParam.id);
        LoadSceneManager.Instance.LoadSceneByIndex(rm.sceneID, () => {
            MissionControl.Instance.Setup(resultParam.id, resultParam.startScore, resultParam.startBoom, resultParam.buffStrength, resultParam.buffTime);
            ViewManager.Instance.OnSwitchView(ViewIndex.EmptyView);
            DialogManager.Instance.HideDialog(index); 
        });
    }

    public void OnMainMenu()
    {
        LoadSceneManager.Instance.LoadSceneByIndex(1, () => { ViewManager.Instance.OnSwitchView(ViewIndex.HomeView); DialogManager.Instance.HideDialog(index); });
    }
}
