using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSetting : BaseDialog
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

    public void OnClose()
    {
        DialogManager.Instance.HideDialog(index);
    }

    public void OnInfo()
    {

    }
    public void OnHelp()
    {

    }

    public void OnLanguage()
    {

    }
}
