using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogHighestScore : BaseDialog
{
    [SerializeField]
    TextMeshProUGUI textLB;
    public override void OnSetup(DialogParam param)
    {
        base.OnSetup(param);
        textLB.text = ((DialogTextParam)param).text;
    }

    public void OnOKBtn()
    {
        DialogManager.Instance.HideDialog(index);
    }    
}
