using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogPause : BaseDialog
{
    [SerializeField]
    Sprite normalMusic;
    [SerializeField]
    Sprite muteMusic;
    [SerializeField]
    Image musicBtn;
    [SerializeField]
    Sprite normalSound;
    [SerializeField]
    Sprite muteSound;
    [SerializeField]
    Image soundBtn;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        MissionControl.Instance.SetPause(true);
        if (DataAPIControler.Instance.GetMusic() == 1)
        {
            musicBtn.sprite = normalMusic;
        }
        else
        {
            musicBtn.sprite = muteMusic;
        }

        if (DataAPIControler.Instance.GetSound() == 1)
        {
            soundBtn.sprite = normalSound;
        }
        else
        {
            soundBtn.sprite = muteSound;
        }
    }

    public void OnMusic()
    {
        if (DataAPIControler.Instance.SetMusic() == 1)
        {
            musicBtn.sprite = normalMusic;
        }
        else
        {
            musicBtn.sprite = muteMusic;
        }
    }

    public void OnSound()
    {
        if (DataAPIControler.Instance.SetSound() == 1)
        {
            soundBtn.sprite = normalSound;
        }
        else
        {
            soundBtn.sprite = muteSound;
        }
    }

    public void OnRestart()
    {
        DialogYesNoParam param = new DialogYesNoParam();
        param.text = "This will reset your level back to 1. Do you really want to?";
        param.action = (isYes) =>
        {
            if (isYes)
            {
                DataAPIControler.Instance.ChangeMissionData(0, 0, 0, (b) => { });
                ConfigMissionRecord rm = ConfigManager.Instance.configMission.GetRecordByKeySearch(1);
                LoadSceneManager.Instance.LoadSceneByIndex(rm.sceneID, () => {
                    MissionControl.Instance.Setup(1, 0, 0);
                    DialogManager.Instance.HideDialog(index);
                });
            }
        };

        DialogManager.Instance.ShowDialog(DialogIndex.DialogYesNo, param);
    }

    public void OnMainMenu()
    {
        LoadSceneManager.Instance.LoadSceneByIndex(1, () => { ViewManager.Instance.OnSwitchView(ViewIndex.HomeView); });
        DialogManager.Instance.HideDialog(index);
    }

    public void OnBackGame()
    {
        MissionControl.Instance.SetPause(false);
        DialogManager.Instance.HideDialog(index);
    }

    public override void OnHidewDialog()
    {
        base.OnHidewDialog();
        MissionControl.Instance.SetPause(false);
    }
}
