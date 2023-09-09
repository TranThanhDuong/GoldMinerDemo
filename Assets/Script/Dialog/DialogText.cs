using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogText : BaseDialog
{
    [SerializeField]
    TextMeshProUGUI text;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        DialogTextParam p = (DialogTextParam)param;
        text.SetText(p.text);
    }

    public void OnOkBtn()
    {
        DialogManager.Instance.HideDialog(index);
    }
    public override void OnHidewDialog()
    {
        text.text = "";
        base.OnHidewDialog();
    }
}
