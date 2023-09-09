using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogYesNo : BaseDialog
{
    [SerializeField]
    TextMeshProUGUI textLB;

    Action<bool> action;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        DialogYesNoParam p = (DialogYesNoParam)param;
        textLB.text = p.text;
        action = p.action;
    }

    public void OnNoBtn()
    {
        action?.Invoke(false);
        DialogManager.Instance.HideDialog(index);
    }

    public void OnYesBtn()
    {
        action?.Invoke(true);
        DialogManager.Instance.HideDialog(index);
    }

    public override void OnHidewDialog()
    {
        base.OnHidewDialog();
        action = null;
        textLB.text = "";
    }
}
