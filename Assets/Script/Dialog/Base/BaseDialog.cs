using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex index;
    private BaseDialogAnimation baseDialogAnimation;
    private void Awake()
    {
        baseDialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }
    public virtual void OnSetup(DialogParam param)
    { }
    public void OnShow(Action callBack)
    {
        baseDialogAnimation.OnShowAnim(() =>
        {
            OnShowDialog();
            callBack?.Invoke();
        });
    }
    public void OnHide(Action callBack)
    {
        baseDialogAnimation.OnHideAnim(() =>
        {
            OnHidewDialog();
            callBack?.Invoke();
        });
    }
    public virtual void OnShowDialog()
    { }
    public virtual void OnHidewDialog()
    { }
}
